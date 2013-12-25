using System;
using System.Threading;
using MyTest.MyClassTest;

namespace MyTest.PageTest.tryCatch
{
    public partial class tryCatchTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.Click += new EventHandlerWrapper(ButtonCalculateClick);
        }


        protected void ButtonCalculateClick(object sender, EventArgs e)
        {

            Thread.Sleep(2000);

            int op1 = int.Parse(this.TextBox1.Text);
            int op2 = int.Parse(this.TextBox2.Text);
            int result = op1 / op2;
            this.TextBox3.Text = result.ToString();
        }  
    }
}