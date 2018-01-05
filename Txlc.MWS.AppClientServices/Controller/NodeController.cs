using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.NodeModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class NodeController : BaseModule
    {
        public NodeController()
            : base("hzsy/api/node")
        {
            // Post["/banner"] = Banner;
            Post["/nodeSummary"] = NodeSummary;
            Post["/nodeCompSummary"] = NodeCompSummary;
            Post["/nodeInfo"] = NodeInfo;
        }

        public string NodeSummary(dynamic _)
        {
            var recdata = this.GetResquetData<NodelInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                NodeBusiness messageBll = new NodeBusiness();
                List<NodeSummaryModel> info = messageBll.GetNodeSummary();
                if (info != null)
                {
                    return this.SendData<List<NodeSummaryModel>>(info, "获取合计经营节点成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("合计经营节点为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "合计经营节点异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "合计经营节点异常：" + ex.Message);

            }
        }

        public string NodeCompSummary(dynamic _)
        {
            var recdata = this.GetResquetData<NodelInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                NodeBusiness messageBll = new NodeBusiness();
                List<NodeCompSummaryModel> info = messageBll.GetNodeCompSummary(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<NodeCompSummaryModel>>(info, "获取经营节点列表成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("获取经营节点列表为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取经营节点异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取经营节点异常：" + ex.Message);

            }
        }

        public string NodeInfo(dynamic _)
        {
            var recdata = this.GetResquetData<NodelInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                NodeBusiness messageBll = new NodeBusiness();
                NodeInfoModel info = messageBll.GetNodeInfo(recdata.data.CompCode);
                if (info != null)
                {
                    return this.SendData<NodeInfoModel>(info, "获取经营节点详细成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("获取经营节点列详细为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "获取经营节点详细异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取经营节点详细异常：" + ex.Message);

            }
        }
    }
}