using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeaRun.Util.Log;
using Txlc.MWS.Util;
using Txlc.MWS.AppBusiness;

namespace Txlc.MWS.AppServices.Models
{
    public class OrderModule : BaseModule
    {
        public OrderModule()
            : base("txlc/api")
        {
            Post["/AddOrderInfo"] = AddOrderInfo;
            // Post["/AddPayInfo"] = AddPayInfo;
            Post["/GetOrderList"] = GetOrderList;

            Post["/GetOrderDetail"] = GetOrderDetail;

            Post["/CheckOrder"] = CheckOrder;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string CheckOrder(dynamic _)
        {
            var recdata = this.GetResquetData<PDAOrderBase>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("OrderModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAOrderBusiness bl = new PDAOrderBusiness();
                bool bo = bl.CheckOrder(recdata.data);
                if (bo)
                {
                    ResponseModule res = new ResponseModule();
                    res.code = "200";
                    res.message = "支付验证通过！";
                    string strJson = Json.ToJson(res);
                    return strJson;
                }
                else
                {
                    return this.WriteExceptionLog("OrderModule", recdata.data.userName, recdata.Mac, "支付验证异常:" + Json.ToJson(recdata), "支付验证异常");
                }
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("OrderModule", recdata.data.userName, recdata.Mac, "支付验证异常:[" + recdata.data.orderId + "]异常信息：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "支付验证异常:" + ex.Message);
            }
        }

        public string GetOrderDetail(dynamic _)
        {
            var recdata = this.GetResquetData<PDAOrderDetail>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("OrderModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAOrderBusiness bl = new PDAOrderBusiness();
                List<PDAOrderDetail> list = bl.GetOrderDetail(recdata.data.orderId);
                //WriteInfoLog("OrderModule", recdata.data.userName, recdata.Mac, "订单支付成功！订单号：" + recdata.data.buyerId);
                return this.SendData<List<PDAOrderDetail>>(list, "获取订单明细成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("OrderModule", recdata.data.userName, recdata.Mac, "获取订单明细异常:[" + recdata.data.orderId + "]异常信息：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取订单明细异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 支付订单
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public string AddOrderInfo(dynamic _)
        {
            var recdata = this.GetResquetData<PDAOrderBase>();

            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("OrderModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAOrderBusiness bl = new PDAOrderBusiness();
                PDAOrderBase info = bl.AddOrderInfo(recdata.data);
                WriteInfoLog("OrderModule", recdata.data.userName, recdata.Mac, "订单支付成功！订单号：" + recdata.data.orderId);

              //  WriteInfoLog("OrderModule", "aa", "bb", Json.ToJson(info));

                return this.SendData<PDAOrderBase>(info, "订单支付成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("OrderModule", recdata.data.userName, recdata.Mac, "订单支付异常:[" + recdata.data.orderId + "]异常信息：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "订单支付异常:" + ex.Message);


            }
        }

        //public string AddPayInfo(dynamic _)
        //{
        //    var log = LogFactory.GetLogger("LoginModule");
        //    LogMessage logMessage = new LogMessage();
        //    var recdata = this.GetResquetData<PDAPayInfo>();
        //    try
        //    {
        //        bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
        //        if (!flag)
        //        {
        //            ResponseModule res = new ResponseModule();
        //            res.code = "500";
        //            res.message = "密匙错误，接口不能被调用！";

        //            logMessage.OperationTime = DateTime.Now;
        //            logMessage.Url = this.Request.Url;
        //            logMessage.Class = this.ToString();
        //            logMessage.Host = this.Request.Headers.Host;
        //            logMessage.Ip = recdata.Mac;
        //            logMessage.Content = "密匙错误，接口不能被调用！";
        //            string strMessage = new LogFormat().InfoFormat(logMessage);
        //            log.Error(strMessage);

        //            return Json.ToJson(res);
        //        }
        //        PDAPayInfo payInfo = new PDAPayInfo();
        //        payInfo.buyerId = recdata.data.buyerId;
        //        payInfo.buyerName = recdata.data.buyerName;
        //        payInfo.icNum = recdata.data.icNum;
        //        payInfo.orderId = recdata.data.orderId;
        //        payInfo.totalMoney = recdata.data.totalMoney;
        //        payInfo.totalWeight = recdata.data.totalWeight;
        //        //    payInfo.transactionTime = recdata.data.transactionTime;
        //        payInfo.userId = recdata.data.userId;
        //        payInfo.userName = recdata.data.userName;

        //        logMessage.OperationTime = DateTime.Now;
        //        logMessage.UserName = recdata.data.userName;
        //        logMessage.Url = this.Request.Url;
        //        logMessage.Class = this.ToString();
        //        logMessage.Host = this.Request.Headers.Host;
        //        logMessage.Ip = recdata.Mac;
        //        logMessage.Content = "支付成功！";
        //        string strMessageInfo = new LogFormat().InfoFormat(logMessage);
        //        log.Info(strMessageInfo);

        //        return this.SendData<PDAPayInfo>(payInfo, "支付成功！", ResponseType.Success);

        //    }
        //    catch (Exception ex)
        //    {
        //        logMessage.OperationTime = DateTime.Now;
        //        if (recdata != null)
        //            logMessage.UserName = recdata.data.userName;
        //        logMessage.Url = this.Request.Url;
        //        logMessage.Class = this.ToString();
        //        logMessage.Host = this.Request.Headers.Host;
        //        if (recdata != null)
        //            logMessage.Ip = recdata.Mac;
        //        logMessage.Content = ex.Message;
        //        string strMessageInfo = new LogFormat().InfoFormat(logMessage);
        //        log.Error(strMessageInfo);

        //        ResponseModule res = new ResponseModule();
        //        res.code = "500";
        //        res.message = ex.Message;
        //        return Json.ToJson(res);
        //    }

        //}

        public string GetOrderList(dynamic _)
        {
            var recdata = this.GetResquetData<PDAOrderBase>();
            try
            {
                bool flag = DataValidation(recdata.Date, recdata.Random, recdata.Staffid);
                if (!flag)
                {
                    return this.WriteValidationLog("OrderModule", recdata.Mac);
                }
                //判断令牌是否过期
                bool flag2 = TokenValidation(recdata.SessionId, recdata.Token);
                if (!flag2)
                {
                    return this.SendTokenValidation();
                }

                PDAOrderBusiness bl = new PDAOrderBusiness();
                PDAOrderList info = bl.GetOrderList(recdata.data);
                //WriteInfoLog("OrderModule", recdata.data.userName, recdata.Mac, "获取订单成功！");

                return this.SendData<PDAOrderList>(info, "获取订单成功！", ResponseType.Success);
            }
            catch (Exception ex)
            {
                return this.WriteExceptionLog("OrderModule", recdata.data.userName, recdata.Mac, "获取订单异常：" + Json.ToJson(recdata) + "[异常信息:" + ex.Message + "]", "获取订单异常：" + ex.Message);
            }
        }
    }

}