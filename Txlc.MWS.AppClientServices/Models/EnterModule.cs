using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaRun.Util.Log;
using Txlc.MWS.Util;
using Txlc.MWS.AppBusiness;

namespace Txlc.MWS.AppServices.Models
{
    public class EnterModule : BaseModule
    {
        public EnterModule()
            : base("txlc/api")
        {
            Post["/GetanimalQuarantineIds"] = GetanimalQuarantineIds;
            Post["/GetWeight"] = GetWeight;
            Post["/GetEnterSetting"] = GetEnterSetting;
            Post["/AddEnterSetting"] = AddEnterSetting;
            Post["/UpdateEnterSetting"] = UpdateEnterSetting;
            Post["/DeleteEnterSetting"] = DeleteEnterSetting;
            Post["/GetTallyInfo"] = GetTallyInfo;
            Post["/SaveEnterInfo"] = SaveEnterInfo;
            Post["/GetEnterInfo"] = GetEnterInfo;
            Post["/GetEnterDetail"] = GetEnterDetail;
            Post["/GetCompList"] = GetCompList;
            Post["/GetTranInfo"] = GetTranInfo;


        }
        /// <summary>
        /// 获取交易重量信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetTranInfo(dynamic _)
        {
            var recdata = this.GetResquetData<TranInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();

                TranInfo EnterInfos = bl.GetTranWeightInfo(recdata.data.userId, recdata.data.compId);

                if (EnterInfos.surplusWeight <= 0)
                {
                    EnterInfos = bl.GetTranWeightInfo(recdata.data.userId, null);
                }

                WriteInfoLog("EnterModule", recdata.data.userName, recdata.Mac, "获取交易重量成功！" + Json.ToJson(recdata));

                return this.SendData<TranInfo>(EnterInfos, "获取重量成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取企业信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取重量异常：" + ex.Message);

            }
        }

        /// <summary>
        /// 获取屠宰企业信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetCompList(dynamic _)
        {
            var recdata = this.GetResquetData<BaseNode>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();

                List<BaseNode> EnterInfos = bl.GetCompList(recdata.data.areaCode);

                WriteInfoLog("EnterModule", recdata.data.userName, recdata.Mac, "获取屠宰企业信息成功！" + Json.ToJson(recdata));

                return this.SendData<List<BaseNode>>(EnterInfos, "获取企业信息成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取企业信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取动物检疫证号异常：" + ex.Message);

            }
        }

        /// <summary>
        /// 获取进场动物检疫证号
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetanimalQuarantineIds(dynamic _)
        {
            var recdata = this.GetResquetData<PDAEnterIn>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();

                List<PDAEnterOut> EnterInfos = bl.GetanimalQuarantineIds(recdata.data.userId);

                WriteInfoLog("EnterModule", recdata.data.userName, recdata.Mac, "获取动物检疫证号成功！" + Json.ToJson(recdata));

                return this.SendData<List<PDAEnterOut>>(EnterInfos, "获取动物检疫证号成功！", ResponseType.Success);


            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取动物检疫证号异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取动物检疫证号异常：" + ex.Message);
            }

        }
        /// <summary>
        /// 获取理货称重数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetWeight(dynamic _)
        {
            var recdata = this.GetResquetData<PDAWeightIn>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                PDAWeightOut WeightInfo = bl.GetWeight(recdata.data.userId, recdata.data.hookNum);

                //WriteInfoLog("EnterModule", recdata.data.userName, recdata.Mac, "获取理货重量成功！");
                if (WeightInfo != null)
                {
                    return this.SendData<PDAWeightOut>(WeightInfo, "获取理货重量成功！", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "该钩号无理货信息！" + Json.ToJson(recdata), "该钩号无理货信息！");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取理货数据异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取理货数据异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 获取进场设置
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetEnterSetting(dynamic _)
        {
            var recdata = this.GetResquetData<PDAEnterIn>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                List<PDAEnterSetOut> info = bl.GetEnterSetting(recdata.data.userId, recdata.data.userName);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                return this.SendData<List<PDAEnterSetOut>>(info, "获取进场设置成功！", ResponseType.Success);



            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取用户信息异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 添加进场设置
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string AddEnterSetting(dynamic _)
        {
            var recdata = this.GetResquetData<PDAEnterSetOut>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                PDAEnterSetOut ires = bl.AddEnterSetting(recdata.data);
                if (ires != null)
                {
                    return this.SendData<PDAEnterSetOut>(ires, "添加进场设置成功！", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "添加进场设置异常！" + Json.ToJson(recdata), "添加进场设置异常！");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "添加进场设置异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "添加进场设置异常：" + ex.Message);
            }

        }
        /// <summary>
        /// 编辑进场设置
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string UpdateEnterSetting(dynamic _)
        {
            var recdata = this.GetResquetData<PDAEnterSetOut>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                PDAEnterSetOut ires = bl.UpdateEnterSetting(recdata.data);
                if (ires != null)
                {
                    return this.SendData<PDAEnterSetOut>(ires, "编辑进场设置成功！", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "编辑进场设置异常！" + Json.ToJson(recdata), "编辑进场设置异常！");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("UserModule", recdata.data.userName, recdata.Mac, "编辑进场设置异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "编辑进场设置异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DeleteEnterSetting(dynamic _)
        {
            var recdata = this.GetResquetData<PDAEnterSetOut>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                int ires = bl.DeleteEnterSetting(recdata.data.comeInMessageId);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                return this.SendData("删除进场设置成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "删除进场设置异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "删除进场设置异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 返回理货信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetTallyInfo(dynamic _)
        {
            var recdata = this.GetResquetData<PDAWeightIn>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                List<PDAWeightOut> info = bl.GetTallyInfo(recdata.data);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                return this.SendData<List<PDAWeightOut>>(info, "获取理货信息成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取理货信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取理货信息异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 保存进场信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string SaveEnterInfo(dynamic _)
        {
            var recdata = this.GetResquetData<PDAEnterInfo>();
            //this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "哈哈哈" + Json.ToJson(recdata), "保存进场异常！");
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                // WriteInfoLog("UserModule",  recdata.data, recdata.Mac, "获取用户信息成功");
                int ires = bl.SaveEnterInfo(recdata.data);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                if (ires > 0)
                {
                    // WriteInfoLog("aaaaaa", "asdf", "asdf", Json.ToJson(recdata));
                    return this.SendData("保存进场信息成功！", ResponseType.Success);

                }
                else
                {
                    return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "保存进场异常!" + Json.ToJson(recdata), "保存进场异常！");
                }
                //return this.SendData("删除进场设置成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "保存进场异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "保存进场异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 获取进场列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetEnterInfo(dynamic _)
        {
            var recdata = this.GetResquetData<PDAWeightIn>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                List<PDAEnterInfo> info = bl.GetEnterInfo(recdata.data);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                return this.SendData<List<PDAEnterInfo>>(info, "获取进场列表成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取进场列表异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取进场列表异常：" + ex.Message);
            }
        }

        public string GetEnterDetail(dynamic _)
        {
            var recdata = this.GetResquetData<PDAEnterIn>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("EnterModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAEnterBusiness bl = new PDAEnterBusiness();
                PDAEnterInfo info = bl.GetEnterDetail(recdata.data);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "测试日志："+Json.ToJson(info));
                if (info != null)
                {
                    return this.SendData<PDAEnterInfo>(info, "获取进场详细成功！", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取进场详细异常" + Json.ToJson(recdata), "获取进场详细异常");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("EnterModule", recdata.data.userName, recdata.Mac, "获取进场详细异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取进场详细异常：" + ex.Message);
            }
        }

    }


}