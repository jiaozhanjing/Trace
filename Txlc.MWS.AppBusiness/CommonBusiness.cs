using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.CommonModel;
using Trace.AppModel.LoginModel;
using Trace.Dal;
using Txlc.MWS.Util;

namespace Trace.AppBusiness
{
    public class CommonBusiness : BaseBusiness
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public SmsModel GetVerificationCodeAndSend(string tel, string codeType)
        {
            try
            {
                SmsModel info = null;
                string strCode = CommonHelper.RndNum(6);
                string[] strData = new string[] { strCode };
                string i = codeType == "1001" ? "1" : "2";
                string strTemp = SendSms(tel, strData, Convert.ToInt32(codeType), i, strCode);
                info = new SmsModel();
                info.CodeType = codeType;
                info.MobileCode = tel;
                info.VerificationCode = strCode;
                info.Id = strTemp;
                return info;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// 获得最近发的一个验证码
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public SmsModel GetLastVerifiCode(string tel, string codeType)
        {
            SmsModel Info = null;

            string strSql = string.Format(@"select * from (select t.id,
                                           t.mobile_code,
                                           t.security_code,
                                           t.time_out,
                                           t.is_user,
                                           t.using_time,
                                           t.code_type,
                                           t.insert_time
                                      from BASE_SMS t
                                     where t.mobile_code = :v_mobile_code
                                       and t.time_out >= :v_time_out                                      
                                       and t.code_type = :v_code_type 
                                      order by t.insert_time desc ) where rownum <= 1 
                                          ");
            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":v_mobile_code", tel));
            aslgList.Add(new OracleParameter(":v_time_out", DateTime.Now));
            aslgList.Add(new OracleParameter(":v_code_type", Convert.ToInt32(codeType)));

            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Info = new SmsModel();
                    Info.MobileCode = dt.Rows[0]["mobile_code"].ToString();
                    Info.VerificationCode = dt.Rows[0]["security_code"].ToString();
                    Info.Id = dt.Rows[0]["id"].ToString();
                    Info.IsUser = Convert.ToInt32(dt.Rows[0]["is_user"]);

                }
            }
            return Info;


        }

        /// <summary>
        /// 根据短信ID更新，短信的使用状态
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public int UpdateSmsUsingById(string strId)
        {
            string strSql = string.Format(@" update base_sms t set t.is_user=1 where t.id=:v_id ");
            return OracleHelper.ExecuteNonQuery(strSql, new OracleParameter(":v_id", strId));
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="tel">手机号</param>
        /// <param name="smsData">验证码，字符串数组形式</param>
        /// <param name="iCodeType">验证码类型1001登录验证码；1002修改密码</param>
        /// <param name="strModelNumber">模板类型1：登陆；2修改密码</param>
        /// <returns></returns>
        public string SendSms(string tel, string[] smsData, int iCodeType, string strModelNumber, string strVerifiCode)
        {
            string strRet = string.Empty;

            #region "短信"
            string ret = null;
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            string SMS_URL = ConfigurationManager.AppSettings["SMS_URL"].ToString().Trim();
            string SMS_PORT = ConfigurationManager.AppSettings["SMS_PORT"].ToString().Trim();
            string ACCOUNT_SID = ConfigurationManager.AppSettings["SMS_ACCOUNT_SID"].ToString().Trim();
            string AUTH_TOKEN = ConfigurationManager.AppSettings["SMS_AUTH_TOKEN"].ToString().Trim();
            string APP_ID = ConfigurationManager.AppSettings["SMS_APP_ID"].ToString().Trim();
            string strModel = ConfigurationManager.AppSettings["SMS_MODEL_" + strModelNumber].ToString().Trim();
            bool isInit = api.init(SMS_URL, SMS_PORT);
            api.setAccount(ACCOUNT_SID, AUTH_TOKEN);
            api.setAppId(APP_ID);
            try
            {
                if (isInit)
                {

                    Dictionary<string, object> retData = api.SendTemplateSMS(tel, strModel, smsData);
                    ret = getDictionaryData(retData);
                    //短信发送成功！
                    if (ret.Contains("成功"))
                    {
                        DateTime outTime = DateTime.Now.AddMinutes(5);
                        string strSql = string.Format(@"insert into BASE_SMS
                          (id, MOBILE_CODE, SECURITY_CODE, TIME_OUT, IS_USER, CODE_TYPE)
                        values
                          (:v_id, :v_mobile_code, :V_SECURITY_CODE, :V_TIME_OUT, :V_IS_USER, :V_CODE_TYPE)");
                        List<OracleParameter> aslgList = new List<OracleParameter>();
                        string strGuid = GUID;
                        strRet = strGuid;
                        aslgList.Add(new OracleParameter(":v_id", strGuid));
                        aslgList.Add(new OracleParameter(":v_mobile_code", tel));
                        aslgList.Add(new OracleParameter(":V_SECURITY_CODE", strVerifiCode));
                        aslgList.Add(new OracleParameter(":V_TIME_OUT", outTime));
                        aslgList.Add(new OracleParameter(":V_IS_USER", Convert.ToInt32(0)));
                        aslgList.Add(new OracleParameter(":V_CODE_TYPE", iCodeType));
                        int ires = OracleHelper.ExecuteNonQuery(strSql, aslgList.ToArray());

                        if (ires <= 0)
                        {
                            throw new Exception("短信验证码插入失败！");
                        }

                    }
                    else
                    {
                        throw new Exception("验证码发送失败，超过发送次数！");
                    }
                }
                else
                {

                    throw new Exception("短信发送，初始化失败");
                }
                return strRet;
            }
            catch (Exception exc)
            {

                throw new Exception("短信发送失败：" + exc.Message);
            }
            #endregion
        }
        public string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }
        /// <summary>
        /// 检测手机号是否存在
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public LoginOutModel CheckTelphone(string tel)
        {
            LoginOutModel Info = null;
            string strSql = string.Format(@"select t.id,t.telphone,t.IS_DELETED from base_user t where  t.telphone=:v_telphone");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_telphone", tel));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Info = new LoginOutModel();
                    Info.UserId = DBNull.Value == dt.Rows[0]["id"] ? string.Empty : dt.Rows[0]["id"].ToString();
                    Info.MobileCode = DBNull.Value == dt.Rows[0]["telphone"] ? string.Empty : dt.Rows[0]["telphone"].ToString();
                    Info.State = Convert.ToInt32(dt.Rows[0]["IS_DELETED"]);
                }
            }
            return Info;
        }


    }
}
