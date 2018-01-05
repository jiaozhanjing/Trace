using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.TraceModel
{
    public class EnterCompModel
    {
        public string UserId
        { get; set; }        
        /// <summary>
        /// 区划编码
        /// </summary>
        public string AreaCode
        { get; set; }
        /// <summary>
        /// 区划名称
        /// </summary>
        public string AreaName
        { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompName
        { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber
        { get; set; }
        /// <summary>
        /// 检疫机构
        /// </summary>
        public string QuarantineOrganization
        { get; set; }
        /// <summary>
        /// 设置信息主键
        /// </summary>
        public string Id
        { get; set; }
        /// <summary>
        /// 进场设置 添加 企业ID
        /// </summary>
        public string CompId
        {
            get;
            set;
        }
    }
}
