using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.BusinessModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class BusinessBusiness : BaseBusiness
    {
        /// <summary>
        /// 经营户总汇总
        /// </summary>
        /// <returns></returns>
        public List<BusinessSummaryModel> GetBusinessSummary()
        {
            List<BusinessSummaryModel> list = null;
            string strSql = string.Format(@" select comptypename, node_type, bus_number from v_bl_bus_summary");
            DataTable dt = OracleHelper.Query(strSql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<BusinessSummaryModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BusinessSummaryModel temp = new BusinessSummaryModel();
                        temp.BusinessNumbers = DBNull.Value == dt.Rows[i]["bus_number"] ? string.Empty : dt.Rows[i]["bus_number"].ToString();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["node_type"] ? string.Empty : dt.Rows[i]["node_type"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["COMPTYPENAME"] ? string.Empty : dt.Rows[i]["COMPTYPENAME"].ToString();
                        list.Add(temp);
                    }

                }
            }
            return list;
        }
        /// <summary>
        /// 经营户汇总2
        /// </summary>
        /// <param name="strCompTypeCode"></param>
        /// <returns></returns>
        public List<BusinessCompSummaryModel> GetBusinessCompSummary(BusinessInModel info)
        {
            List<BusinessCompSummaryModel> list = null;
            string strSql = string.Format(@" select node_type, comp_id, comp_name, bus_number from v_bl_bus_comp_summ where node_type=:v_node_type");
            DataSet ds = OracleHelper.ExecSplitPage(info.PageSize, info.PageIndex, strSql, new OracleParameter("v_node_type", info.CompTypeCode));
            DataTable dt = ds.Tables[1];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<BusinessCompSummaryModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BusinessCompSummaryModel temp = new BusinessCompSummaryModel();
                        temp.BusinessCompNumbers = DBNull.Value == dt.Rows[i]["bus_number"] ? string.Empty : dt.Rows[i]["bus_number"].ToString();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["node_type"] ? string.Empty : dt.Rows[i]["node_type"].ToString();
                        temp.CompCode = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();

                        list.Add(temp);
                    }

                }
            }
            return list;
        }
        /// <summary>
        /// 经营户详细信息列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<BusinessDetailModel> GetBusinessDetail(BusinessInModel info)
        {
            List<BusinessDetailModel> list = null;
            string strProce = "pro_bus_detail";
            List<OracleParameter> aslgList = new List<OracleParameter>();
            aslgList.Add(new OracleParameter("v_comp_code", info.CompCode));
            aslgList.Add(new OracleParameter("v_legalperson", string.IsNullOrEmpty(info.LegalPerson) ? string.Empty : info.LegalPerson));

            aslgList.Add(new OracleParameter("PageSize", info.PageSize));
            aslgList.Add(new OracleParameter("PageIndex", info.PageIndex));

            aslgList.Add(new OracleParameter("v_cur", System.Data.OracleClient.OracleType.Cursor) { Direction = ParameterDirection.Output });

            DataTable dt = OracleHelper.Query(CommandType.StoredProcedure, strProce, aslgList.ToArray());

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<BusinessDetailModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BusinessDetailModel temp = new BusinessDetailModel();
                        temp.BoothNumber = DBNull.Value == dt.Rows[i]["BOOTH_NUMBER"] ? string.Empty : dt.Rows[i]["BOOTH_NUMBER"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["biz_name"] ? string.Empty : dt.Rows[i]["biz_name"].ToString();
                        temp.LegalPerson = DBNull.Value == dt.Rows[i]["legal_represent"] ? string.Empty : dt.Rows[i]["legal_represent"].ToString();
                        temp.License = DBNull.Value == dt.Rows[i]["reg_id"] ? string.Empty : dt.Rows[i]["reg_id"].ToString();
                        temp.Telphone = DBNull.Value == dt.Rows[i]["tel"] ? string.Empty : dt.Rows[i]["tel"].ToString();

                        list.Add(temp);
                    }
                }
            }
            return list;
        }
    }
}
