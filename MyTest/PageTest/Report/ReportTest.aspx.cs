using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace MyTest.PageTest.Report
{
    public partial class ReportTest : System.Web.UI.Page
    {

        //页面开始初始化 在所有控件都已初始化且已应用所有外观设置后引发。使用该事件来读取或初始化控件属性。
        protected void Page_PreInit(object sender, EventArgs e) { }
        protected void Page_Init(object sender, EventArgs e) { }

        //加载数据 会为自身和所有控件加载视图状态，然后会处理 Request 实例包括的任何回发数据
        protected void Page_PreLoad(object sender, EventArgs e) { }
        //Page 在 Page 上调用 OnLoad 事件方法，然后以递归方式对每个子控件执行相同操作，如此循环往复，直到加载完本页和所有控件为止。
        protected void Page_Load(object sender, EventArgs e)
        {
            //ASCX12DataSet.ConfigElementsDataTable dt1 = new ASCX12DataSet.ConfigElementsDataTable();
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PageTest/Report/Report1.rdlc");
            //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ReportPrintTest",SqlDataSource1));
            ReportViewer1.LocalReport.EnableExternalImages = true; // 允许使用外部图片
            //ReportViewer1.DataBind();
            ReportViewer1.ShowPrintButton = true;
        }
        //页面控制处理 页上的每个控件都会发生 PreRender 事件。使用该事件对页或其控件的内容进行最后更改。
        protected void Page_PreRender(object sender, EventArgs e) { }
        //这不是事件；在处理的这个阶段，Page 对象会在每个控件上调用此方法。所有 ASP.NET Web 服务器控件都有一个用于写出发送给浏览器的控件标记的 Render 方法。
        protected void Page_Render(object sender, EventArgs e) { }
        //页面装载后，关闭打开的文件和数据库连接，或完成日志记录或其他请求特定任务
        protected void Page_Unload(object sender, EventArgs e) { }
    }
}