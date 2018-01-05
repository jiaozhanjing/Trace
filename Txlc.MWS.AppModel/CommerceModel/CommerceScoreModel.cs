using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.CommerceModel
{
    [Serializable]
    public class CommerceScoreModel
    {
        public string TotalScore
        {
            get;
            set;
        }

        public string CheckMonth
        {
            get;
            set;
        }

        /// <summary>
        /// 企业达标得分
        /// </summary>
        public string StandardScore
        {
            get;
            set;
        }
        /// <summary>
        /// 数据上报得分
        /// </summary>
        public string DataUpScore
        {
            get;
            set;
        }
       /// <summary>
        ///  日常管理得分
       /// </summary>
        public string DailyScore
        {
            get;
            set;
        }
    }
}
