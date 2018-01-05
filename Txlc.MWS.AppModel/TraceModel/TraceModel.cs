using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.TraceModel
{
    [Serializable]
    public class TraceModel
    {
        /// <summary>
        /// 进场时间
        /// </summary>
        public string EnterTime
        {
            get;
            set;
        }
        /// <summary>
        /// 货主
        /// </summary>
        public string Seller
        {
            get;
            set;
        }
        /// <summary>
        /// 屠宰企业、供应商
        /// </summary>
        public string Supplier
        {
            get;
            set;
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string Batch
        {
            get;
            set;
        }
        /// <summary>
        /// 动物检疫证号
        /// </summary>
        public string CheckNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 检测日期
        /// </summary>
        public string TestingTime
        {
            get;
            set;
        }
        /// <summary>
        /// 检测单号
        /// </summary>
        public string TestingNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string TestingType
        {
            get;
            set;
        }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string TestingResult
        {
            get;
            set;
        }
        /// <summary>
        /// 采样名称
        /// </summary>
        public string TestingSamp
        {
            get;
            set;
        }
        /// <summary>
        /// 采样数量
        /// </summary>
        public string TestingSampNum
        {
            get;
            set;
        }
        /// <summary>
        /// 检测员
        /// </summary>
        public string Testinger
        {
            get;
            set;
        }
        /// <summary>
        ///    交易时间
        /// </summary>
        public string TradeTime
        {
            get;
            set;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId
        {
            get;
            set;
        }
        /// <summary>
        /// 买家
        /// </summary>
        public string Buyer
        {
            get;
            set;
        }
        /// <summary>
        /// 追溯码
        /// </summary>
        public string TraceCode
        {
            get;
            set;
        }
        /// <summary>
        /// 重量
        /// </summary>
        public string TraceWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 金额
        /// </summary>
        public string TraceMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 检疫机构
        /// </summary>
        public string CheckOrgan
        {
            get;
            set;
        }
    }
}
