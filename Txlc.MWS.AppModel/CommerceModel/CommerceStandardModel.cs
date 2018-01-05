using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.CommerceModel
{
    [Serializable]
    public class CommerceStandardModel
    {
        public string TotalStandard
        {
            get;
            set;
        }

        public string MeatStandard
        {
            get;
            set;
        }

        public string VegStandard
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
