using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;

namespace MyTest.PageTest
{
    public partial class IEnumeratorTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList();
            arr.Add("How");
            arr.Add("are");
            arr.Add("you");
            //arr.Add(100);
            //arr.Add(200);
            //arr.Add(300);
            //arr.Add(1.2);
            //arr.Add(22.8);

            //IEnumerator<string> ie = arr.GetEnumerator();
           
        }


        //public IEnumerator<ConfigElementsVO> GetList()
        //{
        //   List<TestClass>

        //    return null;
        //}
    }
}