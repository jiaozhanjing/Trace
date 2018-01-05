using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.TraceModel
{
    public class EnterBaseInfoModel
    {
        public string UserId
        { get; set; }

        public string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// 进场ID
        /// </summary>
        public string EnterId
        { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>
        public string EnterDate
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
        /// 动物检疫证号
        /// </summary>
        public string AnimalQuarantineId
        { get; set; }
        /// <summary>
        /// 检疫机构
        /// </summary>
        public string QuarantineOrganization
        { get; set; }
        /// <summary>
        /// 产品类型热鲜肉：10100；冷鲜肉：10200
        /// </summary>
        public string ProductType
        { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public double EnterWeight
        { get; set; }
        /// <summary>
        /// 头数
        /// </summary>
        public double HeadNum
        { get; set; }
        /// <summary>
        /// 签发日期
        /// </summary>
        public string IssueTime
        { get; set; }
        /// <summary>
        /// 有效天数
        /// </summary>
        public int ValidDays
        { get; set; }
        /// <summary>
        /// 检疫员
        /// </summary>
        public string QuarantineOfficer
        { get; set; }
        public List<EnterDetailInfoModel> EnterCheckInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 审批状态-1退回 0：未审核1：已审核
        /// </summary>
        public string status
        { get; set; }
        /// <summary>
        /// 肉品品质证号
        /// </summary>
        public string InstectionMeat
        {
            get;
            set;
        }
        /// <summary>
        /// 违禁药品检疫号
        /// </summary>
        public string Quarantine_Id
        {
            get;
            set;
        }
        /// <summary>
        /// 临时号
        /// </summary>
        public string AreaTempCode
        {
            get;
            set;
        }
        /// <summary>
        /// 企业id
        /// </summary>
        public string CompId
        { get; set; }

        /// <summary>
        /// 是否通过1001未通过；1002通过；
        /// </summary>
        public string IsPass
        {
            get;
            set;
        }

        public string OrganId
        {
            get;
            set;
        }

        public string OrganName
        {
            get;
            set;
        }
    }
}
