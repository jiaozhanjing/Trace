namespace Trace.AppServices
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 杭州九捷科技有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.09 13:57
    /// 描 述:响应数据实体
    /// </summary>
    public class ResponseModule<T> where T : class
    {
        /// <summary>
        /// 标记
        /// </summary>
        public string code { set; get; }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string message { set; get; }
        /// <summary>
        /// 返回结果数据
        /// </summary>
        public T data { set; get; }
    }

    public class ResponseModule
    {
        /// <summary>
        /// 标记
        /// </summary>
        public string code { set; get; }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string message { set; get; }

        public object data { get; set; }
    }
}