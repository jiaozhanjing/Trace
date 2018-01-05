using System;
using System.Collections.Generic;
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
    public class LoginBusiness : BaseBusiness
    {
        /// <summary>
        /// 普通登陆
        /// </summary>
        /// <param name="strMobileCode">,手机号或者账号</param>
        /// <returns></returns>
        public LoginOutModel NormalLogin(string strMobileCodeOrAccountName)
        {
            LoginOutModel Info = null;
            try
            {
                string strSql = string.Format(@"select id,
                                               user_name,
                                               telphone,
                                               password,
                                               insert_time,
                                               organ_id,
                                               update_time,
                                               real_name,
                                               is_deleted,
                                               user_type,organ_name 
                                          from base_user t where t.telphone=:V_TELPHONE or t.user_name=:v_user_name");



                DataTable dt = OracleHelper.Query(strSql, new OracleParameter("V_TELPHONE", strMobileCodeOrAccountName), new OracleParameter("v_user_name", strMobileCodeOrAccountName));


                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Info = new LoginOutModel();

                        Info.CompName = DBNull.Value == dt.Rows[0]["organ_name"] ? string.Empty : dt.Rows[0]["organ_name"].ToString();
                        Info.MobileCode = dt.Rows[0]["TELPHONE"].ToString();
                        Info.RealName = DBNull.Value == dt.Rows[0]["real_name"] ? string.Empty : dt.Rows[0]["real_name"].ToString();
                        Info.State = Convert.ToInt32(dt.Rows[0]["is_deleted"]);
                        Info.UserId = DBNull.Value == dt.Rows[0]["id"] ? string.Empty : dt.Rows[0]["id"].ToString();
                        Info.SessionId = Info.UserId;
                        Info.Token = DESEncrypt.Encrypt(Info.SessionId);
                        Info.UserType = DBNull.Value == dt.Rows[0]["user_type"] ? string.Empty : dt.Rows[0]["user_type"].ToString();
                        //Info.UserType = DBNull.Value == dt.Rows[0]["user_type"] ? string.Empty : dt.Rows[0]["user_type"].ToString();
                        Info.OrganId = DBNull.Value == dt.Rows[0]["organ_id"] ? string.Empty : dt.Rows[0]["organ_id"].ToString(); ;
                        Info.OrganName = DBNull.Value == dt.Rows[0]["organ_name"] ? string.Empty : dt.Rows[0]["organ_name"].ToString(); ;
                        Info.Password = DBNull.Value == dt.Rows[0]["password"] ? string.Empty : dt.Rows[0]["password"].ToString(); ;
                        Info.UserName = DBNull.Value == dt.Rows[0]["user_name"] ? string.Empty : dt.Rows[0]["user_name"].ToString(); ;
                    }
                }
                return Info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 快捷登陆
        /// </summary>
        /// <param name="strMobileCode"></param>
        /// <param name="strVerifiCode"></param>
        /// <returns></returns>
        public LoginOutModel MessageLogin(string strMobileCode)
        {
            try
            {
                LoginOutModel Info = NormalLogin(strMobileCode);

                return Info;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 修改登陆密码
        /// </summary>
        /// <param name="strMobileCode"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public int ModifyPassword(string strMobileCode, string strPassword)
        {
            try
            {
                string strSql = string.Format(@"update base_user t set t.password =:v_password where t.telphone=:v_telphone ");
                List<OracleParameter> aslgList = new List<OracleParameter>();
                aslgList.Add(new OracleParameter(":v_password", strPassword));
                aslgList.Add(new OracleParameter(":v_telphone", strMobileCode));
                int iRet = OracleHelper.ExecuteNonQuery(strSql, aslgList.ToArray());
                return iRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
