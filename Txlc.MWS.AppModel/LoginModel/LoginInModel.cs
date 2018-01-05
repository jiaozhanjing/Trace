using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.LoginModel
{
    [Serializable]
    public class LoginInModel : BaseModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileCode
        {
            get;
            set;
        }
        /// <summary>
        /// 加密后的 密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LoginName
        {
            get;
            set;
        }
    }
}
