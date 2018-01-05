using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.CommerceModel
{
    [Serializable]
    public class CommerceInModel : BaseModel
    {
        public string CheckMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckYear
        {
            get;
            set;
        }

        public string GoodsType
        {
            get;
            set;
        }

        public string CompName
        {
            get;
            set;
        }

        public string IsStandard
        {
            get;
            set;
        }
    }
}
