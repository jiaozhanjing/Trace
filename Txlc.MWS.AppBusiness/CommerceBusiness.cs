using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.CommerceModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class CommerceBusiness : BaseBusiness
    {
        /// <summary>
        /// 首页总得分
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<CommerceScoreModel> GetCommerceScore(CommerceInModel info)
        {
            List<CommerceScoreModel> list = null;
            string strSql = string.Format(@" select ID,CHECK_DATE,TOTAL_SCORE,CHECK_MONTH,CHECK_YEAR from BL_COMMERCE_SCORE t where CHECK_YEAR=:V_CHECK_YEAR order by CHECK_MONTH desc ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_YEAR", info.CheckYear));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<CommerceScoreModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CommerceScoreModel temp = new CommerceScoreModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.TotalScore = DBNull.Value == dt.Rows[i]["TOTAL_SCORE"] ? string.Empty : dt.Rows[i]["TOTAL_SCORE"].ToString();

                        list.Add(temp);
                    }
                }
            }

            return list;

        }
        /// <summary>
        /// 首页达标率和 运行达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommerceStandardModel GetCommerceStandard(CommerceInModel info)
        {
            CommerceStandardModel retInfo = null;

            string strSql = string.Format(@" select id,
                                               total_standard,
                                               meat_standard,
                                               veg_standard,
                                               check_date,
                                               check_month,
                                               check_year,
                                               insert_time,
                                               update_time
                                          from bl_commerce_standard where check_month=:v_check_date ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_check_date", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    retInfo = new CommerceStandardModel();
                    retInfo.CheckMonth = info.CheckMonth;
                    retInfo.MeatStandard = DBNull.Value == dt.Rows[0]["meat_standard"] ? string.Empty : dt.Rows[0]["meat_standard"].ToString();
                    retInfo.TotalStandard = DBNull.Value == dt.Rows[0]["total_standard"] ? string.Empty : dt.Rows[0]["total_standard"].ToString();
                    retInfo.VegStandard = DBNull.Value == dt.Rows[0]["veg_standard"] ? string.Empty : dt.Rows[0]["veg_standard"].ToString();
                }
            }
            return retInfo;

        }
        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<CommerceDataUpModel> GetCommerceDataUp(CommerceInModel info)
        {
            List<CommerceDataUpModel> list = null;

            string strSql = string.Format(@" select id, check_month, data_number, check_date, insert_time, update_time from bl_commerce_data_up where check_month=:v_check_month ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_check_month", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<CommerceDataUpModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CommerceDataUpModel temp = new CommerceDataUpModel();
                        temp.CheckDay = DBNull.Value == dt.Rows[0]["check_date"] ? string.Empty : dt.Rows[0]["check_date"].ToString();
                        temp.DataNumber = DBNull.Value == dt.Rows[0]["data_number"] ? string.Empty : dt.Rows[0]["data_number"].ToString();
                        list.Add(temp);
                    }

                }
            }
            return list;

        }
        /// <summary>
        /// 商户考核总体情况，得分
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<CommerceScoreModel> GetCommerceAllInfo(CommerceInModel info)
        {
            List<CommerceScoreModel> list = null;
            string strSql = string.Format(@" select ID,CHECK_DATE,TOTAL_SCORE,CHECK_MONTH,CHECK_YEAR,OPERATION_SCORE,DATA_UP_SCORE,STANDARD_SCORE from BL_COMMERCE_SCORE t where CHECK_MONTH=:V_CHECK_MONTH order by CHECK_MONTH desc ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<CommerceScoreModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CommerceScoreModel temp = new CommerceScoreModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.TotalScore = DBNull.Value == dt.Rows[i]["TOTAL_SCORE"] ? string.Empty : dt.Rows[i]["TOTAL_SCORE"].ToString();
                        temp.DailyScore = DBNull.Value == dt.Rows[i]["OPERATION_SCORE"] ? string.Empty : dt.Rows[i]["OPERATION_SCORE"].ToString();
                        temp.DataUpScore = DBNull.Value == dt.Rows[i]["DATA_UP_SCORE"] ? string.Empty : dt.Rows[i]["DATA_UP_SCORE"].ToString();
                        temp.StandardScore = DBNull.Value == dt.Rows[i]["STANDARD_SCORE"] ? string.Empty : dt.Rows[i]["STANDARD_SCORE"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 肉菜运行达标情况列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<ComStandCompModel> GetStandardCompList(CommerceInModel info)
        {
            List<ComStandCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_TYPE_CODE,COMP_TYPE_NAME,SCORE,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,GOODS_TYPE from BL_MEAT_STANDARD_COMP t where CHECK_MONTH=:V_CHECK_MONTH and goods_type=:v_goods_type order by COMP_TYPE_CODE");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth), new OracleParameter(":v_goods_type", info.GoodsType));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<ComStandCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ComStandCompModel temp = new ComStandCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["COMP_TYPE_CODE"] ? string.Empty : dt.Rows[i]["COMP_TYPE_CODE"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["COMP_TYPE_NAME"] ? string.Empty : dt.Rows[i]["COMP_TYPE_NAME"].ToString();
                        temp.Score = DBNull.Value == dt.Rows[i]["SCORE"] ? string.Empty : dt.Rows[i]["SCORE"].ToString();

                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 屠宰场运行达标情况详细
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<StandardCompModel> GetStandardAnimalList(CommerceInModel info)
        {
            List<StandardCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_ID,COMP_NAME,CHECK_YEAR,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,IS_STANDARD from BL_ANIMAL_STAND t where CHECK_MONTH=:V_CHECK_MONTH ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<StandardCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StandardCompModel temp = new StandardCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompId = DBNull.Value == dt.Rows[i]["COMP_ID"] ? string.Empty : dt.Rows[i]["COMP_ID"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["COMP_NAME"] ? string.Empty : dt.Rows[i]["COMP_NAME"].ToString();
                        temp.IsStandard = DBNull.Value == dt.Rows[i]["IS_STANDARD"] ? string.Empty : dt.Rows[i]["IS_STANDARD"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 批发市场运行达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<StandardCompModel> GetStandardMarketList(CommerceInModel info)
        {
            List<StandardCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_ID,COMP_NAME,CHECK_YEAR,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,IS_STANDARD,GOODS_TYPE from BL_MARKET_STANDARD t where CHECK_MONTH=:V_CHECK_MONTH and GOODS_TYPE=:v_goods_type");

            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and COMP_NAME like '%{0}%'", info.CompName);
            }
            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and IS_STANDARD={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            aslgList.Add(new OracleParameter(":v_goods_type", info.GoodsType));

            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<StandardCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StandardCompModel temp = new StandardCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompId = DBNull.Value == dt.Rows[i]["COMP_ID"] ? string.Empty : dt.Rows[i]["COMP_ID"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["COMP_NAME"] ? string.Empty : dt.Rows[i]["COMP_NAME"].ToString();
                        temp.IsStandard = DBNull.Value == dt.Rows[i]["IS_STANDARD"] ? string.Empty : dt.Rows[i]["IS_STANDARD"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 批发市场经营户运行达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<StandardCompModel> GetStandardMarketBusList(CommerceInModel info)
        {
            List<StandardCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_ID,COMP_NAME,CHECK_YEAR,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,IS_STANDARD,GOODS_TYPE,BUS_ID,BUS_NAME from BL_MARKET_BUS_STANDARD t where CHECK_MONTH=:V_CHECK_MONTH and GOODS_TYPE=:v_goods_type");

            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and COMP_NAME like '%{0}%'", info.CompName);
            }
            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and IS_STANDARD={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            aslgList.Add(new OracleParameter(":v_goods_type", info.GoodsType));

            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<StandardCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StandardCompModel temp = new StandardCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompId = DBNull.Value == dt.Rows[i]["COMP_ID"] ? string.Empty : dt.Rows[i]["COMP_ID"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["COMP_NAME"] ? string.Empty : dt.Rows[i]["COMP_NAME"].ToString();
                        temp.IsStandard = DBNull.Value == dt.Rows[i]["IS_STANDARD"] ? string.Empty : dt.Rows[i]["IS_STANDARD"].ToString();
                        temp.BusId = DBNull.Value == dt.Rows[i]["BUS_ID"] ? string.Empty : dt.Rows[i]["BUS_ID"].ToString();
                        temp.BusName = DBNull.Value == dt.Rows[i]["BUS_NAME"] ? string.Empty : dt.Rows[i]["BUS_NAME"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 菜市场运行达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<StandardCompModel> GetStandardRetailList(CommerceInModel info)
        {
            List<StandardCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_ID,COMP_NAME,CHECK_YEAR,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,IS_STANDARD,GOODS_TYPE from BL_REATAIL_STANDARD t where CHECK_MONTH=:V_CHECK_MONTH and GOODS_TYPE=:v_goods_type");

            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and COMP_NAME like '%{0}%'", info.CompName);
            }
            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and IS_STANDARD={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            aslgList.Add(new OracleParameter(":v_goods_type", info.GoodsType));

            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<StandardCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StandardCompModel temp = new StandardCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompId = DBNull.Value == dt.Rows[i]["COMP_ID"] ? string.Empty : dt.Rows[i]["COMP_ID"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["COMP_NAME"] ? string.Empty : dt.Rows[i]["COMP_NAME"].ToString();
                        temp.IsStandard = DBNull.Value == dt.Rows[i]["IS_STANDARD"] ? string.Empty : dt.Rows[i]["IS_STANDARD"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 菜市场经营户运行达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<StandardCompModel> GetStandardRetailBusList(CommerceInModel info)
        {
            List<StandardCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_ID,COMP_NAME,CHECK_YEAR,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,IS_STANDARD,GOODS_TYPE,BUS_ID,BUS_NAME from BL_RETAIL_BUS_STANDARD t where CHECK_MONTH=:V_CHECK_MONTH and GOODS_TYPE=:v_goods_type");

            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and COMP_NAME like '%{0}%'", info.CompName);
            }
            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and IS_STANDARD={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            aslgList.Add(new OracleParameter(":v_goods_type", info.GoodsType));

            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<StandardCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StandardCompModel temp = new StandardCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompId = DBNull.Value == dt.Rows[i]["COMP_ID"] ? string.Empty : dt.Rows[i]["COMP_ID"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["COMP_NAME"] ? string.Empty : dt.Rows[i]["COMP_NAME"].ToString();
                        temp.IsStandard = DBNull.Value == dt.Rows[i]["IS_STANDARD"] ? string.Empty : dt.Rows[i]["IS_STANDARD"].ToString();
                        temp.BusId = DBNull.Value == dt.Rows[i]["BUS_ID"] ? string.Empty : dt.Rows[i]["BUS_ID"].ToString();
                        temp.BusName = DBNull.Value == dt.Rows[i]["BUS_NAME"] ? string.Empty : dt.Rows[i]["BUS_NAME"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 超市运行达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<StandardCompModel> GetStandardSuperMarketList(CommerceInModel info)
        {
            List<StandardCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_ID,COMP_NAME,CHECK_YEAR,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,IS_STANDARD,GOODS_TYPE from BL_SUPERMARKET_STANDARD t where CHECK_MONTH=:V_CHECK_MONTH and GOODS_TYPE=:v_goods_type");

            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and COMP_NAME like '%{0}%'", info.CompName);
            }
            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and IS_STANDARD={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            aslgList.Add(new OracleParameter(":v_goods_type", info.GoodsType));

            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<StandardCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StandardCompModel temp = new StandardCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompId = DBNull.Value == dt.Rows[i]["COMP_ID"] ? string.Empty : dt.Rows[i]["COMP_ID"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["COMP_NAME"] ? string.Empty : dt.Rows[i]["COMP_NAME"].ToString();
                        temp.IsStandard = DBNull.Value == dt.Rows[i]["IS_STANDARD"] ? string.Empty : dt.Rows[i]["IS_STANDARD"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        /// <summary>
        ///团体运行达标情况
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<StandardCompModel> GetStandardTeamList(CommerceInModel info)
        {
            List<StandardCompModel> list = null;
            string strSql = string.Format(@" select ID,COMP_ID,COMP_NAME,CHECK_YEAR,CHECK_MONTH,CHECK_YEAR,CHECK_DATE,IS_STANDARD,GOODS_TYPE from BL_TEAM_STANDARD t where CHECK_MONTH=:V_CHECK_MONTH and GOODS_TYPE=:v_goods_type");

            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and COMP_NAME like '%{0}%'", info.CompName);
            }
            if (!string.IsNullOrEmpty(info.IsStandard))
            {
                if (info.IsStandard != "-1")
                {
                    strSql = strSql + string.Format(@" and IS_STANDARD={0}", Convert.ToInt32(info.IsStandard));
                }
            }

            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            aslgList.Add(new OracleParameter(":v_goods_type", info.GoodsType));

            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<StandardCompModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StandardCompModel temp = new StandardCompModel();
                        temp.CheckMonth = DBNull.Value == dt.Rows[i]["CHECK_MONTH"] ? string.Empty : dt.Rows[i]["CHECK_MONTH"].ToString();
                        temp.CompId = DBNull.Value == dt.Rows[i]["COMP_ID"] ? string.Empty : dt.Rows[i]["COMP_ID"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["COMP_NAME"] ? string.Empty : dt.Rows[i]["COMP_NAME"].ToString();
                        temp.IsStandard = DBNull.Value == dt.Rows[i]["IS_STANDARD"] ? string.Empty : dt.Rows[i]["IS_STANDARD"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// 考核报告内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommerceReportModel GetReport(CommerceInModel info)
        {
            CommerceReportModel temp = null;
            string strSql = string.Format(@" select report_content,
                                               id,
                                               check_month,
                                               check_year,
                                               check_date,
                                               insert_time,
                                               update_time
                                          from bl_commerce_report t where CHECK_MONTH=:V_CHECK_MONTH ");
            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter(":V_CHECK_MONTH", info.CheckMonth));
            DataTable dt = OracleHelper.Query(strSql, aslgList.ToArray());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    temp = new CommerceReportModel();
                    temp.ReportContent = DBNull.Value == dt.Rows[0]["report_content"] ? string.Empty : dt.Rows[0]["report_content"].ToString();
                }
            }
            return temp;
        }



    }
}
