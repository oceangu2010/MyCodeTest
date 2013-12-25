using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.PageTest.Paging
{
    public class jPaginateService
    {

        /// <summary>
        /// 取得总记录数
        /// </summary>
        /// <param name="sql">执行sql</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <returns></returns>
        public int getTotalPages(string sql, int pageSize)
        {
            int pages = 0;

            int totalRec = 0;
            try
            {
                object obj = null;//DbHelper.ExecuteScalar(DbHelper.ConnectionString, CommandType.Text, sql, null);
                if (obj != null)
                {
                    totalRec = Convert.ToInt32(obj);
                    pages = totalRec / pageSize;
                    if (totalRec % pageSize != 0)
                    {
                        pages += 1;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return pages;
        }


        /// <summary>
        /// 取得符合条件的数据
        /// 由于前端是ajax请求,所以未使用"页面模型"
        /// </summary>
        /// <param name="sql">获取数据的sql语句</param>
        /// <param name="str_id">用于OVER(ORDER BY " + str_id + ")的排序字段</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="currentPage">当前是第几页</param>
        /// <returns></returns>
        public static DataSet getPaginateRows(string sql, string str_id, int pageSize, int currentPage)
        {
            DataSet ds = null;
            if (string.IsNullOrEmpty(sql))
            {
                return ds;
            }

            StringBuilder sb_count = new StringBuilder();
            sb_count.Append("SELECT u.* FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + str_id + ") rn, rhf.* FROM (");
            sb_count.Append(sql);
            sb_count.Append(")");
            sb_count.Append(" rhf WHERE ROWNUM <= (:pCurrentPage * :pPageSize)) u WHERE rn >= ((:pCurrentPage-1) * :pPageSize)");

            OracleParameter[] paras = {
                new OracleParameter("pCurrentPage", currentPage),
                new OracleParameter("pPageSize", pageSize)          
            };

            try
            {
                string conn = DbHelper.ConnectionString;
                ds = DbHelper.ExecuteDataSet(conn, CommandType.Text, sb_count.ToString(), paras);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        /// <summary>
        /// 成交记录数据转换:DataSet转Json
        /// </summary>
        /// <param name="ds">数据源DataSet</param>
        /// <returns></returns>
        public string imtTradeRecodeDataSet2Json(DataSet ds)
        {
            string jsonStr = null;
            List<jPaginateModel> list = new List<jPaginateModel>();

            if (DbHelper.DataSetIsNull(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    jPaginateModel iitMod = new jPaginateModel();
                    iitMod.Id = Convert.ToInt32(dr["id"].ToString());
                    iitMod.Stk_Unicode = Convert.ToInt32(dr["Stk_Unicode"].ToString());
                    iitMod.Stk_Code = dr["Stk_Code"].ToString();

                    iitMod.Stk_Name = dr["Stk_Name"].ToString();
                    iitMod.Bs_Time = Convert.ToDateTime(dr["Bs_Time"].ToString());
                    iitMod.Bs_Mark_Name = dr["Bs_Mark"].ToString();

                    iitMod.Invsadv_Id = Convert.ToInt32(dr["Invs_Id"].ToString());
                    iitMod.Invs_Name = dr["Invs_Name"].ToString();
                    iitMod.Bs_Price = Convert.ToDouble(dr["Bs_Price"].ToString());
                    iitMod.Bs_Count = Convert.ToInt32(dr["Bs_Count"].ToString());
                    iitMod.Occur_Amount = Convert.ToDouble(dr["Occur_Amount"].ToString());
                    iitMod.Is_Finished_Name = dr["Is_Finished"].ToString();

                    list.Add(iitMod);
                }

                //Newtonsoft.Json在转换日期的时候，会出现格式和时区(差8小时)差别
                //如果不作如下格式转换，会出现形如Date(1335247957000+0800)/的日期
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
                timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";

                jsonStr = JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter);
                //jsonStr = "[{Stk_Code:'601398',Stk_name:'工商银行'},{Stk_Code:'500999',Stk_name:'农业银行'}]";
            }

            return jsonStr;
        }
    }
}