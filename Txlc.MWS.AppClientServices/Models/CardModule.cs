using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaRun.Util.Log;
using Txlc.MWS.Util;
using Txlc.MWS.AppBusiness;

namespace Txlc.MWS.AppServices.Models
{
    public class CardModule : BaseModule
    {
        public CardModule()
            : base("txlc/api")
        {
            Post["/SaveCardInfoList"] = SaveCardInfoList;
            Post["/GetCardInfoList"] = GetCardInfoList;
            Post["/DeleteCardInfo"] = DeleteCardInfo;
            Post["/GetCardCompList"] = GetCardCompList;
            Post["/AddCardComp"] = AddCardComp;
            Post["/GetOrderCardInfo"] = GetOrderCardInfo;
        }
        /// <summary>
        /// 获取交易重量信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string SaveCardInfoList(dynamic _)
        {
            var recdata = this.GetResquetData<CardInfoBase>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("CardModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDACardInfoBusiness bl = new PDACardInfoBusiness();

                int iRet = bl.SaveCardInfoList(recdata.data);

                if (iRet <= 0)
                {
                    throw new Exception("添加失败，成功条数为0！");
                }


                //WriteInfoLog("EnterModule", recdata.data.userName, recdata.Mac, "获取交易重量成功！" + Json.ToJson(recdata));

                return this.SendData("添加分销凭证成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("CardModule", recdata.data.userName, recdata.Mac, "添加分销凭证异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "添加分销凭证异常：" + ex.Message);

            }
        }

        /// <summary>
        /// 获取屠宰企业信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetCardInfoList(dynamic _)
        {
            var recdata = this.GetResquetData<CardInfoList>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("CardModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDACardInfoBusiness bl = new PDACardInfoBusiness();

                List<CardInfoList> EnterInfos = bl.GetCardInfoList(recdata.data);

                //WriteInfoLog("CardModule", recdata.data.userName, recdata.Mac, "获取屠宰企业信息成功！" + Json.ToJson(recdata));

                return this.SendData<List<CardInfoList>>(EnterInfos, "获取分销凭证列表成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {

                return this.WriteExceptionLog("CardModule", recdata.data.userName, recdata.Mac, "获取分销凭证列表异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取分销凭证列表异常：" + ex.Message);

            }
        }

        /// <summary>
        /// 获取进场动物检疫证号
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string DeleteCardInfo(dynamic _)
        {
            var recdata = this.GetResquetData<CardInfoListBase>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("CardModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDACardInfoBusiness bl = new PDACardInfoBusiness();

                OrderCardInfo EnterInfos = bl.DeleteCardInfo(recdata.data.rqDeleteCardInfos);

                // WriteInfoLog("CardModule", recdata.data.userName, recdata.Mac, "获取动物检疫证号成功！" + Json.ToJson(recdata));
                if (EnterInfos != null)
                {
                    return this.SendData<OrderCardInfo>(EnterInfos, "删除成功！", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("CardModule", "删除分销凭证", recdata.Mac, "删除分销凭证异常！" + Json.ToJson(recdata), "删除分销凭证异常！");
              
                }
               // return this.SendData("删除分销凭证成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("CardModule", "删除分销凭证", recdata.Mac, "删除分销凭证异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "删除分销凭证异常：" + ex.Message);
            }

        }
        /// <summary>
        /// 获取理货称重数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetCardCompList(dynamic _)
        {
            var recdata = this.GetResquetData<CardCompInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("CardModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDACardInfoBusiness bl = new PDACardInfoBusiness();
                List<CardCompInfo> WeightInfo = bl.GetCardCompList(recdata.data);

                //WriteInfoLog("EnterModule", recdata.data.userName, recdata.Mac, "获取理货重量成功！");
                if (WeightInfo != null)
                {
                    return this.SendData<List<CardCompInfo>>(WeightInfo, "获取分销单位成功！", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("CardModule", recdata.data.userName, recdata.Mac, "该商户无分销单位！" + Json.ToJson(recdata), "该商户无分销单位！");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("CardModule", recdata.data.userName, recdata.Mac, "获取分销单位异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取分销单位异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 获取进场设置
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string AddCardComp(dynamic _)
        {
            var recdata = this.GetResquetData<CardCompInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("CardModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDACardInfoBusiness bl = new PDACardInfoBusiness();
                int iRet = bl.AddCardComp(recdata.data);
                //WriteInfoLog("UserModule", recdata.data.userName, recdata.Mac, "获取用户信息成功");
                if (iRet > 0)
                {
                    return this.SendData("添加分销单位成功！", ResponseType.Success);
                }
                else
                {
                    throw new Exception("添加分销单位失败，添加条数为0！");
                }


            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("CardModule", recdata.data.userName, recdata.Mac, "添加分销单位异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "添加分销单位异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 添加进场设置
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string GetOrderCardInfo(dynamic _)
        {
            var recdata = this.GetResquetData<OrderCardInfo>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("CardModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDACardInfoBusiness bl = new PDACardInfoBusiness();
                OrderCardInfo ires = bl.GetOrderCardInfo(recdata.data);
                if (ires != null)
                {
                    return this.SendData<OrderCardInfo>(ires, "获取订单开证信息成功！", ResponseType.Success);
                }
                else
                {
                    return this.WriteExceptionLog("CardModule", recdata.data.userName, recdata.Mac, "获取订单开证信息异常！" + Json.ToJson(recdata), "获取订单开证信息异常！");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("CardModule", recdata.data.userName, recdata.Mac, "获取订单开证信息异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取订单开证信息异常：" + ex.Message);
            }

        }      

    }


}