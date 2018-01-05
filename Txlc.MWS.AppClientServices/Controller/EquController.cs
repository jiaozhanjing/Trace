using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel.EquModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class EquController : BaseModule
    {
        public EquController()
            : base("hzsy/api/equipment")
        {
            Post["/totalList"] = TotalList;
            Post["/compUserList"] = CompUserList;
            Post["/compUserDetail"] = CompUserDetail;
        }
        /// <summary>
        /// 设备统计
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string TotalList(dynamic _)
        {
            var recdata = this.GetResquetData<EquInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                EquBusiness messageBll = new EquBusiness();
                List<EquTotalModel> info = messageBll.GetEquTotal(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<EquTotalModel>>(info, "获取设备统计成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("设备统计为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "设备统计异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "设备统计异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 设备领用企业列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CompUserList(dynamic _)
        {
            var recdata = this.GetResquetData<EquInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                EquBusiness messageBll = new EquBusiness();
                List<EquUserModel> info = messageBll.GetEquUserList(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<EquUserModel>>(info, "获取设备领用成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("设备领用为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "设备领用异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "设备领用异常：" + ex.Message);

            }
        }
        /// <summary>
        /// 设备领用明细
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CompUserDetail(dynamic _)
        {
            var recdata = this.GetResquetData<EquInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                EquBusiness messageBll = new EquBusiness();
                List<EquUserModel> info = messageBll.GetEquUserDetail(recdata.data);
                if (info != null)
                {
                    return this.SendData<List<EquUserModel>>(info, "获取设备领用明细成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("设备领用明细为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "设备领用明细异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "设备领用明细异常：" + ex.Message);

            }
        }
    }
}