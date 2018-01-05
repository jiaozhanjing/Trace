using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.NodeModel
{
    [Serializable]
    public class NodelInModel : BaseModel
    {
        /// <summary>
        /// 企业类型编码
        /// </summary>
        public string CompTypeCode
        {
            get;
            set;
        }
        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompCode
        {
            get;
            set;
        }
    }
}
