using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Text;


namespace MyTest.PageTest.Report
{
    public partial class reportTest2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDataToReport();
            }
        }
        private Hashtable hshTable = new Hashtable();
        void FillDataToReport()
        {
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("EMPNO", typeof(string));
            dt.Columns.Add("EMPNAME", typeof(string));
            dt.Rows.Add("000", "hello world");
            dt.Rows.Add("001", "中国人");
            dt.Rows.Add("002", "报表测试");
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PageTest/Report/Report_EMP.rdlc");

            //List<Hashtable> list = new List<Hashtable>();
            //hshTable.Add("EMPNO", "000");
            //hshTable.Add("EMPNAME", "hello world000");
            //list.Add(hshTable);
            //hshTable.Clear();

            //hshTable.Add("EMPNO", "001");
            //hshTable.Add("EMPNAME", "hello world001");
            //list.Add(hshTable);
            //hshTable.Clear();

            //hshTable.Add("EMPNO", "002");
            //hshTable.Add("EMPNAME", "hello world002");
            //list.Add(hshTable);
            //hshTable.Clear();
            //List<string> l = new List<string>();
            //List<List<string>> list = new List<List<string>>();
            //l.Add("000"); l.Add("11111"); list.Add(l); l.Clear();
            //l.Add("111"); l.Add("222222"); list.Add(l); l.Clear();
            //l.Add("222"); l.Add("333333"); list.Add(l); l.Clear();
            //如何传参数
            ReportParameter myParameter = new ReportParameter("参数01", "参数的值");
            //ReportParameter[] myParameterList = { myParameter };
            //ReportViewer1.LocalReport.SetParameters(myParameterList);
            //ReportDataSource rds = new ReportDataSource("T_EMP", list);
            //=Parameters!keyName.Value
            ReportParameter parammeter = new ReportParameter("keyName", "zhangsan");
            ReportParameter[] param = { parammeter, myParameter };
            this.ReportViewer1.LocalReport.SetParameters(param);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("T_EMP", dt));
            //this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("T_EMP", list));
            this.ReportViewer1.LocalReport.Refresh();
        }


        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            const string printerName = "Microsoft Office Document Image Writer";

            if (m_streams == null || m_streams.Count == 0)
                return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format("Can't find printer \"{0}\".", printerName);
                //MessageBox.Show(msg, "Print Error");
                Response.Write(msg);
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }

        private Stream CreateStream(string name,
        string fileNameExtension, Encoding encoding,
        string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(@"F:\myself\" + name +
                "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>8.5in</PageWidth>" +
              "  <PageHeight>11in</PageHeight>" +
              "  <MarginTop>0.25in</MarginTop>" +
              "  <MarginLeft>0.25in</MarginLeft>" +
              "  <MarginRight>0.25in</MarginRight>" +
              "  <MarginBottom>0.25in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void Run()
        {
            LocalReport report = new LocalReport();
            //report.ReportPath = @"c:\My Reports\Report.rdlc";
            //report.DataSources.Add(new ReportDataSource("Sales", LoadSalesData()));
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("EMPNO", typeof(string));
            dt.Columns.Add("EMPNAME", typeof(string));
            dt.Rows.Add("000", "hello world");
            dt.Rows.Add("001", "中国人");
            dt.Rows.Add("002", "报表测试");
            report.ReportPath = Server.MapPath("~/PageTest/Report/Report_EMP.rdlc");
            report.DataSources.Add(new ReportDataSource("T_EMP", dt));

            Export(report);

            m_currentPageIndex = 0;
            Print();
        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //m_currentPageIndex = 0;
            Run();
        }
    }
}