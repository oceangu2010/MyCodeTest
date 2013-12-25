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
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            // 获取参数data
            string data = context.Request.QueryString["data"];
            
            // 使用条码控件生成条码图片并保存在内存中
            BarcodeControl barcode = new BarcodeControl();
            barcode.BarcodeType = BarcodeType.CODE128B;
            barcode.CopyRight = ""; // 空字符串就会不显示标题
            barcode.Data = data;

            MemoryStream stream = new MemoryStream();
            barcode.MakeImage(ImageFormat.Png, 1, 50, true, false, null, stream);

            // 向客户端输出条码图片
            context.Response.ContentType = "image/png";
            context.Response.OutputStream.Write(stream.ToArray(), 0, (int)stream.Length);
            stream.Close();
        }
        #endregion
    }
}
