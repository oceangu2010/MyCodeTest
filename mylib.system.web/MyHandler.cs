using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using Cobainsoft.Windows.Forms;

namespace mylib.system.web
{
    public class MyHandler : System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region IHttpHandler ��Ա

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            // ��ȡ����data
            string data = context.Request.QueryString["data"];
            
            // ʹ������ؼ���������ͼƬ���������ڴ���
            BarcodeControl barcode = new BarcodeControl();
            barcode.BarcodeType = BarcodeType.CODE128B;
            barcode.CopyRight = ""; // ���ַ����ͻ᲻��ʾ����
            barcode.Data = data;

            MemoryStream stream = new MemoryStream();
            barcode.MakeImage(ImageFormat.Png, 1, 50, true, false, null, stream);

            // ��ͻ����������ͼƬ
            context.Response.ContentType = "image/png";
            context.Response.OutputStream.Write(stream.ToArray(), 0, (int)stream.Length);
            stream.Close();
        }
        #endregion
    }
}
