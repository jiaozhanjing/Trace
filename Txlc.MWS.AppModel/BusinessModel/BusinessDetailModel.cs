using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.BusinessModel
{
    [Serializable]
    public class BusinessDetailModel
    {
        public string BoothNumber
        {
            get;
            set;
        }

        public string LegalPerson
        {
            get;
            set;
        }

        public string License
        {
            get;
            set;
        }

        public string Telphone
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
