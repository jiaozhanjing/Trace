using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.DataUpModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class DataUpBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public DataUpAllInfoModel GetDataUpAllInfo(DataUpInModel info)
        {
            DataUpAllInfoModel temp = null;
            string strSql = string.Format(@" select id,
                                               total_number,
                                               dataup_score,
                                               quality_score,
                                               dataup_standard_score,
                                               data_complete_score,
                                               data_price_score,
                                               data_weight_score,
                                               meat_trace_score,
                                               veg_trace_score,
                                               check_month,
                                               check_year,
                                               check_date,
                                               insert_time,DATA_GROW_SCORE,
                                               update_time
                                          from bl_data_up_info t where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    temp = new DataUpAllInfoModel();

                    temp.DataCompleteScore = DBNull.Value == dt.Rows[0]["data_complete_score"] ? string.Empty : dt.Rows[0]["data_complete_score"].ToString();
                    temp.DataPriceScore = DBNull.Value == dt.Rows[0]["data_price_score"] ? string.Empty : dt.Rows[0]["data_price_score"].ToString();
                    temp.DataUpGrowScore = DBNull.Value == dt.Rows[0]["DATA_GROW_SCORE"] ? string.Empty : dt.Rows[0]["DATA_GROW_SCORE"].ToString();
                    temp.DataUpScore = DBNull.Value == dt.Rows[0]["dataup_score"] ? string.Empty : dt.Rows[0]["dataup_score"].ToString();
                    temp.DataUpStandardScore = DBNull.Value == dt.Rows[0]["dataup_standard_score"] ? string.Empty : dt.Rows[0]["dataup_standard_score"].ToString();
                    temp.DataWeightScore = DBNull.Value == dt.Rows[0]["data_weight_score"] ? string.Empty : dt.Rows[0]["data_weight_score"].ToString();
                    temp.MeatTraceScore = DBNull.Value == dt.Rows[0]["meat_trace_score"] ? string.Empty : dt.Rows[0]["meat_trace_score"].ToString();
                    temp.QualityScore = DBNull.Value == dt.Rows[0]["quality_score"] ? string.Empty : dt.Rows[0]["quality_score"].ToString();
                    temp.TotalNumber = DBNull.Value == dt.Rows[0]["total_number"] ? string.Empty : dt.Rows[0]["total_number"].ToString();
                    temp.VegTraceScore = DBNull.Value == dt.Rows[0]["veg_trace_score"] ? string.Empty : dt.Rows[0]["veg_trace_score"].ToString();


                }
            }

            return temp;
        }

        /// <summary>
        /// 数据上报达标情况列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpStandardListModel> GetDataUpStandardList(DataUpInModel info)
        {
            List<DataUpStandardListModel> list = null;
            string strSql = string.Format(@"  select id,
                       comp_type_code,
                       comp_type_name,
                       stardard_number,
                       data_number,
                       check_date,
                       insert_time,
                       update_time,
                       check_month,
                       check_year
                  from bl_data_up_comp_standard
                  where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpStandardListModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpStandardListModel temp = new DataUpStandardListModel();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["comp_type_code"] ? string.Empty : dt.Rows[i]["comp_type_code"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["comp_type_name"] ? string.Empty : dt.Rows[i]["comp_type_name"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[i]["data_number"] ? string.Empty : dt.Rows[i]["data_number"].ToString();
                        temp.StardardNumber = DBNull.Value == dt.Rows[i]["stardard_number"] ? string.Empty : dt.Rows[i]["stardard_number"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 数据上传屠宰场达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpCompListModel> GetDataUpAnimal(DataUpInModel info)
        {
            List<DataUpCompListModel> list = null;
            string strSql = string.Format(@" select id,
                                               comp_id,
                                               comp_name,
                                               check_month,
                                               check_year,
                                               check_date,
                                               insert_time,
                                               is_standard,
                                               update_time,
                                               data_number
                                          from bl_data_up_animal_standard
                                           where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpCompListModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpCompListModel temp = new DataUpCompListModel();
                        temp.CompId = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[i]["data_number"] ? string.Empty : dt.Rows[i]["data_number"].ToString();
                        temp.IsStardard = DBNull.Value == dt.Rows[i]["is_standard"] ? string.Empty : dt.Rows[i]["is_standard"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 批发市场
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpCompListModel> GetDataUpMarket(DataUpInModel info)
        {
            List<DataUpCompListModel> list = null;
            string strSql = string.Format(@" select id,
                                               comp_id,
                                               comp_name,
                                               check_month,
                                               check_year,
                                               check_date,
                                               insert_time,
                                               is_standard,
                                               update_time,
                                               data_number
                                          from bl_data_up_market_standard
                                           where CHECK_MONTH=:V_CHECK_MONTH ");
            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and comp_name like '%{0}%'", info.CompName);
            }

            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and is_standard={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpCompListModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpCompListModel temp = new DataUpCompListModel();
                        temp.CompId = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[i]["data_number"] ? string.Empty : dt.Rows[i]["data_number"].ToString();
                        temp.IsStardard = DBNull.Value == dt.Rows[i]["is_standard"] ? string.Empty : dt.Rows[i]["is_standard"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        ///菜市场
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpCompListModel> GetDataUpRetail(DataUpInModel info)
        {
            List<DataUpCompListModel> list = null;
            string strSql = string.Format(@" select id,
                                               comp_id,
                                               comp_name,
                                               check_month,
                                               check_year,
                                               check_date,
                                               insert_time,
                                               is_standard,
                                               update_time,
                                               data_number
                                          from bl_data_up_retail_standard
                                           where CHECK_MONTH=:V_CHECK_MONTH ");
            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and comp_name like '%{0}%'", info.CompName);
            }

            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and is_standard={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpCompListModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpCompListModel temp = new DataUpCompListModel();
                        temp.CompId = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[i]["data_number"] ? string.Empty : dt.Rows[i]["data_number"].ToString();
                        temp.IsStardard = DBNull.Value == dt.Rows[i]["is_standard"] ? string.Empty : dt.Rows[i]["is_standard"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 超市
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpCompListModel> GetDataUpSuper(DataUpInModel info)
        {
            List<DataUpCompListModel> list = null;
            string strSql = string.Format(@" select id,
                                               comp_id,
                                               comp_name,
                                               check_month,
                                               check_year,
                                               check_date,
                                               insert_time,
                                               is_standard,
                                               update_time,
                                               data_number
                                          from bl_data_up_super_standard
                                           where CHECK_MONTH=:V_CHECK_MONTH ");
            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and comp_name like '%{0}%'", info.CompName);
            }

            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and is_standard={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpCompListModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpCompListModel temp = new DataUpCompListModel();
                        temp.CompId = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[i]["data_number"] ? string.Empty : dt.Rows[i]["data_number"].ToString();
                        temp.IsStardard = DBNull.Value == dt.Rows[i]["is_standard"] ? string.Empty : dt.Rows[i]["is_standard"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 团体
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpCompListModel> GetDataUpTeam(DataUpInModel info)
        {
            List<DataUpCompListModel> list = null;
            string strSql = string.Format(@" select id,
                                               comp_id,
                                               comp_name,
                                               check_month,
                                               check_year,
                                               check_date,
                                               insert_time,
                                               is_standard,
                                               update_time,
                                               data_number
                                          from bl_data_up_team_standard
                                           where CHECK_MONTH=:V_CHECK_MONTH ");
            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and comp_name like '%{0}%'", info.CompName);
            }

            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and is_standard={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpCompListModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpCompListModel temp = new DataUpCompListModel();
                        temp.CompId = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[i]["data_number"] ? string.Empty : dt.Rows[i]["data_number"].ToString();
                        temp.IsStardard = DBNull.Value == dt.Rows[i]["is_standard"] ? string.Empty : dt.Rows[i]["is_standard"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 数据增长
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpGrowModel> GetDataUpGrow(DataUpInModel info)
        {
            List<DataUpGrowModel> list = null;
            string strSql = string.Format(@" select id,
                                                   comp_type_code,
                                                   comp_type_name,
                                                   mom_mom,
                                                   data_number,
                                                   insert_time,
                                                   update_time,
                                                   check_date,
                                                   check_month,
                                                   check_year
                                              from bl_data_up_grow
                                           where CHECK_MONTH=:V_CHECK_MONTH ");



            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpGrowModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpGrowModel temp = new DataUpGrowModel();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["comp_type_code"] ? string.Empty : dt.Rows[i]["comp_type_code"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["comp_type_name"] ? string.Empty : dt.Rows[i]["comp_type_name"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[i]["data_number"] ? string.Empty : dt.Rows[i]["data_number"].ToString();
                        temp.Mom = DBNull.Value == dt.Rows[i]["mom_mom"] ? 0 : Convert.ToDouble(dt.Rows[i]["mom_mom"]);
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 数据质量得分列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpQualityModel> GetDataUpQualityList(DataUpInModel info)
        {
            List<DataUpQualityModel> list = null;
            string strSql = string.Format(@" select id,
                                               complete_score,
                                               price_score,
                                               weight_score,
                                               meat_trace_score,
                                               vet_trace_score,
                                               insert_time,
                                               update_time,
                                               check_month,
                                               check_year,
                                               check_date
                                          from bl_data_up_quality  where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpQualityModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpQualityModel temp = new DataUpQualityModel();
                        temp.CompleteScore = DBNull.Value == dt.Rows[i]["complete_score"] ? string.Empty : dt.Rows[i]["complete_score"].ToString();
                        temp.MeatTraceScore = DBNull.Value == dt.Rows[i]["meat_trace_score"] ? string.Empty : dt.Rows[i]["meat_trace_score"].ToString();
                        temp.PriceScore = DBNull.Value == dt.Rows[i]["price_score"] ? string.Empty : dt.Rows[i]["price_score"].ToString();
                        temp.VegTraceScore = DBNull.Value == dt.Rows[i]["vet_trace_score"] ? string.Empty : dt.Rows[i]["vet_trace_score"].ToString();
                        temp.WeightScore = DBNull.Value == dt.Rows[i]["weight_score"] ? string.Empty : dt.Rows[i]["weight_score"].ToString();

                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 肉列链条合成
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpComposeModel> GetDataUpMeatQuality(DataUpInModel info)
        {
            List<DataUpComposeModel> list = null;
            string strSql = string.Format(@" select id,
       total_number,
       success_number,
       check_month,
       check_year,
       check_date,
       insert_time,
       update_time
  from bl_data_up_meat where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpComposeModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpComposeModel temp = new DataUpComposeModel();
                        temp.TotalNumber = DBNull.Value == dt.Rows[i]["total_number"] ? string.Empty : dt.Rows[i]["total_number"].ToString();
                        temp.SuccessNumber = DBNull.Value == dt.Rows[i]["success_number"] ? string.Empty : dt.Rows[i]["success_number"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 肉列链条合成明细
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpComposeDetailModel> GetDataUpMeatQualityDetail(DataUpInModel info)
        {
            List<DataUpComposeDetailModel> list = null;
            string strSql = string.Format(@"select id,
       trace_code,
       is_success,
       check_month,
       check_year,
       check_date,
       insert_time,
       update_time
  from bl_data_up_meat_detail where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpComposeDetailModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpComposeDetailModel temp = new DataUpComposeDetailModel();
                        temp.IsSuccess = DBNull.Value == dt.Rows[i]["is_success"] ? string.Empty : dt.Rows[i]["is_success"].ToString();
                        temp.TraceCode = DBNull.Value == dt.Rows[i]["trace_code"] ? string.Empty : dt.Rows[i]["trace_code"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 肉列链条合成
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DataUpComposeModel> GetDataUpVegQuality(DataUpInModel info)
        {
            List<DataUpComposeModel> list = null;
            string strSql = string.Format(@" select id,
       total_number,
       success_number,
       check_month,
       check_year,
       check_date,
       insert_time,
       update_time
  from bl_data_up_veg where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpComposeModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpComposeModel temp = new DataUpComposeModel();
                        temp.TotalNumber = DBNull.Value == dt.Rows[i]["total_number"] ? string.Empty : dt.Rows[i]["total_number"].ToString();
                        temp.SuccessNumber = DBNull.Value == dt.Rows[i]["success_number"] ? string.Empty : dt.Rows[i]["success_number"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        public List<DataUpComposeDetailModel> GetDataUpVegQualityDetail(DataUpInModel info)
        {
            List<DataUpComposeDetailModel> list = null;
            string strSql = string.Format(@"select id,
       trace_code,
       is_success,
       check_month,
       check_year,
       check_date,
       insert_time,
       update_time
  from bl_data_up_veg_detail where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<DataUpComposeDetailModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataUpComposeDetailModel temp = new DataUpComposeDetailModel();
                        temp.IsSuccess = DBNull.Value == dt.Rows[i]["is_success"] ? string.Empty : dt.Rows[i]["is_success"].ToString();
                        temp.TraceCode = DBNull.Value == dt.Rows[i]["trace_code"] ? string.Empty : dt.Rows[i]["trace_code"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
    }
}
