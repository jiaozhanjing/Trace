using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trace.AppModel;
namespace Trace.AppBusiness
{
    public class BaseBusiness
    {
        public string GUID
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }
    }
}
