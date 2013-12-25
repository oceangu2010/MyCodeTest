using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyTest.MyClassTest;


namespace MyTest.MyClassTest
{
    public partial class ControlCache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCache.Text = DateTime.Now.ToString();
            bag.GetBag();

            A aa = new A();
            
            C cc = new C();
     
        }

        protected void btnCache_Click(object sender, EventArgs e)
        {
            btnCache.Text = DateTime.Now.ToString();
        }

        protected void btnNoCache_Click(object sender, EventArgs e)
        {
            lblCache.Text = DateTime.Now.ToString();
        }
    }

}