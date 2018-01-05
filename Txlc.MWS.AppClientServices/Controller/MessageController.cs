using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trace.AppBusiness;
using Trace.AppModel;
using Trace.AppModel.MessageModel;
using Txlc.MWS.Util;

namespace Trace.AppServices.Controller
{
    public class MessageController : BaseModule
    {
        public MessageController()
            : base("hzsy/api/message")
        {
           // Post["/banner"] = Banner;
            Post["/noticeList"] = NoticeList;
            Post["/noticeDetail"] = NoticeDetail;
        }
     
        public string NoticeList(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<BaseModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                MessageBusiness messageBll = new MessageBusiness();
                List<NoticeModel> info = messageBll.GetNoticeList(recdata.data.PageSize, recdata.data.PageIndex);
                if (info != null)
                {
                    return this.SendData<List<NoticeModel>>(info, "获取公告列表成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("公告列表为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "公告列表异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "公告列表异常：" + ex.Message);

            }
        }

        public string NoticeDetail(dynamic _)
        {
            //接收request数据
            var recdata = this.GetResquetData<NoticeInModel>();
            try
            {
                #region "接口调用验证"
                //bool flag = DataValidation(recdata.DateTime, recdata.Random, recdata.Sign);

                //if (!flag)
                //{
                //    return this.WriteValidationLog("LoginModule", recdata.DeviceId);
                //}
                #endregion
                MessageBusiness messageBll = new MessageBusiness();
                NoticeModel info = messageBll.GetNoticeInfo(recdata.data.NoticeId);
                if (info != null)
                {
                    return this.SendData<NoticeModel>(info, "获取公告成功", ResponseType.Success);
                }
                else
                {
                    return this.SendData("公告为空！", ResponseType.Success);

                }

            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("LoginModule", recdata.DeviceId, recdata.DeviceId, "公告异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "公告异常：" + ex.Message);

            }
        }
    }
}