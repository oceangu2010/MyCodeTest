using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Cols_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Report();
    }


    public List<RLDCModel> GetDatas()
    {
        //string constr = ConfigurationManager.ConnectionStrings["MyTestConnectionString"].ConnectionString;
        //SqlConnection conn = new SqlConnection(constr);
        //conn.Open();
        //string sql = "select * from DataToExcel";
        //SqlCommand cmd = new SqlCommand(sql, conn);
        //SqlDataReader sdr = cmd.ExecuteReader();
        //List<RLDCModel> list = new List<RLDCModel>();
        //while (sdr.Read())
        //{
        //    RLDCModel model = new RLDCModel();
        //    model.ID = int.Parse(sdr["ID"].ToString());
        //    model.Name = sdr["Name"].ToString();
        //    model.Sex = sdr["Sex"].ToString();
        //    model.Address = sdr["Address"].ToString();
        //    list.Add(model);
        //}
        //sdr.Close();
        //conn.Close();
        List<RLDCModel> list = new List<RLDCModel>();
        for(var i =0;i<10;i++)
        {
            RLDCModel model = new RLDCModel();
            model.ID = i;
            model.Name =string.Format("Name{0}",i);
            model.Sex = (i%2).ToString();
            model.Address = string.Format("长江中路{0}号",i);
            list.Add(model);
        }

        return list;
    }
    public void Report()
    {
        ReportViewer1.Reset();
        DataTable dt = new DataTable("DataSet1_dtTest");
        dt.Columns.Add("ID", typeof(System.Int32));
        dt.Columns.Add("Name", typeof(System.String));
        dt.Columns.Add("Sex", typeof(System.String));
        dt.Columns.Add("Address", typeof(System.String));
        List<RLDCModel> list = GetDatas();
        #region 给报表行赋值
        foreach (RLDCModel model in list)
        {
            DataRow row = dt.NewRow();
            row["ID"] = model.ID;
            row["Name"] = model.Name;
            row["Sex"] = model.Sex;
            row["Address"] = model.Address;
            dt.Rows.Add(row);
        }
        #endregion

       // string rdlcPath = @"Report.rdlc";
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RLDCReport/Simple2/Report.rdlc");//rdlcPath;
        this.ReportViewer1.LocalReport.DisplayName = "多行数据报表测试";
        ReportDataSource datasource = new ReportDataSource("DataSet1_dtTest", dt);
        this.ReportViewer1.LocalReport.DataSources.Add(datasource);

        #region 给报表传参

        //ReportParameter [] prams ={
        //                      new ReportParameter("Year",DateTime.Now.Year.ToString()),
        //                      new ReportParameter("Moth",DateTime.Now.Month.ToString()),
        //                      new ReportParameter("Day",DateTime.Now.Day.ToString())
        //                      };

        //this.ReportViewer1.LocalReport.SetParameters(prams);

        #endregion
        this.ReportViewer1.LocalReport.Refresh();
    }
}

#region RDLCModel实体类

/// <summary>
///Model 的摘要说明
/// </summary>
public class RLDCModel
{
    public RLDCModel()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public int ID;
    public string Name;
    public string Sex;
    public string Address;

    private int id
    {
        get
        {
            return ID;
        }
        set
        {
            ID = value;
        }
    }

    private string CompanyName
    {
        get
        {
            return Name;
        }
        set
        {
            Name = value;
        }
    }

    private string sex
    {
        get
        {
            return Sex;
        }
        set
        {
            Sex = value;
        }
    }

    private string address
    {
        get
        {
            return Address;
        }
        set
        {
            Address = value;
        }
    }
}

#endregion