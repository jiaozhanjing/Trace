using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.SweepModel
{
    [Serializable]
    public class SweepModel : BaseModel
    {
        public string QRconent
        {
            get;
            set;
        }
        public string TraceCode
        {
            get;
            set;
        }
    }
}
