using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.RetailModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class RetailBusiness
    {
        /// <summary>
        /// 农贸考核排名
        /// </summary>
        /// <param name="info"></param>
        /// <param name="PriceType"></param>
        /// <returns></returns>
        public List<RetailBaseModel> GetRetailCheckList(RetailInModel info)
        {
            List<RetailBaseModel> list = null;
            string strSql = string.Format(@"select id,      
       comp_id,
       comp_name,
       comp_score
  from bl_retail_score t where to_char(t.check_date,'yyyy-MM-dd')=:v_check_date order by t.comp_score desc ");

            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_check_date", info.CheckDay));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<RetailBaseModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RetailBaseModel temp = new RetailBaseModel();
                        temp.CompId = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();
                        temp.CompScore = DBNull.Value == dt.Rows[i]["comp_score"] ? string.Empty : dt.Rows[i]["comp_score"].ToString();

                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 农贸排名明细
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<RetailDetailModel> GetRetailCheckDetail(RetailInModel info)
        {
            List<RetailDetailModel> list = null;
            string strSql = string.Format(@"select id,      
                                              check_date,
                                               insert_time,
                                               update_time,
                                               base_score,
                                               check_score,
                                               query_machine_score,
                                               meat_in_score,
                                               veg_in_score,
                                               meat_print_score,
                                               veg_print_score,
                                               market_check_score,
                                               equ_score,
                                               veg_in_out_score,
                                               meat_in_out_score,
                                               veg_card_score,
                                               comp_id,
                                               comp_name,
                                               comp_score
  from bl_retail_score t where to_char(t.check_date,'yyyy-MM-dd')=:v_check_date and t.comp_id=:v_comp_id order by t.comp_score desc ");

            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_check_date", info.CheckDay), new OracleParameter(":v_comp_id", info.CompId));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<RetailDetailModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RetailDetailModel temp = new RetailDetailModel();
                        temp.BaseScore = DBNull.Value == dt.Rows[i]["base_score"] ? string.Empty : dt.Rows[i]["base_score"].ToString();
                        temp.CheckScore = DBNull.Value == dt.Rows[i]["check_score"] ? string.Empty : dt.Rows[i]["check_score"].ToString();
                        temp.EquScore = DBNull.Value == dt.Rows[i]["equ_score"] ? string.Empty : dt.Rows[i]["equ_score"].ToString();
                        temp.MarketCheckScore = DBNull.Value == dt.Rows[i]["market_check_score"] ? string.Empty : dt.Rows[i]["market_check_score"].ToString();
                        temp.MeatInOutScore = DBNull.Value == dt.Rows[i]["meat_in_out_score"] ? string.Empty : dt.Rows[i]["meat_in_out_score"].ToString();
                        temp.MeatInScore = DBNull.Value == dt.Rows[i]["meat_in_score"] ? string.Empty : dt.Rows[i]["meat_in_score"].ToString();
                        temp.MeatPrintScore = DBNull.Value == dt.Rows[i]["meat_print_score"] ? string.Empty : dt.Rows[i]["meat_print_score"].ToString();
                        temp.QueryMachineScore = DBNull.Value == dt.Rows[i]["query_machine_score"] ? string.Empty : dt.Rows[i]["query_machine_score"].ToString();
                        temp.VegCardScore = DBNull.Value == dt.Rows[i]["veg_card_score"] ? string.Empty : dt.Rows[i]["veg_card_score"].ToString();
                        temp.VegInOutScore = DBNull.Value == dt.Rows[i]["veg_in_out_score"] ? string.Empty : dt.Rows[i]["veg_in_out_score"].ToString();
                        temp.VegInScore = DBNull.Value == dt.Rows[i]["veg_in_score"] ? string.Empty : dt.Rows[i]["veg_in_score"].ToString();
                        temp.VegPrintScore = DBNull.Value == dt.Rows[i]["veg_print_score"] ? string.Empty : dt.Rows[i]["veg_print_score"].ToString();

                        list.Add(temp);
                    }
                }
            }

            return list;
        }
    }
}
