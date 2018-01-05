using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.TraceModel
{
    public class EnterDetailInfoModel
    {
        /// <summary>
        /// 检测单号
        /// </summary>
        public string CheckNum
        { get; set; }
        /// <summary>
        /// 检测项编码 0：盐酸克罗多；1：亚硝酸盐；2：挥发性盐基氮
        /// </summary>
        public string CheckItemCode
        { get; set; }
        /// <summary>
        /// 检测项名称
        /// </summary>
        public string CheckItemName
        { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string CheckResult
        { get; set; }
        /// <summary>
        /// 采样名称 0：猪尿样，1 尿检
        /// </summary>
        public string SampleName
        { get; set; }
        /// <summary>
        /// 采样code
        /// </summary>
        public string SampleCode
        { get; set; }
        /// <summary>
        /// 采样数量
        /// </summary>
        public double SampleNum
        { get; set; }
        /// <summary>
        /// 检测人
        /// </summary>
        public string CheckOfficer
        { get; set; }
        /// <summary>
        /// 采样时间
        /// </summary>
        public string SampleTime
        { get; set; }
        /// <summary>
        /// 检测时间
        /// </summary>
        public string CheckTime
        { get; set; }

        /// <summary>
        /// 进场ID
        /// </summary>
        public string EnterId
        { get; set; }
        /// <summary>
        /// 检测ID
        /// </summary>
        public string Id
        { get; set; }

        public string CheckId
        {
            get;
            set;
        }
    }
}
