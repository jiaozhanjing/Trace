using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaRun.Util.Log;
using Txlc.MWS.Util;
using Txlc.MWS.AppBusiness;

namespace Txlc.MWS.AppServices.Models
{
    public class UserModule : BaseModule
    {
        public UserModule()
            : base("txlc/api")
        {
            Post["/GetUserInfo"] = GetUserInfo;
            Post["/AddUserInfo"] = AddUserInfo;
            Post["/GetIcInfo"] = GetIcInfo;
            Post["/UpdatePassword"] = UpdatePassword;
            Post["/GetAccountSeq"] = GetAccountSeq;
            Post["/GetAccountSum"] = GetAccountSum;
            Post["/GetAccountInfo"] = GetAccountInfo;
        }

        public string GetAccountInfo(dynamic _)
        {
            var recdata = this.GetResquetData<AccountInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("UserModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAUserBusiness bl = new PDAUserBusiness();
                AccountInfo info = bl.GetAccountInfo(recdata.data.userId);
                //WriteInfoLog("OrderModule", recdata.data.userName, recdata.Mac, "获取账单！");

                return this.SendData<AccountInfo>(info, "获取账户信息成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "获取账户信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取账户信息异常：" + ex.Message);
            }
        }

        public string GetAccountSum(dynamic _)
        {
            var recdata = this.GetResquetData<AccountSumInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("UserModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAUserBusiness bl = new PDAUserBusiness();
                List<AccountSumInfo> info = bl.GetAccountSum(recdata.data);
                //WriteInfoLog("OrderModule", recdata.data.userName, recdata.Mac, "获取账单！");

                return this.SendData<List<AccountSumInfo>>(info, "获取账单汇总成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "获取账单汇总异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取账单汇总异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GetAccountSeq(dynamic _)
        {
            var recdata = this.GetResquetData<AccountSeqInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("OrderModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAUserBusiness bl = new PDAUserBusiness();
                AccountSeqOutInfo info = bl.GetAccountSeq(recdata.data);
                //WriteInfoLog("OrderModule", recdata.data.userName, recdata.Mac, "获取账单！");

                return this.SendData<AccountSeqOutInfo>(info, "获取账单成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "获取账单异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取账单异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string UpdatePassword(dynamic _)
        {
            var recdata = this.GetResquetData<UpdatePassInfo>();
             WriteInfoLog("OrderModule", "updatepassword", "bb", Json.ToJson(recdata));
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("UserModule", recdata.Mac);
                }
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }
                PDAUserBusiness bl = new PDAUserBusiness();
                int ires = bl.UpdatePassword(recdata.data);
                if (ires == -1)
                {
                    ResponseModule res = new ResponseModule();
                    res.code = "305";
                    res.message = "原始密码不正确！";
                    string strJson = Json.ToJson(res);
                    return strJson;

                }
                if (ires == 1)
                {
                    return this.SendData("修改密码成功！", ResponseType.Success);
                }
                else
                {
                    throw new Exception("未找到用户名！");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "修改密码异常：" + Json.ToJson(recdata) + "[异常信息：" + ex.Message + "]", "修改密码异常：" + ex.Message);
            }
        }
        public string GetUserInfo(dynamic _)
        {
            var recdata = this.GetResquetData<PDAUserInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("UserModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAUserBusiness bl = new PDAUserBusiness();
                PDAUserInfo info = bl.GetUserInfo(recdata.data.userId);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                return this.SendData<PDAUserInfo>(info, "获取用户信息成功！", ResponseType.Success);



            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取用户信息异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 保存信息成功
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string AddUserInfo(dynamic _)
        {

            var recdata = this.GetResquetData<PDAUserInfo>();

            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("UserModule", recdata.Mac);
                }
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }
                PDAUserBusiness bl = new PDAUserBusiness();
                int ires = bl.AddUserInfo(recdata.data);

                return this.SendData("保存用户信息成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "保存用户信息异常：" + Json.ToJson(recdata) + "[异常信息：" + ex.Message + "]", "保存用户信息异常：" + ex.Message);
            }

        }

        public string GetIcInfo(dynamic _)
        {
            var recdata = this.GetResquetData<PDAUserInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("UserModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAUserBusiness bl = new PDAUserBusiness();
                List<PDAUserICInfo> info = bl.GetIcInfo(recdata.data.userId, recdata.data.userName);
                string temp = Json.ToJson(info);
                //   WriteInfoLog("UserModule", "aaaa", "asdf", temp);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                return this.SendData<List<PDAUserICInfo>>(info, "获取IC卡信息成功！", ResponseType.Success);



            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "获取IC卡信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取IC卡信息异常：" + ex.Message);
            }
        }

    }

}