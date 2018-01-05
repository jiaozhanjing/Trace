using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel
{
    public class BaseModel
    {

        /// <summary>
        /// 列表页面 页大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 列表页面 页码
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }
    }
}
