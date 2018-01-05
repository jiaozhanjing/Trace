using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.MessageModel
{
    [Serializable]
    public class NoticeModel : BaseModel
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        public string NoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishDate
        {
            get;
            set;
        }
        /// <summary>
        /// 对应图片路径
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 公告简写
        /// </summary>
        public string NoticeShortContent
        {
            get;
            set;
        }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string NoticeContent
        {
            get;
            set;
        }
    }
}
