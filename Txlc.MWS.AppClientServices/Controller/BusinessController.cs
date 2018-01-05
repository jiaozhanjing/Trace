using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.BusinessModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class BusinessController : BaseModule
    {
        public BusinessController()
            : base("hzsy/api/business")
        {
            // Post["/banner"] = Banner;
            Post["/businessSummary"] = BusinessSummary;
            Post["/businessCompSummary"] = BusinessCompSummary;
            Post["/businessDetail"] = BusinessDetail;

        }
        public string BusinessSummary(dynamic _)
        {
            var recdata = this.GetResquetData<BusinessInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                BusinessBusiness messageBll = new BusinessBusiness();
                List<BusinessSummaryModel> info = messageBll.GetBusinessSummary();
                if (info != null)
                {
                    return this.SendData<List<BusinessSummaryModel>>(info, "获取合计经营户成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("合计经营户为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "合计经营户异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "合计经营户异常：" + ex.Message);

            }
        }
        public string BusinessCompSummary(dynamic _)
        {
            var recdata = this.GetResquetData<BusinessInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                BusinessBusiness messageBll = new BusinessBusiness();
                List<BusinessCompSummaryModel> info = messageBll.GetBusinessCompSummary(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<BusinessCompSummaryModel>>(info, "获取企业合计经营户成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("企业合计经营户为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "企业合计经营户异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "企业合计经营户异常：" + ex.Message);

            }
        }

        public string BusinessDetail(dynamic _)
        {
            var recdata = this.GetResquetData<BusinessInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                BusinessBusiness messageBll = new BusinessBusiness();
                List<BusinessDetailModel> info = messageBll.GetBusinessDetail(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<BusinessDetailModel>>(info, "获取经营户列表成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("获取经营户列表为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取经营户列表异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取经营户列表异常：" + ex.Message);

            }
        }
    }
}