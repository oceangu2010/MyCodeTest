using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using Microsoft.Reporting.WebForms;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 为 GridView 设置数据源
        GridView1.DataSource = VisitorTraffic.find_all();
        GridView1.DataBind();

        // 初始化报表
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RLDCReport/Simple1/Report.rdlc");
        ReportViewer1.LocalReport.EnableExternalImages = true; // 允许使用外部图片

        // 获取 MyHandler.jxd 的完整路径
        string barcode_url = Request.Url.Scheme + "://" 
            + Request.Url.Authority
            + Request.ApplicationPath
            + "/MyHandler.jxd?data="; //-> "http://localhost:6344/HttpHandlerDemo/MyHandler.jxd?data="

        // 将 MyHandler.jxd 的完整路径通过报表参数传给报表
        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("barcode_url", barcode_url) });

        // 为报表设置数据源visitor_traffic  VisitorTraffic。
        ReportViewer1.LocalReport.DataSources.Add(  new Microsoft.Reporting.WebForms.ReportDataSource("VisitorTraffic", VisitorTraffic.find_all()));

        ReportViewer1.LocalReport.Refresh();
    }
}
