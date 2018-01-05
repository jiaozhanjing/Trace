using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.IO;
using Txlc.MWS.Util;
using Nancy.Responses.Negotiation;
using Nancy.Json;
using Txlc.Util.Attributes;
using LeaRun.Util.Log;
namespace Trace.AppServices
{
    public abstract class BaseModule : NancyModule
    {
        public const string SECRETKEY = "##txlc@2017##";
        public BaseModule()
            : base()
        {

        }
        public BaseModule(string modulePath)
            : base(modulePath)
        {

        }

        public Response GetFile(string strName)
        {
            return Response.AsFile(strName);
        }
        /// <summary>
        /// 获取提交数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModule<T>() where T : class
        {

            var data = this.Request.Body;
            if (data.Length > 0)
            {
                StreamReader reader = new StreamReader(data);
                string strbase64 = reader.ReadToEnd();
                byte[] bbase64 = Convert.FromBase64String(strbase64);
                string str = System.Text.Encoding.UTF8.GetString(bbase64);
                T obj = str.ToObject<T>();
                return obj;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ReceiveModule<T> GetResquetData<T>() where T : class
        {
            try
            {
                ReceiveModule<T> ret = new ReceiveModule<T>();
                string strSign = string.Empty;
                string strDate = string.Empty;
                string strDeviceId = string.Empty;
                string strRandom = string.Empty;
                string strAppVersion = string.Empty;
                string strToken = string.Empty;
                string strSessionId = string.Empty;
                string strChannelId = string.Empty;
                string strSystemType = string.Empty;

                var header = this.Request.Headers;
                //时间戳
                if (header["DateTime"] != null && header["DateTime"].ToArray().Count() > 0)
                    strDate = header["DateTime"].ToArray()[0];
                //mac地址
                if (header["DeviceId"] != null && header["DeviceId"].ToArray().Count() > 0)
                    strDeviceId = header["Mac"].ToArray()[0];
                //随机数
                if (header["Random"] != null && header["Random"].ToArray().Count() > 0)
                    strRandom = header["Random"].ToArray()[0];
                //签名
                if (header["Sign"] != null && header["Sign"].ToArray().Count() > 0)
                    strSign = header["Sign"].ToArray()[0];
                //令牌
                if (header["Token"] != null && header["Token"].ToArray().Count() > 0)
                    strToken = header["Token"].ToArray()[0];

                //令牌
                if (header["SessionId"] != null && header["SessionId"].ToArray().Count() > 0)
                    strSessionId = header["SessionId"].ToArray()[0];

                if (header["ChannelId"] != null && header["ChannelId"].ToArray().Count() > 0)
                    strChannelId = header["ChannelId"].ToArray()[0];

                if (header["AppVersion"] != null && header["AppVersion"].ToArray().Count() > 0)
                    strAppVersion = header["AppVersion"].ToArray()[0];

                if (header["SystemType"] != null && header["SystemType"].ToArray().Count() > 0)
                    strSystemType = header["SystemType"].ToArray()[0];

                ret.DateTime = strDate;
                ret.DeviceId = strDeviceId;
                ret.Random = strRandom;
                ret.Sign = strSign;
                ret.Token = strToken;
                ret.SessionId = strSessionId;
                ret.ChannelId = strChannelId;
                ret.AppVersion = strAppVersion;
                ret.SystemType = strSystemType;

                T obj;
                var data = this.Request.Body;

                //  WriteInfoLog("LoginModule", "asdfasdf", "asdsssss", data.ToString());

                if (data.Length > 0)
                {
                    StreamReader reader = new StreamReader(data, System.Text.Encoding.GetEncoding("utf-8"));
                    string strbase64 = reader.ReadToEnd();
                    //   WriteInfoLog("LoginModule", "5555555555555555", "55555555555", strbase64);
                    //byte[] bbase64 = Convert.FromBase64String(strbase64);
                    //string str = System.Text.Encoding.UTF8.GetString(bbase64);
                    obj = strbase64.ToObject<T>();
                    ret.data = obj;
                }
                #region "正式环境需要删掉"
                //var log = LogFactory.GetLogger("GetResquetData");
                //LogMessage logMessage = new LogMessage();
                //logMessage.OperationTime = DateTime.Now;
                //logMessage.Url = this.Request.Url;
                //logMessage.Class = this.ToString();
                //logMessage.Host = this.Request.Headers.Host;
                //logMessage.Ip = strDeviceId;
                //logMessage.Content = "请求接口";
                //string strMessage = new LogFormat().InfoFormat(logMessage);
                //log.Debug(strMessage);
                #endregion

                return ret;

            }
            catch (Exception ex)
            {
                WriteExceptionLog("LoginModule", "1111111", "111111111", "111111", ex.Message);
                throw ex;
            }


        }
        /// <summary>
        /// 验证接口信息是否合法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool DataValidation(string date, string random, string staffid)
        {
            if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(random) || string.IsNullOrEmpty(staffid))
            {
                return false;
            }
            else if (Md5Helper.MD5(SECRETKEY + date + random + SECRETKEY, 32) == staffid)
            {
                return true;
            }
            else
            {
                return false;
            }
            // return true;
        }
        /// <summary>
        /// 验证信息是否合法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TokenValidation(string sessionId, string token)
        {
            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(token))
            {
                return false;
            }
            string temp = this.ReadCache(sessionId);
            if (temp == null || temp != token)
            {
                return false;
            }

            return true;
        }

