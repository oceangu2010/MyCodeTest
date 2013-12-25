using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MyTest.PageTest.Paging
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        /// <summary>
        /// jPaginate 的摘要说明
        /// </summary>
        [WebService(Namespace = "http://tempuri.org/")]
        [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
        [ToolboxItem(false)]
        // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
        [System.Web.Script.Services.ScriptService]
        public class jPaginate : System.Web.Services.WebService
        {

            /// <summary>
            /// 取出总页数
            /// </summary>
            /// <param name="pageSize">每页记录条数</param>
            /// <returns></returns>
            [WebMethod]
            public string getTotalPage(int pageSize)
            {
                jPaginateService mts = new jPaginateService();
                string sql = "select count(*) from invs_imt_trade";
                int pages = mts.getTotalPages(sql, pageSize);
                string str = "{\"pages\":" + pages + "}";
                return str;
            }

            /// <summary>
            /// 取出分页数据
            /// </summary>
            /// <param name="pageSize">每页记录条数</param>
            /// <param name="currentPage">当前是第几页</param>
            /// <returns></returns>
            [WebMethod]
            public string getPaginateData(int pageSize, int currentPage)
            {
                jPaginateService mits = new jPaginateService();

                string sql = "select uui.name invs_name, t.invs_id, t.id, t.stk_unicode, t.stk_code, " +
                             "psc.sec_name stk_name, decode(t.bs_mark, 1,'买',0,'卖') bs_mark,  " +
                             "t.bs_time, t.bs_price, t.bs_count, t.occur_amount,  " +
                             "decode(t.is_finished, 1,'已清仓',0,'持有') is_finished  " +
                             "from INVS_IMT_TRADE t " +
                             "left outer join user_uni_info uui on t.invs_id=uui.user_uniid left outer join jjdb.pub_sec_code@jjdb psc on t.stk_unicode=psc.sec_unicode  " +
                             "where 1=1 order by t.id desc ";
                string str_id = "id desc";
                DataSet ds = jPaginateService.getPaginateRows(sql, str_id, pageSize, currentPage);
                string imtPostStr = mits.imtTradeRecodeDataSet2Json(ds);

                return imtPostStr;
            }
        }
    }
}
