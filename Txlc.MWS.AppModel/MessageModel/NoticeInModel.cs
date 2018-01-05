using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.MessageModel
{
    [Serializable]
    public class NoticeInModel : BaseModel
    {
        public string NoticeId
        {
            get;
            set;
        }
    }
}
