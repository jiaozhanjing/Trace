using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.BusinessModel
{
    [Serializable]
   public class BusinessCompSummaryModel
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

        public string BusinessCompNumbers
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
