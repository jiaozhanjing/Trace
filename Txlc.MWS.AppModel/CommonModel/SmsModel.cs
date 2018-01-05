using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trace.AppModel.CommonModel
{
    [Serializable]
    public class SmsModel : BaseModel
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
        /// 验证码：6位数字，5分钟失效
        /// </summary>
        public string VerificationCode
        {
            get;
            set;
        }
        /// <summary>
        /// 1001：登陆验证码，1002：重置密码。其他待定。
        /// </summary>
        public string CodeType
        {
            get;
            set;
        }
        /// <summary>
        /// 短信ID
        /// </summary>
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// 0未使用1已使用
        /// </summary>
        public Int32 IsUser
        {
            get;
            set;
        }
    }
}
