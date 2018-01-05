using System.ComponentModel;

namespace Trace.AppServices
{
    /// <summary>
    /// 响应类型
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 200,
        /// <summary>
        /// 访问
        /// </summary>
        [Description("用户已离线")]
        Leave = 1,
        /// <summary>
        /// 离开
        /// </summary>
        [Description("请求失败")]
        Fail = 500
    }
}
