using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.TraceModel
{
    [Serializable]
    public class BaseNodeModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>

        /// <summary>
        /// 企业ID
        /// </summary>
        public string CompId
        {
            get;
            set;
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompName
        {
            get;
            set;
        }
        /// <summary>
        /// 区划code
        /// </summary>
        public string AreaCode
        {
            get;
            set;
        }
    }
}
