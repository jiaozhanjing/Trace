using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.NodeModel
{
    [Serializable]
    public class NodeCompSummaryModel
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

        public string NodeCompNumbers
        {
            get;
            set;
        }

        public string CompCode
        {
            get;
            set;
        }
        public string CompName
        {
            get;
            set;
        }
    }
}
