using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.RetailModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class RetailController : BaseModule
    {
        public RetailController()
            : base("hzsy/api/retail")
        {
            Post["/retailCheckList"] = RetailCheckList;
            Post["/retailCheckDetail"] = RetailCheckDetail;
        }

        public string RetailCheckList(dynamic _)
        {
            var recdata = this.GetResquetData<RetailInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                RetailBusiness messageBll = new RetailBusiness();
                List<RetailBaseModel> info = messageBll.GetRetailCheckList(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<RetailBaseModel>>(info, "获取农贸考核排名成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("农贸考核排名为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "农贸考核排名异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "农贸考核排名异常：" + ex.Message);

            }
        }

        public string RetailCheckDetail(dynamic _)
        {
            var recdata = this.GetResquetData<RetailInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                RetailBusiness messageBll = new RetailBusiness();
                List<RetailDetailModel> info = messageBll.GetRetailCheckDetail(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<RetailDetailModel>>(info, "获取农贸考核明细成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("农贸考核明细为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "农贸考核明细异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "农贸考核明细异常：" + ex.Message);

            }
        }
    }
}