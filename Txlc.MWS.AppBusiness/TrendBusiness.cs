using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.TrendModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class TrendBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="PriceType"></param>
        /// <returns></returns>
        public List<TrendOutModel> GetAvgPrice(TrendInModel info, string PriceType)
        {
            List<TrendOutModel> list = null;
            string strSql = string.Format(@"select t.id,t.check_date,t.avg_price from bl_trend_market_price t 
where t.price_type=:v_price_type and t.check_date between :v_start_time and :v_end_time");

            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_start_time", Convert.ToDateTime(info.StartTime + " 00:00:00")), new OracleParameter(":v_end_time", Convert.ToDateTime(info.EndTime + " 23:59:59")), new OracleParameter(":v_price_type", PriceType));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<TrendOutModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TrendOutModel temp = new TrendOutModel();
                        temp.CheckDay = DBNull.Value == dt.Rows[i]["check_date"] ? string.Empty :Convert.ToDateTime(dt.Rows[i]["check_date"]).ToString("yyyy-MM-dd");
                        temp.Price = DBNull.Value == dt.Rows[i]["avg_price"] ? string.Empty : dt.Rows[i]["avg_price"].ToString();

                        list.Add(temp);
                    }
                }
            }

            return list;
        }
    }
}
