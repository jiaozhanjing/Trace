namespace Trace.AppServices
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 杭州九捷科技有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.09 13:57
    /// 描 述:接收数据实体
    /// </summary>
    public class ReceiveModule<T> where T : class
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { set; get; }
        /// <summary>
        /// Post Request body内容
        /// </summary>
        public T data { set; get; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string DateTime { set; get; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string Random { set; get; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceId { set; get; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 回话ID
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// App版本信息
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// 渠道编号
        /// </summary>
        public string ChannelId { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; set; }


    }

}