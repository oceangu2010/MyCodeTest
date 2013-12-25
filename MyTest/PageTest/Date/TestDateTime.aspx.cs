using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyTest.PageTest.Date
{
    public partial class TestDateTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            int year = int.Parse(txtDate.Text.Trim());
            int month = int.Parse(txtMonth.Text.Trim());
            Response.Write(DateTime.DaysInMonth(year, month));
        }

    }
}