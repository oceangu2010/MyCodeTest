using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

namespace MyTest.PageTest.Json
{
    public partial class JsonUrl2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //包括js解析与C#解析，URL传递json字符串
            hidJson.Value = Request.QueryString["json"].ToString();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(myClass));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(hidJson.Value)))
            {
                myClass jsonObject = (myClass)ser.ReadObject(ms);
                Response.Write(jsonObject.Name);
                Response.Write(jsonObject.Sex);
            }
        }
    }

    [Serializable]
    [DataContract]
    public class myClass
    {
        [DataMember]
        public string Name { get; set; }

         [DataMember]
        public string Sex { get; set; }
    }
}