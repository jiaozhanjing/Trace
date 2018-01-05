using System;
using System.Configuration;
using Trace.AppBusiness;
using Trace.AppModel.CommonModel;
using Trace.AppModel.LoginModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class LoginController : BaseModule
    {
        public LoginController()
            : base("hzsy/api/login")
        {
            Post["/normalLogin"] = NormalLogin;
            Post["/messageLogin"] = MessageLogin;
            Post["/modifyPassword"] = ModifyPassword;
            Post["/outLogin"] = OutLogin;
        }
        /// <summary>
        /// 普通登陆
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string NormalLogin(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<LoginInModel>();
           // WriteInfoLog("LoginModule", recdata.data.LoginName, recdata.DeviceId, "登陆成功123123123123123！");
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion

                LoginBusiness bl = new LoginBusiness();
                LoginOutModel UserInfo = bl.NormalLogin(recdata.data.LoginName);

                int TokenTime = Convert.ToInt32(ConfigurationManager.AppSettings["TOKEN_TIME"]);
                if (UserInfo != null)
                {
                    if (UserInfo.State == 1)
                    {
                        return SendData("账户已注销！", ResponseType.Fail);
                    }                 
                    else if (UserInfo.Password != recdata.data.Password)
                    {
                        return SendData("密码不正确！", ResponseType.Fail);
                    }
                    else
                    {
                        //记录token
                        this.WriteCache(UserInfo.Token, UserInfo.SessionId, TokenTime);

                       WriteInfoLog("LoginModule", recdata.data.LoginName, recdata.DeviceId, "登陆成功！");

                        return this.SendData<LoginOutModel>(UserInfo, "登陆成功", ResponseType.Success);
                    }
                }
                else
                {
                    return SendData("账户不存在！", ResponseType.Fail);
                }
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", recdata.data.LoginName, recdata.DeviceId, "登陆异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "登陆异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 短信快捷登陆
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string MessageLogin(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<LoginInModel>();
         

            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion

                //获取快捷登陆验证码是否正确
                CommonBusiness comBll = new CommonBusiness();
                SmsModel smInfo = comBll.GetLastVerifiCode(recdata.data.MobileCode, "1001");
                if (smInfo == null)
                {
                    return SendData("验证码不存在", ResponseType.Fail);
                }
                else if (smInfo.IsUser == 1)
                {
                    return SendData("验证码已使用", ResponseType.Fail);
                }
                else if (smInfo.VerificationCode != recdata.data.VerificationCode)
                {
                    return SendData("验证码不正确", ResponseType.Fail);
                }
                else
                {
                    LoginBusiness bl = new LoginBusiness();
                    LoginOutModel UserInfo = bl.NormalLogin(recdata.data.MobileCode);

                    int TokenTime = Convert.ToInt32(ConfigurationManager.AppSettings["TOKEN_TIME"]);
                    if (UserInfo != null)
                    {
                        if (UserInfo.State == 1)
                        {
                            return SendData("账户已注销！", ResponseType.Fail);
                        }                 
                        else
                        {
                            int i = comBll.UpdateSmsUsingById(smInfo.Id);
                            if (i != 1)
                            {
                                return SendData("验证码状态修改失败！", ResponseType.Fail);
                            }
                            //记录token
                            this.WriteCache(UserInfo.Token, UserInfo.SessionId, TokenTime);

                            WriteInfoLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "登陆成功！");



                            return this.SendData<LoginOutModel>(UserInfo, "登陆成功", ResponseType.Success);
                        }
                    }
                    else
                    {
                        return SendData("账户不存在！", ResponseType.Fail);
                    }
                }
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "登陆异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "登陆异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string ModifyPassword(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<LoginInModel>();
         //   WriteInfoLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "测试：：" + Json.ToJson(recdata));
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion

                //获取快捷登陆验证码是否正确
                CommonBusiness comBll = new CommonBusiness();
                SmsModel smInfo = comBll.GetLastVerifiCode(recdata.data.MobileCode, "1002");
                if (smInfo == null)
                {
                    return SendData("验证码不存在", ResponseType.Fail);
                }
                else if (smInfo.IsUser == 1)
                {
                    return SendData("验证码已使用", ResponseType.Fail);
                }
                else if (smInfo.VerificationCode != recdata.data.VerificationCode)
                {
                    return SendData("验证码不正确", ResponseType.Fail);
                }
                else
                {
                    int i = comBll.UpdateSmsUsingById(smInfo.Id);
                    if (i != 1)
                    {
                        return SendData("验证码状态修改失败！", ResponseType.Fail);
                    }
                    LoginBusiness bl = new LoginBusiness();
                    int iUpdate = bl.ModifyPassword(recdata.data.MobileCode, recdata.data.Password);
                    if (iUpdate == 1)
                    {
                        WriteInfoLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "密码修改成功！");
                        return SendData("密码修改成功！", ResponseType.Success);
                    }
                    else
                    {
                        return SendData("密码修改失败！", ResponseType.Fail);
                    }
                }
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "密码修改异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "密码修改异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string OutLogin(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<LoginInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion

                this.RomveCache(recdata.data.UserId);
                return SendData("退出成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", recdata.data.MobileCode, recdata.DeviceId, "密码修改异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "密码修改异常：" + ex.Message);
            }
        }
    }
}