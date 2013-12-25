using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyTest.MyClassTest
{
    public partial class ShowPermineData : System.Web.UI.Page
    {
        //秒表计算公式
        private Stopwatch sw = new Stopwatch();
        string timeMsg = "程序耗时时间为:{0}<br>";

        protected void Page_Load(object sender, EventArgs e)
        {
            //ShowPerimeData();
        }


        #region btnPerime_Click

        protected void btnPerime_Click(object sender, EventArgs e)
        {
            int perimeNumber;
            string str = txtPerime.Text.Trim();
            if (string.IsNullOrEmpty(str)) return;
            string showMsg = string.Empty;

            if(int.TryParse(str,out perimeNumber))
            {
            }


            sw.Start();
             //采用嵌套循环的算法求100以内的素数
            string msg = FindPermine.GetPermine(perimeNumber);
            
            sw.Stop();

            showMsg+=    string.Format(timeMsg, sw.Elapsed.Ticks)+"<br />";
           // Response.Write(msg);
            //TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            

            sw.Start();

           // string showMsg = perimeNumber.ToString() + "以内的素数:<br />";
            FindPermine.FindPrime(perimeNumber);
            //int[] arr = FindPermine.PrimeList;

            //foreach (var i in arr)
            //{
            //    showMsg += i.ToString() + "<br /> ";
            //}
            sw.Stop();
            showMsg += string.Format(timeMsg, sw.Elapsed.Ticks) + "<br />";


            //显示结果
            showPerime.InnerHtml = showMsg;


            // Response.Write(showMsg);


        }
        #endregion

        #region ShowPerimeData function


        private void ShowPerimeData()
        {
            int inputNumber = 10;

            //采用嵌套循环的算法求100以内的素数
            string msg = FindPermine.GetPermine(inputNumber);
            Response.Write(msg);



            string showMsg = inputNumber.ToString() + "以内的素数:<br />";
            FindPermine.FindPrime(inputNumber);
            int[] arr = FindPermine.PrimeList;

            foreach (var i in arr)
            {
                showMsg += i.ToString() + "<br /> ";
            }

            Response.Write(showMsg);
        }

        #endregion

    }//end class
}