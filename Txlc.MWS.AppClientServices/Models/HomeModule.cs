using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Responses.Negotiation;
using Txlc.MWS.Util;

namespace Txlc.MWS.AppServices.Models
{
    public class HomeModule : BaseModule
    {
        public HomeModule()
            : base("txlc/api")
        {
            Get["/test"] = aaa;

        }

        public Response aaa(dynamic _)
        {
            // return Negotiate.WithStatusCode(HttpStatusCode.OK).WithModel("adsfadsfasdf");
            //return "asdfasdfasdf";
            // byte[] bbase64 = Convert.FromBase64String("hello word 小糖豆");
            //System.Text.UTF8Encoding.UTF8.
            //var data = this.Request.Body;
            //this.Request.Headers["aaa"].ToArray()[0]
            //string straaa = this.Request.Headers["aaa"].ToString();
            //WCF_Device aa = new WCF_Device();
            //BJNL.MVS.Model.UserInfoStr bb = aa.PDAUserLogin2("3301012001790", "888888");
            //string cc = Json.ToJson(bb);
            //return cc;
            //return this.SendData<BJNL.MVS.Model.UserInfoStr>(bb, "123", "123", ResponseType.Success);
            //return Response.AsJson<UserInfoStr>(bb, HttpStatusCode.OK);
            //// string dd = this.SendData<BJNL.MVS.Model.UserInfoStr>(bb, "123123", "5555", ResponseType.Success);

            //// return dd;
            // ResponseModule<T> res = new ResponseModule<T>();
            // res.status = new Status();
            // res.userid = "666";
            // res.status.code = "";
            // res.status.desc = EnumAttribute.GetDescription(type);
            // res.result = obj;
            // res.token = token;
            // return Response.AsText("hello word 小糖豆", "text/html;charset=UTF-8");

            //  return resData;

            string aa = "测试接口！";
            return Response.AsText(aa, "text/html;charset=UTF-8");
        }
    }
}