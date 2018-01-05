using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.CommonModel;
using Trace.AppModel.LoginModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class CommonController : BaseModule
    {
        public CommonController()
            : base("hzsy/api/common")
        {
            Post["/verificationCode"] = VerificationCode;
            Post["/getLastVerifiCode"] = GetLastVerifiCode;
            Post["/about"] = About;
            Post["/serviceTime"] = GetServiceTime;
            Get["/download/{name}"] = _ =>
            {
                string fileName = _.name;
                var relatePath = @"download\" + fileName;
                return GetFile(relatePath);
            };
            Post["/getVersion"] = GetVersion;
        }
        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetVersion(dynamic _)
        {
            try
            {
                string strVersion = ConfigurationManager.AppSettings["VERSION"].ToString();
                return this.SendData(strVersion, "获取版本信息成功", ResponseType.Success);

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", "", "", "获取版本失败!" + ex.Message, "获取版本失败！" + ex.Message);
            }


        }
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetServiceTime(dynamic _)
        {
            try
            {
                string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return this.SendData(strDateTime, "获取服务器时间成功", ResponseType.Success);
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", "", "", "获取时间失败!" + ex.Message, "获取时间失败！" + ex.Message);
            }

        }
        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string About(dynamic _)
        {
            try
            {
                string strAbout = "关于的内容，关于的内容，关于的内容，关于的内容关于的内容，关于的内容关于的内容，关于的内容关于的内容，关于的内容关于的内容，关于的内容";
                return this.SendData(strAbout, "获取关于内容成功", ResponseType.Success);
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", "", "", "获取关于内容失败!" + ex.Message, "获取关于内容失败！" + ex.Message);
            }
        }
        /// <summary>
        /// 获取并发送验证码
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string VerificationCode(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<SmsModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                CommonBusiness bl = new CommonBusiness();
                if (recdata.data.CodeType != "1003")
                {
                    LoginOutModel accountInfo = bl.CheckTelphone(recdata.data.MobileCode);

                    if (accountInfo == null)
                    {
                        return SendData("手机号不存在，请检查账户！", ResponseType.Fail);
                    }
                    else if (accountInfo.State == 1)
                    {
                        return SendData("手机号已冻结！", ResponseType.Fail);
                    }
                    else if (accountInfo.State == 2)
                    {
                        return SendData("手机号已注销！", ResponseType.Fail);
                    }
                    else
                    {
                        SmsModel smsInfo = bl.GetVerificationCodeAndSend(recdata.data.MobileCode, recdata.data.CodeType);
                        if (smsInfo != null)
                        {
                            return this.SendData<SmsModel>(smsInfo, "验证码发送成功！", ResponseType.Success);
                        }
                        else
                        {
                            return SendData("验证码发送失败！", ResponseType.Fail);
                        }
                    }
                }
                else
                {
                    SmsModel smsInfo = bl.GetVerificationCodeAndSend(recdata.data.MobileCode, recdata.data.CodeType);
                    if (smsInfo != null)
                    {
                        return this.SendData<SmsModel>(smsInfo, "验证码发送成功！", ResponseType.Success);
                    }
                    else
                    {
                        return SendData("验证码发送失败！", ResponseType.Fail);
                    }
                }
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "验证码发送异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "验证码发送异常：" + ex.Message);
            }
        }

        public string GetLastVerifiCode(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<SmsModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion

                CommonBusiness bl = new CommonBusiness();
                LoginOutModel accountInfo = bl.CheckTelphone(recdata.data.MobileCode);
                if (accountInfo == null)
                {
                    return SendData("手机号不存在，请检查账户！", ResponseType.Fail);
                }
                else if (accountInfo.State == 1)
                {
                    return SendData("手机号已冻结！", ResponseType.Fail);
                }
                else if (accountInfo.State == 2)
                {
                    return SendData("手机号已注销！", ResponseType.Fail);
                }
                else
                {
                    SmsModel smsInfo = bl.GetLastVerifiCode(recdata.data.MobileCode, recdata.data.CodeType);
                    if (smsInfo != null)
                    {
                        return this.SendData<SmsModel>(smsInfo, "验证码发送成功！", ResponseType.Success);
                    }
                    else
                    {
                        return SendData("验证码发送失败！", ResponseType.Fail);
                    }
                }
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "验证码发送异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "验证码发送异常：" + ex.Message);
            }
        }

    }
}