        #region 响应接口
        /// <summary>
        /// 响应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="userid"></param>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string SendData<T>(T obj, string message, ResponseType type) where T : class
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue;
            ResponseModule<T> res = new ResponseModule<T>();
            res.code = ((int)type).ToString();
            res.data = obj;
            res.message = message;
            string strJson = Json.ToJson(res);
            return strJson;


        }

        public string SendData(string message, ResponseType type)
        {
            ResponseModule res = new ResponseModule();
            res.code = ((int)type).ToString();
            res.message = message;
            res.data = null;
            string strJson = Json.ToJson(res);
            return strJson;
        }
        #endregion

        #region 接口调用日志
        /// <summary>
        /// 接口调用失败，记录日志
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public string WriteValidationLog(string modelName, string mac)
        {
            ResponseModule res = new ResponseModule();
            res.code = "500";
            res.message = "密匙错误，接口不能被调用！";
            var log = LogFactory.GetLogger(modelName);
            LogMessage logMessage = new LogMessage();
            logMessage.OperationTime = DateTime.Now;
            logMessage.Url = this.Request.Url;
            logMessage.Class = this.ToString();
            logMessage.Host = this.Request.Headers.Host;
            logMessage.Ip = mac;
            logMessage.Content = "密匙错误，接口不能被调用！";
            string strMessage = new LogFormat().InfoFormat(logMessage);
            log.Error(strMessage);

            return Json.ToJson(res);
        }
        /// <summary>
        /// 令牌过期
        /// </summary>
        /// <returns></returns>
        public string SendTokenValidation()
        {
            ResponseModule res = new ResponseModule();
            res.code = "400";
            res.message = "登陆超时，请重新登陆！";


            return Json.ToJson(res);
        }
        public string WriteExceptionLog(string modelName, string userName, string mac, string message, string message2)
        {
            var log = LogFactory.GetLogger(modelName);
            LogMessage logMessage = new LogMessage();
            logMessage.OperationTime = DateTime.Now;
            if (!string.IsNullOrEmpty(userName))
                logMessage.UserName = userName;
            logMessage.Url = this.Request.Url;
            logMessage.Class = this.ToString();
            logMessage.Host = this.Request.Headers.Host;
            if (!string.IsNullOrEmpty(mac))
                logMessage.Ip = mac;
            logMessage.Content = message;
            string strMessageInfo = new LogFormat().InfoFormat(logMessage);
            log.Error(strMessageInfo);

            ResponseModule res = new ResponseModule();
            res.code = "500";
            res.message = message2;
            return Json.ToJson(res);
        }

        public void WriteInfoLog(string modelName, string userName, string mac, string message)
        {
            var log = LogFactory.GetLogger(modelName);
            LogMessage logMessage = new LogMessage();
            logMessage.OperationTime = DateTime.Now;
            logMessage.UserName = userName;
            logMessage.Url = this.Request.Url;
            logMessage.Class = this.ToString();
            logMessage.Host = this.Request.Headers.Host;
            logMessage.Ip = mac;
            logMessage.Content = message;
            string strMessageInfo = new LogFormat().InfoFormat(logMessage);
            log.Info(strMessageInfo);
        }
        #endregion

        #region "时间处理"


        #endregion

        /// <summary>
        /// 写入缓存，20分钟过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="cacheKey"></param>
        public void WriteCache(string value, string cacheKey, int tokentime)
        {
            CacheFactory.Cache().WriteCache(value, cacheKey, DateTime.Now.AddMinutes(tokentime));
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public string ReadCache(string cacheKey)
        {
            return CacheFactory.Cache().GetCache(cacheKey);
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public void RomveCache(string cacheKey)
        {
            CacheFactory.Cache().RemoveCache(cacheKey);
        }

    }
}