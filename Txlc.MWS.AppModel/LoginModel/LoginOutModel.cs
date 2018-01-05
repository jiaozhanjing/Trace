using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.LoginModel
{
    [Serializable]
    public class LoginOutModel
    {
        /// <summary>
        /// 用户唯一ID（13位数字）
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// 所属企业名称
        /// </summary>
        public string CompName
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆成功返回令牌？
        /// </summary>
        public string Token
        {
            get;
            set;
        }
        /// <summary>
        /// Token对应的key，暂时不用UserId,是服务端生成的GUID
        /// </summary>
        public string SessionId
        {
            get;
            set;
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileCode
        {
            get;
            set;
        }
        /// <summary>
        /// 1001：批发，1002 零售
        /// </summary>
        public string UserType
        {
            get;
            set;
        }
        /// <summary>
        /// 状态0有效，1无效
        /// </summary>
        public Int32 State
        {
            get;
            set;
        }


        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganId
        {
            get;
            set;
        }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
    }
}
