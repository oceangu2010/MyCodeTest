using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyTest.CacheTest
{
    public partial class PageCache : System.Web.UI.Page
    {
        private int ID = -1, langId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ID = Request.QueryString["id"] == null ? -1 : int.Parse(Request.QueryString["id"]);
                langId = Request.QueryString["langId"] == null ? -1 : int.Parse(Request.QueryString["langId"]);
                GetCacheValue(ID, langId);
            }

            
        }

        /// <summary>
        /// 动态显示页面缓存内容
        /// </summary>
        /// <param name="id">页面参数ID</param>
        /// <param name="langid"></param>
        /// <returns></returns>
        private  void GetCacheValue(int id,int langid)
        {
            switch (id)
            {
                case 1:
                    cacheDiv.InnerHtml = "当前时间1:"+DateTime.Now;
                    break;

                case 2:
                    cacheDiv.InnerHtml = "当前时间2:" + DateTime.Now;
                    break;

                case 3:
                    if(langid>0)
                    {
                        cacheDiv.InnerHtml = "cache3页面,含有langid值:" + langid.ToString() + ";   " + DateTime.Now;  
                    }
                    else
                    {
                        cacheDiv.InnerHtml = "cache3页面,无langeid" + DateTime.Now;
                    }
                    break;

                case  4:
                    Response.Buffer   =   false; 
                    Response.Cache.SetNoStore(); 
                    //页面不缓存
                    Response.Expires   =   0;   
                    Response.CacheControl   =   "no-cache ";   

                    cacheDiv.InnerHtml = "页面不缓存"+DateTime.Now;
                    break;

                default:
                    cacheDiv.InnerHtml = "默认显示页面";
                    break;
            }
        }
    }
}