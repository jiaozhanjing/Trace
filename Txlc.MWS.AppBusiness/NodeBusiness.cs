using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.NodeModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class NodeBusiness : BaseBusiness
    {
        public List<NodeSummaryModel> GetNodeSummary()
        {
            List<NodeSummaryModel> list = null;
            string strSql = string.Format(@" select comptypecode, comptypename, node_number from v_bl_node_summary");
            DataTable dt = OracleHelper.Query(strSql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<NodeSummaryModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        NodeSummaryModel temp = new NodeSummaryModel();
                        temp.NodeNumbers = DBNull.Value == dt.Rows[i]["node_number"] ? string.Empty : dt.Rows[i]["node_number"].ToString();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["comptypecode"] ? string.Empty : dt.Rows[i]["comptypecode"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["comptypename"] ? string.Empty : dt.Rows[i]["comptypename"].ToString();
                        list.Add(temp);
                    }

                }
            }
            return list;
        }

        public List<NodeCompSummaryModel> GetNodeCompSummary(NodelInModel info)
        {
            List<NodeCompSummaryModel> list = null;
            string strSql = string.Format(@" select comptypecode, comptypename, comp_id, comp_name from v_bl_node_comp_summ where comptypecode=:v_node_type");
            DataSet ds = OracleHelper.ExecSplitPage(info.PageSize, info.PageIndex, strSql, new OracleParameter("v_node_type", info.CompTypeCode));
            DataTable dt = ds.Tables[1];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<NodeCompSummaryModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        NodeCompSummaryModel temp = new NodeCompSummaryModel();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["comptypecode"] ? string.Empty : dt.Rows[i]["comptypecode"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["comptypename"] ? string.Empty : dt.Rows[i]["comptypename"].ToString();
                        temp.CompCode = DBNull.Value == dt.Rows[i]["comp_id"] ? string.Empty : dt.Rows[i]["comp_id"].ToString();
                        temp.CompName = DBNull.Value == dt.Rows[i]["comp_name"] ? string.Empty : dt.Rows[i]["comp_name"].ToString();

                        list.Add(temp);
                    }

                }
            }
            return list;
        }
        /// <summary>
        /// 获取经营节点的信息
        /// </summary>
        /// <param name="strCompId"></param>
        /// <returns></returns>
        public NodeInfoModel GetNodeInfo(string strCompId)
        {
            NodeInfoModel info = null;

            string strSql = string.Format(@" select comp_id, comp_name, legal_represent, reg_id, addr, tel, area_name,''  compprofile from v_bl_node_info where comp_id=:v_comp_id ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter("v_comp_id", strCompId));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    info = new NodeInfoModel();
                    info.Address = dt.Rows[0]["addr"] == DBNull.Value ? string.Empty : dt.Rows[0]["addr"].ToString();
                    info.CompName = dt.Rows[0]["comp_name"] == DBNull.Value ? string.Empty : dt.Rows[0]["comp_name"].ToString();
                    info.CompProfile = dt.Rows[0]["compprofile"] == DBNull.Value ? string.Empty : dt.Rows[0]["compprofile"].ToString();
                    info.ContactInformation = dt.Rows[0]["tel"] == DBNull.Value ? string.Empty : dt.Rows[0]["tel"].ToString();
                    info.LegalPerson = dt.Rows[0]["legal_represent"] == DBNull.Value ? string.Empty : dt.Rows[0]["legal_represent"].ToString();
                    info.License = dt.Rows[0]["reg_id"] == DBNull.Value ? string.Empty : dt.Rows[0]["reg_id"].ToString();
                    info.Telphone = dt.Rows[0]["tel"] == DBNull.Value ? string.Empty : dt.Rows[0]["tel"].ToString();
                }
            }

            return info;
        }
    }
}
