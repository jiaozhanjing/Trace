using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.DataUpModel
{
    public class DataUpGrowModel
    {
        public string CompTypeCode
        {
            get;
            set;
        }

        public string CompTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 环比
        /// </summary>
        public double Mom
        {
            get;
            set;
        }
        /// <summary>
        /// 数据量
        /// </summary>
        public string DataNumber
        {
            get;
            set;
        }
    }
}
