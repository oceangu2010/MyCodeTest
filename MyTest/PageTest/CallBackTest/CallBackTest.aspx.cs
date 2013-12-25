using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyTest.PageTest;
using System.Security.Cryptography;

namespace MyTest.MyClassTest
{
    public partial class CallBackTest : System.Web.UI.Page
    {
        Stopwatch sw = new Stopwatch();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(GetMD5("123456").ToUpper());
        }

        public static string GetMD5(string enCodingStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(enCodingStr));
            return BitConverter.ToString(result).ToLower();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //sw.Start();
            //string msg = new DelegateClass().MethodTest("english");
            //sw.Stop();
            //Response.Write(string.Format("内容:{0},时间:{1}", msg, sw.Elapsed.Ticks));
            //Response.Write("<br />");
            //Response.Write( DynamicArrays.ShowArrayMsg());

            Response.Write("<br />");
            Response.Write(DelegateClass.FuncTest());

        }

        //二维数组之和求值
        protected void btnArray_Click(object sender, EventArgs e)
        {
            int sum=ArrayClass.GetArrayValue();
            Response.Write("<br />");
            Response.Write(String.Format("二维数组之和为：{0}",sum));
        }

        protected void btnRevetStr_Click(object sender, EventArgs e)
        {
            string str = txtInputStr.Text.Trim();
            str= ArrayClass.RevertStr(str);
            Response.Write("<br />");
            Response.Write(String.Format("倒转之后的字符串为：{0}", str));
        }

        protected void btnRecursive_Click(object sender, EventArgs e)
        {
            var str = Recursive.GetFinal(10);
            Response.Write("<br />");
            Response.Write(String.Format("10阶乘为：{0}", str));

            var msg="";
                Recursive.ConvertToBinary(ref msg,100);

                Response.Write("<br />");
                Response.Write(String.Format("100转化二进制为：{0}", msg));
        }


    }
}