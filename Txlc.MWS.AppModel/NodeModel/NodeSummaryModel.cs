using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.NodeModel
{
    [Serializable]
   public class NodeSummaryModel
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

       public string NodeNumbers
       {
           get;
           set;
       }

    }
}
