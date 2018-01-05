using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.TraceModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class SweepBusiness : BaseBusiness
    {
        /// <summary>
        /// 追溯信息
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public TraceModel GetTraceInfoByTraceCode(string strCode)
        {
            TraceModel info = null;

            string strSql = "PRO_APP_TRACE";
            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter("TraceCode", strCode));
            aslgList.Add(new OracleParameter("v_cur", System.Data.OracleClient.OracleType.Cursor) { Direction = ParameterDirection.Output });

            DataTable dt = OracleHelper.Query(CommandType.StoredProcedure, strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    info = new TraceModel();
                    info.Batch = DBNull.Value == dt.Rows[0]["Batch"] ? string.Empty : dt.Rows[0]["Batch"].ToString();
                    info.Buyer = DBNull.Value == dt.Rows[0]["Buyer"] ? string.Empty : dt.Rows[0]["Buyer"].ToString();
                    info.CheckNumber = DBNull.Value == dt.Rows[0]["CheckNumber"] ? string.Empty : dt.Rows[0]["CheckNumber"].ToString();
                    info.EnterTime = DBNull.Value == dt.Rows[0]["EnterTime"] ? string.Empty : dt.Rows[0]["EnterTime"].ToString();
                    info.OrderId = DBNull.Value == dt.Rows[0]["OrderId"] ? string.Empty : dt.Rows[0]["OrderId"].ToString();
                    info.PlateNumber = DBNull.Value == dt.Rows[0]["PlateNumber"] ? string.Empty : dt.Rows[0]["PlateNumber"].ToString();
                    info.Seller = DBNull.Value == dt.Rows[0]["Seller"] ? string.Empty : dt.Rows[0]["Seller"].ToString();
                    info.Supplier = DBNull.Value == dt.Rows[0]["Supplier"] ? string.Empty : dt.Rows[0]["Supplier"].ToString();
                    info.Testinger = DBNull.Value == dt.Rows[0]["Testinger"] ? string.Empty : dt.Rows[0]["Testinger"].ToString();
                    info.TestingNumber = DBNull.Value == dt.Rows[0]["TestingNumber"] ? string.Empty : dt.Rows[0]["TestingNumber"].ToString();
                    info.TestingResult = "合格";
                    info.TestingSamp = DBNull.Value == dt.Rows[0]["TestingSamp"] ? string.Empty : dt.Rows[0]["TestingSamp"].ToString();
                    info.TestingSampNum = DBNull.Value == dt.Rows[0]["TestingSampNum"] ? string.Empty : dt.Rows[0]["TestingSampNum"].ToString();
                    info.TestingTime = DBNull.Value == dt.Rows[0]["TestingTime"] ? string.Empty : dt.Rows[0]["TestingTime"].ToString();
                    info.TestingType = DBNull.Value == dt.Rows[0]["TestingType"] ? string.Empty : dt.Rows[0]["TestingType"].ToString();
                    info.TraceCode = DBNull.Value == dt.Rows[0]["TraceCode"] ? string.Empty : dt.Rows[0]["TraceCode"].ToString();
                    info.TraceWeight = DBNull.Value == dt.Rows[0]["TraceWeight"] ? string.Empty : dt.Rows[0]["TraceWeight"].ToString();
                    info.TradeTime = DBNull.Value == dt.Rows[0]["TradeTime"] ? string.Empty : dt.Rows[0]["TradeTime"].ToString();
                    info.TraceMoney = DBNull.Value == dt.Rows[0]["TraceMoney"] ? string.Empty : dt.Rows[0]["TraceMoney"].ToString();
                    info.CheckOrgan = DBNull.Value == dt.Rows[0]["CheckOrgan"] ? string.Empty : dt.Rows[0]["CheckOrgan"].ToString();
                }
            }
            return info;

        }
    }
}
