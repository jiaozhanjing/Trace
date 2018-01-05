using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Trace.AppModel.MessageModel;
using Trace.Dal;

namespace Trace.AppBusiness
{
    public class MessageBusiness : BaseBusiness
    {
        
        /// <summary>
        /// 分页展示
        /// </summary>
        /// <returns></returns>
        public List<NoticeModel> GetNoticeList(int iPageSize, int iPageIndex)
        {
            // int iPageEnd = iPageSize * iPageIndex;
            //int iPageStart = (iPageIndex - 1) * iPageSize + 1;


            List<NoticeModel> list = null;
            string strSql = string.Format(@"select t.notice_id,
                                           t.image_url,
                                           t.notice_title,
                                           t.short_content,
                                           t.content,
                                           t.publish_time
                                      from BL_NOTICE t
                                     where t.is_deleted = 0
                                       and t.is_publish = 1
                                     order by t.order_index desc, t.publish_time desc
                                     ");
            //List<OracleParameter> aslgList = new List<OracleParameter>();
            //aslgList.Add(new OracleParameter(":v_end", iPageEnd));
            //aslgList.Add(new OracleParameter(":v_start", iPageStart));
            DataSet ds = OracleHelper.ExecSplitPage(iPageSize, iPageIndex, strSql, null);

            DataTable dt = ds.Tables[1];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    list = new List<NoticeModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        NoticeModel info = new NoticeModel();
                        info.ImageUrl = DBNull.Value == dt.Rows[i]["image_url"] ? string.Empty : dt.Rows[i]["image_url"].ToString();
                        info.NoticeContent = DBNull.Value == dt.Rows[i]["content"] ? string.Empty : dt.Rows[i]["content"].ToString();
                        info.NoticeId = DBNull.Value == dt.Rows[i]["notice_id"] ? string.Empty : dt.Rows[i]["notice_id"].ToString();
                        info.NoticeShortContent = DBNull.Value == dt.Rows[i]["short_content"] ? string.Empty : dt.Rows[i]["short_content"].ToString();
                        info.NoticeTitle = DBNull.Value == dt.Rows[i]["notice_title"] ? string.Empty : dt.Rows[i]["notice_title"].ToString();
                        info.PublishDate = DBNull.Value == dt.Rows[i]["publish_time"] ? string.Empty : dt.Rows[i]["publish_time"].ToString();

                        list.Add(info);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 根据id获取公告信息
        /// </summary>
        /// <param name="strNoticeId"></param>
        /// <returns></returns>
        public NoticeModel GetNoticeInfo(string strNoticeId)
        {

            NoticeModel info = null;
            string strSql = string.Format(@"select t.notice_id,
                                           t.image_url,
                                           t.notice_title,
                                           t.short_content,
                                           t.content,
                                           t.publish_time
                                      from BL_NOTICE t
                                     where t.notice_id=:v_notice_id and  t.is_deleted = 0
                                       and t.is_publish = 1 
                                     order by t.order_index desc, t.publish_time desc 
                                     ");
            DataTable dt = OracleHelper.Query(strSql, (new OracleParameter(":v_notice_id", strNoticeId)));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    info = new NoticeModel();
                    info.ImageUrl = DBNull.Value == dt.Rows[0]["image_url"] ? string.Empty : dt.Rows[0]["image_url"].ToString();
                    info.NoticeContent = DBNull.Value == dt.Rows[0]["content"] ? string.Empty : dt.Rows[0]["content"].ToString();
                    info.NoticeId = DBNull.Value == dt.Rows[0]["notice_id"] ? string.Empty : dt.Rows[0]["notice_id"].ToString();
                    info.NoticeShortContent = DBNull.Value == dt.Rows[0]["short_content"] ? string.Empty : dt.Rows[0]["short_content"].ToString();
                    info.NoticeTitle = DBNull.Value == dt.Rows[0]["notice_title"] ? string.Empty : dt.Rows[0]["notice_title"].ToString();
                    info.PublishDate = DBNull.Value == dt.Rows[0]["publish_time"] ? string.Empty : dt.Rows[0]["publish_time"].ToString();
                }
            }
            return info;
        }


    }
}
