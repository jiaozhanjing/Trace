using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.CommerceModel
{
    /// <summary>
    /// 肉类蔬菜运行达标情况列表
    /// </summary>
    public class ComStandCompModel
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

        public string Score
        {
            get;
            set;
        }

        public string CheckMonth
        {
            get;
            set;
        }
    }
}
