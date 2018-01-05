using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.SweepModel;
using Trace.AppModel.TraceModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class SweepController : BaseModule
    {
        public SweepController()
            : base("hzsy/api/sweep")
        {
            Post["/sweepTrace"] = SweepTrace;
            Post["/sweepOrderDetail"] = SweepOrderDetail;
        }

        public string SweepTrace(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<SweepModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                SweepBusiness sweepBll = new SweepBusiness();

                TraceModel info = sweepBll.GetTraceInfoByTraceCode(recdata.data.TraceCode);

                if (info != null)
                {
                    return this.SendData<TraceModel>(info, "扫码成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("追溯信息为空！", ResponseType.Success);

                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "扫码异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "扫码异常：" + ex.Message);

            }
        }
        public string SweepOrderDetail(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<SweepModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                SweepBusiness sweepBll = new SweepBusiness();

                TraceModel info = sweepBll.GetTraceInfoByTraceCode(recdata.data.TraceCode);

                if (info != null)
                {
                    return this.SendData<TraceModel>(info, "扫码成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("订单信息为空！", ResponseType.Success);

                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "订单扫码异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "订单扫码异常：" + ex.Message);

            }
        }
    }
}