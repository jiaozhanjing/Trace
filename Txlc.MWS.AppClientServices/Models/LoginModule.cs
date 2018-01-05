using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Txlc.MWS.Util;
using LeaRun.Util.Log;
using Txlc.MWS.AppBusiness.LoginBusiness;
using System.Configuration;

namespace Txlc.MWS.AppServices.Models
{
    public class LoginModule : BaseModule
    {
        public LoginModule()
            : base("txlc/api")
        {
            Post["/CheckLogin"] = CheckLogin;
            Post["/GetServiceTime"] = GetServiceTime;
            Post["/GetVersion"] = GetVersion;
            Get["/download/{name}"] = _ =>
            {
                string fileName = _.name;
                var relatePath = @"download\" + fileName;
                return GetFile(relatePath);
            };

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
        /// 登陆验证
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CheckLogin(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<PDALoginData>();
            try
            {
                #region "接口调用验证"
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);

                if (!flag)
                {
                    return this.WriteValidationLog("LoginModule", recdata.Mac);
                }
                #endregion

                PDALoginBusiness bl = new PDALoginBusiness();
                PDAloginUserInfo UserInfo = bl.CheckLogin(recdata.data.userName, recdata.data.password);
                UserInfo.token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                UserInfo.sessionId = Guid.NewGuid().ToString("N");

                UserInfo.limitSign = Convert.ToInt32(ConfigurationManager.AppSettings["LIMIT_SIGN"]);
                int TokenTime = Convert.ToInt32(ConfigurationManager.AppSettings["TOKEN_TIME"]);
                if (UserInfo != null)
                {
                    //记录token
                    this.WriteCache(UserInfo.token, UserInfo.sessionId, TokenTime);

                    WriteInfoLog("LoginModule", recdata.data.userName, recdata.Mac, "登陆成功！");

                    return this.SendData<PDAloginUserInfo>(UserInfo, "登陆成功", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("LoginModule", recdata.data.userName, recdata.Mac, "登陆失败！请检查用户！" + Json.ToJson(recdata), "登陆失败！请检查用户！");
                }
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("LoginModule", recdata.data.userName, recdata.Mac, "登陆异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "登陆异常：" + ex.Message);
            }
        }
    }





}