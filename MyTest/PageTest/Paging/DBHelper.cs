using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MyTest.PageTest.Paging
{
    /// <summary>
    /// A helper class used to execute queries against an Oracle database
    /// </summary>
    public abstract class DbHelper
    {
        public static string ConnectionString = string.Empty;

        /// <summary>
        /// 判断DataSet ds是否有数据，包括为空，没有表，没有行数据。如果返回true，表示有数据
        /// </summary>
        /// <param name="ds">待验证的DataSet</param>
        /// <returns></returns>
        public static bool DataSetIsNull(DataSet ds)
        {
            bool bl = false; //默认没有数据
            if (ds != null)
            {
                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        bl = true;
                    }
                }
            }

            return bl;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        /// <summary>
        /// Execute a select query that will return a result DataSet
        /// </summary>
        /// <param name="connString">Connection string</param>
        //// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //Create the command and connection and DataSet
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //Prepare the command to execute
               PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);

                //Execute the query, stating that the connection should close when the resulting datareader has been fill
                oda.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }

        }
    }
}