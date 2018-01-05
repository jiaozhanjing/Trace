using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.TrendModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class TrendController : BaseModule
    {
        public TrendController()
            : base("hzsy/api/trend")
        {
            Post["/marketPrice"] = MarketPrice;
            Post["/ratailPrice"] = RatailPrice;

        }

        public string MarketPrice(dynamic _)
        {
            var recdata = this.GetResquetData<TrendInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                TrendBusiness messageBll = new TrendBusiness();
                List<TrendOutModel> info = messageBll.GetAvgPrice(recdata.data, "1001");
                if (info != null)
                {
                    return this.SendData<List<TrendOutModel>>(info, "获取批发价格成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("批发价格为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "批发价格异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "批发价格异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 零售价格
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string RatailPrice(dynamic _)
        {
            var recdata = this.GetResquetData<TrendInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                TrendBusiness messageBll = new TrendBusiness();
                List<TrendOutModel> info = messageBll.GetAvgPrice(recdata.data, "1002");
                if (info != null)
                {
                    return this.SendData<List<TrendOutModel>>(info, "获取零售价格成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("零售价格为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "零售价格异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "零售价格异常：" + ex.Message);

            }
        }
    }
}