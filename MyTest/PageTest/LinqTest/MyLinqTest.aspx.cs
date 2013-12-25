using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyTest.MyClassTest.LinqClass;

namespace MyTest.MyClassTest
{
    public partial class MyLinqTest : System.Web.UI.Page
    {
        //首先对象实例化
        protected NortwndDataContext db = new NortwndDataContext();
        LinqOperate lo = new LinqOperate();

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowLinqPage();
            //测试实例一
           // lo.LinqTest1();
            showCustomer();
        }



        #region 显示客户资料信息
		 

        private  void showCustomer()
        {
            string companyName = "Fo";//"Ernst Handel";

            //具体条件 First
           // Customer customer = lo.QueryCustomer(db.Customers,companyName);
            Customer customer = lo.QueryLastCustomer(db.Customers, companyName);
            //模糊匹配条件
           // List<Customer> list = lo.QueryCustomerList(db.Customers, "Fo");

            Response.Write("<br />");
            Response.Write("客户公司名称全称:" + customer.CompanyName + ";客户地址：" + customer.Address);
           /* foreach (var customer in list)
            {
                Response.Write("客户公司名称全称:"+customer.CompanyName+";客户地址：" + customer.Address);
                Response.Write("<br />");
            }*/
            Response.Write("<br /><br />"); 
        }


        #endregion

        #region 分页处理方法

        private int currentIndex = 1;
        private  void ShowLinqPage()
        {
            if (pageAttr.CurrentIndex == null) pageAttr.CurrentIndex=1;
            lo.BindBoundControl<Customer>(db.Customers.OrderBy(v => v.CustomerID), this.GridView1, 10,(int) pageAttr.CurrentIndex);
        }
        　　　　　　　　


        #endregion

        protected void btnAddPageNum_Click(object sender, EventArgs e)
        {
            if (pageAttr.CurrentIndex == null) pageAttr.CurrentIndex = 1;
            pageAttr.CurrentIndex++;
        }


    }

    public class pageAttr
    {
        public static int? CurrentIndex { get; set; }
    }
}