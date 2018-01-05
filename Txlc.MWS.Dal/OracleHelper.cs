using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;
using System.Xml;

namespace Trace.Dal
{
    public class OracleHelper
    {
        private static string connectionString = "";
        public static string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        static OracleHelper()
        {
            GetConnString();
        }

        private static void GetConnString()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\web.config");
                XmlNode xn = doc.SelectSingleNode("/configuration/*[name()='hibernate-configuration']/*[name()='session-factory']/*[name()='property' and @name='connection.connection_string']");
                if (xn != null) connectionString = xn.InnerText.Trim();
                doc = null;
            }
            catch
            {
                connectionString = "";
            }
        }

        #region open OracleConnection
        /// <summary>
        /// 创建并打开数据库连接
        /// </summary>
        /// <returns></returns>
        private static OracleConnection Open()
        {
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                conn.Open();
            }
            catch { throw; }
            return conn;
        }
        #endregion

        #region close OracleConnection
        /// <summary>
        /// 关闭数据库连接释放对象
        /// </summary>
        /// <param name="conn"></param>
        private static void Close(OracleConnection conn)
        {
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        #endregion

        #region prepare OracleCommand
        private static void PrepareCommand(OracleConnection conn, OracleCommand cmd, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {
            cmd.Connection = conn;
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;
            cmd.Parameters.Clear();

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region Query
        /// <summary>
        /// 执行sql语句，返回查询结果
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable Query(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            DataTable dt = new DataTable();
            OracleDataAdapter sda = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = Open())
            {
                try
                {
                    PrepareCommand(conn, cmd, cmdType, cmdText, commandParameters);
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    dt.TableName = "temptable";
                    return dt;
                }
                catch (Exception e)
                {
                    //WriteErrorLog(cmdText, e, commandParameters);
                    throw;
                }
                finally
                {
                    Close(conn);
                }
            }
        }
        /// <summary>
        /// 执行sql语句，返回查询结果
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable Query(string cmdText, params OracleParameter[] commandParameters)
        {
            return Query(CommandType.Text, cmdText, commandParameters);
        }
        #endregion

        #region GetDataSet
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter sda = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = Open())
            {
                try
                {
                    PrepareCommand(conn, cmd, cmdType, cmdText, commandParameters);
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    return ds;
                }
                catch (Exception e)
                {
                    //WriteErrorLog(cmdText, e, commandParameters);
                    throw;
                }
                finally
                {
                    Close(conn);
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdText, params OracleParameter[] commandParameters)
        {
            return GetDataSet(CommandType.Text, cmdText, commandParameters);
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(IList<DbCommand> cmdList)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter sda = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = Open())
            {
                int i = 0;
                foreach (DbCommand dbpara in cmdList)
                {
                    try
                    {
                        PrepareCommand(conn, cmd, dbpara.CommandType, dbpara.CommandText, dbpara.OracleParameters);
                        sda.SelectCommand = cmd;
                        sda.Fill(ds, "Temp" + i.ToString());
                        ++i;
                    }
                    catch (Exception e)
                    {
                        Close(conn);
                        //WriteErrorLog(dbpara.CommandText, e, dbpara.OracleParameters);
                        throw;
                    }
                }
                return ds;
            }
        }
        #endregion

        #region 存储过程分页查询
        /// <summary>
        /// 存储过程分页查询，table[0]返回记录总数，table[1]为查询结果
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="recordCount">返回的记录总数</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="oracleParameters"></param>
        /// <returns></returns>
        public static DataSet ExecSplitPage(int pageSize, int currentPage, string cmdText, params OracleParameter[] oracleParameters)
        {
            int end = pageSize * currentPage;
            IList<DbCommand> list = new List<DbCommand>();

            list.Add(new DbCommand("select count(*) counts from (" + cmdText + ") t_temp9", oracleParameters));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select * from (
                            select splitPage_table_t1.*, rownum sppage_rn
                              from (" + cmdText + @") splitPage_table_t1
                             where rownum <= " + (pageSize * currentPage).ToString() +
                             @") splitPage_table_t2 
                         where sppage_rn >= " + (pageSize * (currentPage - 1) + 1).ToString());
            list.Add(new DbCommand(sb.ToString(), oracleParameters));

            DataSet ds = GetDataSet(list);
            ds.Tables[1].Columns.Remove("sppage_rn");
            return ds;
        }


        /// <summary>
        /// 存储过程分页查询，table[0]返回记录总数，table[1]为查询结果,table[2]为合计列
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="recordCount">返回的记录总数</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="sumColumns">聚合函数,逗号隔开</param>
        /// <param name="oracleParameters"></param>
        /// <returns></returns>  
        public static DataSet ExecSplitPage(int pageSize, int currentPage, string cmdText, string cmdSum, string sumColumns, params OracleParameter[] oracleParameters)
        {
            int end = pageSize * currentPage;
            IList<DbCommand> list = new List<DbCommand>();

            list.Add(new DbCommand("select count(*) counts" + (string.IsNullOrEmpty(sumColumns) ? "" : (", " + sumColumns)) +
                " from (" + cmdText + ") t", oracleParameters));

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select * from (
                            select splitPage_table_t1.*, rownum sppage_rn
                              from (" + cmdText + @") splitPage_table_t1
                             where rownum <= " + (pageSize * currentPage).ToString() +
                             @") splitPage_table_t2 
                         where sppage_rn >= " + (pageSize * (currentPage - 1) + 1).ToString());
            list.Add(new DbCommand(sb.ToString(), oracleParameters));
            if (!string.IsNullOrWhiteSpace(cmdSum))
            {
                list.Add(new DbCommand(cmdSum, oracleParameters));
            }
            DataSet ds = GetDataSet(list);
            ds.Tables[1].Columns.Remove("sppage_rn");
            return ds;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行sql语句返回受影响行数
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = Open())
            {
                PrepareCommand(conn, cmd, cmdType, cmdText, commandParameters);
                OracleTransaction myTrans = conn.BeginTransaction();
                cmd.Transaction = myTrans;
                try
                {
                    int result = cmd.ExecuteNonQuery();
                    myTrans.Commit();
                    cmd.Parameters.Clear();
                    return result;
                }
                catch
                {
                    myTrans.Rollback();
                    throw;
                }
                finally
                {
                    Close(conn);
                }
            }
        }
        /// <summary>
        /// 执行sql语句返回受影响行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, params OracleParameter[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
        /// <summary>
        /// 执行sql语句返回受影响行数
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(IList<DbCommand> cmdList)
        {
            using (OracleConnection conn = Open())
            {
                OracleCommand cmd = new OracleCommand();

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                OracleTransaction myTrans = conn.BeginTransaction();
                cmd.Transaction = myTrans;
                int result = 0;
                try
                {
                    foreach (DbCommand dbpara in cmdList)
                    {

                        PrepareCommand(conn, cmd, dbpara.CommandType, dbpara.CommandText, dbpara.OracleParameters);
                        result = cmd.ExecuteNonQuery();
                        if (result <= 0)
                        {
                           // throw new Exception("事务执行失败！" + dbpara.CommandText);
                        }
                    }
                    myTrans.Commit();
                }
                catch (Exception e)
                {
                    myTrans.Rollback();
                    throw e;
                }
                finally
                {
                    Close(conn);
                }
                return 1;
            }
        }

        public static int ExecuteNonQuery(IList<DbCommand> cmdList,bool isDele)
        {
            using (OracleConnection conn = Open())
            {
                OracleCommand cmd = new OracleCommand();

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                OracleTransaction myTrans = conn.BeginTransaction();
                cmd.Transaction = myTrans;
                int result = 0;
                try
                {
                    foreach (DbCommand dbpara in cmdList)
                    {

                        PrepareCommand(conn, cmd, dbpara.CommandType, dbpara.CommandText, dbpara.OracleParameters);
                        result = cmd.ExecuteNonQuery();
                        //if (result <= 0)
                        //{
                        //    throw new Exception("事务执行失败！" + dbpara.CommandText);
                        //}
                    }
                    myTrans.Commit();
                }
                catch (Exception e)
                {
                    myTrans.Rollback();
                    throw e;
                }
                finally
                {
                    Close(conn);
                }
                return 1;
            }
        }

        #endregion

        #region Insert<T>
        /// <summary>
        /// 将表映射对象的值新增到相应表中
        /// </summary>
        /// <typeparam name="T">与数据库表一一对应的实体类</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Insert<T>(T model) where T : new()
        {
            string[] s = model.ToString().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string className = s[s.Length - 1];

            Assembly assembly = typeof(T).Assembly;
            Type[] types = assembly.GetTypes();

            List<OracleParameter> alst = new List<OracleParameter>();
            StringBuilder insertStr = new StringBuilder();
            StringBuilder valueStr = new StringBuilder();

            foreach (Type type in types)
            {
                if (type.Name == className)
                {
                    bool flag = true;
                    PropertyInfo[] pis = type.GetProperties();
                    foreach (PropertyInfo pi in pis)
                    {
                        object o = pi.GetValue(model, null);
                        if (o != null)
                        {
                            insertStr.Append((flag ? "" : ", ") + "\"" + pi.Name + "\"");
                            valueStr.Append((flag ? "" : ", ") + ":" + pi.Name);
                            alst.Add(new OracleParameter(":" + pi.Name, o));
                            if (flag) flag = false;
                        }
                    }
                    return ExecuteNonQuery(CommandType.Text,
                        "insert into " + type.Name + "(" + insertStr.ToString() + ") values (" + valueStr.ToString() + ")",
                        alst.ToArray()) > 0;
                }
            }
            return false;
        }
        #endregion


        #region InsertCommand<T>
        /// <summary>
        /// insert参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DbCommand InsertCommand<T>(T model) where T : new()
        {
            string[] s = model.ToString().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string className = s[s.Length - 1];

            List<OracleParameter> alst = new List<OracleParameter>();
            StringBuilder insertStr = new StringBuilder();
            StringBuilder valueStr = new StringBuilder();

            bool flag = true;
            PropertyInfo[] pis = model.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object o = pi.GetValue(model, null);
                if (o != null)
                {
                    insertStr.Append((flag ? "" : ", ") + "\"" + pi.Name + "\"");
                    valueStr.Append((flag ? "" : ", ") + ":" + pi.Name);
                    alst.Add(new OracleParameter(":" + pi.Name, o));
                    if (flag) flag = false;
                }
            }
            DbCommand cmd = new DbCommand();
            cmd.CommandText = "insert into " + className + "(" + insertStr.ToString() + ") values (" + valueStr.ToString() + ")";
            cmd.OracleParameters = alst.ToArray();
            return cmd;
        }
        #endregion

        private static void WriteErrorLog(string sql, Exception e, params OracleParameter[] pars)
        {
            OracleCommand cmd = new OracleCommand();
            StringBuilder sbField = new StringBuilder();
            StringBuilder sbPar = new StringBuilder();
            OracleParameter op;
            sbField.Append("autoid, strsql, e_info");
            sbPar.Append("seq_errlog_id.nextval, :strsql, :e_info");
            op = new OracleParameter(":strsql", OracleType.NVarChar, 2000);
            op.Value = sql;
            cmd.Parameters.Add(op);
            cmd.Parameters.Add(new OracleParameter(":e_info", "源：" + e.Source + "; 描述：" + e.Message));
            if (pars != null)
            {
                int i = 1;
                foreach (OracleParameter par in pars)
                {
                    sbField.Append(", par" + i.ToString());
                    sbPar.Append(", :par" + i.ToString());
                    op = new OracleParameter(":par" + i.ToString(), OracleType.NVarChar, 300);
                    op.Value = par.ParameterName + "-> " + par.Value.ToString();
                    cmd.Parameters.Add(op);
                    ++i;
                }
            }

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                cmd.CommandText = "insert into tb_sys_err_log(" + sbField.ToString() + ") values(" + sbPar.ToString() + ")";
                cmd.Connection = conn;
                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    Close(conn);
                }
            }
        }
    }

    public class DbCommand
    {
        private string _commandText;
        private CommandType _commandType;
        private OracleParameter[] _oracleParameters;

        public DbCommand() { }
        public DbCommand(string commandText, CommandType commandType, params OracleParameter[] oracleParameters)
        {
            this._commandText = commandText;
            this._commandType = commandType;
            this._oracleParameters = oracleParameters;
        }
        public DbCommand(string commandText, params OracleParameter[] oracleParameters) : this(commandText, CommandType.Text, oracleParameters) { }

        public string CommandText
        {
            get { return _commandText; }
            set { _commandText = value; }
        }

        public CommandType CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        }

        public OracleParameter[] OracleParameters
        {
            get { return _oracleParameters; }
            set { _oracleParameters = value; }
        }
    }
}
