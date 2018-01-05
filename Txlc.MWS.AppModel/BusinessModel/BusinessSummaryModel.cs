using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.BusinessModel
{
    [Serializable]
    public class BusinessSummaryModel
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

        public string BusinessNumbers
        {
            get;
            set;
        }
    }
}
