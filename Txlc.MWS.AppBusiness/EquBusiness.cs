using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.EquModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class EquBusiness
    {
        /// <summary>
        /// 设备统计
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<EquTotalModel> GetEquTotal(EquInModel info)
        {
            List<EquTotalModel> list = null;
            string strSql = string.Format(@"select t.equ_type_code, t.equ_type_name, sum(t.user_number) as equnumber
  from bl_equ_info t
 group by t.equ_type_code, t.equ_type_name ");
            DataTable dt = OracleHelper.Query(strSql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<EquTotalModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EquTotalModel temp = new EquTotalModel();
                        temp.EquTypeCode = DBNull.Value == dt.Rows[i]["equ_type_code"] ? string.Empty : dt.Rows[i]["equ_type_code"].ToString();
                        temp.EquTypeName = DBNull.Value == dt.Rows[i]["equ_type_name"] ? string.Empty : dt.Rows[i]["equ_type_name"].ToString();
                        temp.EquNumber = DBNull.Value == dt.Rows[i]["equnumber"] ? string.Empty : dt.Rows[i]["equnumber"].ToString();
                        list.Add(temp);
                    }
                }
            }

            return list;
        }

        public List<EquUserModel> GetEquUserList(EquInModel info)
        {
            List<EquUserModel> list = null;
            string strSql = string.Format(@"select t.comp_type_code,t.comp_type_name, sum(t.user_number) as userNumber
  from bl_equ_info t where t.equ_type_code=:v_equ_type_code
 group by t.comp_type_code,t.comp_type_name ");
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_equ_type_code", info.EquTypeCode));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<EquUserModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EquUserModel temp = new EquUserModel();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["comp_type_code"] ? string.Empty : dt.Rows[i]["comp_type_code"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["comp_type_name"] ? string.Empty : dt.Rows[i]["comp_type_name"].ToString();
                        temp.UserNumber = DBNull.Value == dt.Rows[i]["userNumber"] ? string.Empty : dt.Rows[i]["userNumber"].ToString();
                        list.Add(temp);
                    }
                }
            }
            //           //select comp_id, comp_name, count(t.id) as UserNumber
            // from bl_equ_info t
            //where and t.equ_type_code =
            //   and t.comp_type_code =
            //   and t.comp_name like
            //group by t.comp_id, t.comp_name
            return list;
        }

        public List<EquUserModel> GetEquUserDetail(EquInModel info)
        {
            List<EquUserModel> list = null;
            string strSql = string.Format(@"select t.comp_id,
                                               t.comp_name,
                                               t.equ_type_code,
                                               t.equ_type_name,
                                               t.user_number,comp_type_code,comp_type_name
                                          from bl_equ_info t
                                         where t.equ_type_code=:v_equ_type_code
                                           and t.comp_type_code=:v_comp_type_code ");
            if (!string.IsNullOrEmpty(info.CompName))
            {
                strSql = strSql + string.Format(@" and t.comp_name like '%{0}%'", info.CompName);
            }
            DataTable dt = OracleHelper.Query(strSql, new OracleParameter(":v_equ_type_code", info.EquTypeCode), new OracleParameter(":v_comp_type_code", info.CompTypeCode));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<EquUserModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EquUserModel temp = new EquUserModel();
                        temp.CompTypeCode = DBNull.Value == dt.Rows[i]["comp_type_code"] ? string.Empty : dt.Rows[i]["comp_type_code"].ToString();
                        temp.CompTypeName = DBNull.Value == dt.Rows[i]["comp_type_name"] ? string.Empty : dt.Rows[i]["comp_type_name"].ToString();
                        temp.UserNumber = DBNull.Value == dt.Rows[i]["user_number"] ? string.Empty : dt.Rows[i]["user_number"].ToString();
                        list.Add(temp);
                    }
                }
            }
            //           //select comp_id, comp_name, count(t.id) as UserNumber
            // from bl_equ_info t
            //where and t.equ_type_code =
            //   and t.comp_type_code =
            //   and t.comp_name like
            //group by t.comp_id, t.comp_name
            return list;
        }

    }
}
