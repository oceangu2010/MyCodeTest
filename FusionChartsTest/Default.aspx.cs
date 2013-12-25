using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using FusionChartsTest;

namespace FusionChartsTest
{
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*StringBuilder xmlData = new StringBuilder();
        xmlData.Append("<chart caption='Factory Output report' subCaption='By Quantity' showBorder='1' formatNumberScale='0' rotatelabels='1' showvalues='0'>");
        xmlData.AppendFormat("<categories>");
        string factoryQuery = "select distinct format(datepro,'dd/mm/yyyy') as dd from factory_output";
        DbConn oRs = new DbConn(factoryQuery);
        while (oRs.ReadData.Read())
        {
            xmlData.AppendFormat("<category label='{0}'/>", oRs.ReadData["dd"].ToString());
        }
        oRs.ReadData.Close();
        xmlData.AppendFormat("</categories>");
        string factoryquery2 = "select * from factory_master";
        DbConn oRs1 = new DbConn(factoryquery2);
        while (oRs1.ReadData.Read())
        {
            xmlData.AppendFormat("<dataset seriesName='{0}'>", oRs1.ReadData["factoryname"].ToString());
            string factoryquery3 = "select quantity from factory_output where factoryid=" + oRs1.ReadData["factoryid"].ToString();
            DbConn oRs2 = new DbConn(factoryquery3);
            while (oRs2.ReadData.Read())
            {
                xmlData.AppendFormat("<set value='{0}'/>", oRs2.ReadData[0].ToString());
            }
            oRs2.ReadData.Close();
            xmlData.AppendFormat("</dataset>");
        }
        oRs1.ReadData.Close();
        xmlData.AppendFormat("</chart>");
        Literal1.Text = FusionCharts.RenderChart("FusionCharts/MSLine.swf", "", xmlData.ToString(), "myFirst", "600", "300", false, true, false);*/

        //Chart.FusionChartHelper help = new Chart.FusionChartHelper(Chart.FusionChartType.Column2D);       

        //string factoryquery3 = "select datepro,quantity from factory_output where factoryid=1";
        //DbConn oRs2 = new DbConn();
        
        //help.SetDataSource(oRs2.GetDataSet(factoryquery3));
        //help.ChartHeight = 580;
        //help.ChartWidth = 900;

        //Literal1.Text = help.getChartString();
    }
}
}
