using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace HYC.RDLC
{
    public class ReportViewer_Stream_Print : IDisposable
    {
        /// <summary>
        /// 用来记录当前打印到第几页了
        /// </summary>
        private int m_currentPageIndex;

        /// <summary>
        /// 声明一个Stream对象的列表用来保存报表的输出数据,LocalReport对象的Render方法会将报表按页输出为多个Stream对象。
        /// </summary>
        private IList<Stream> m_streams;

        private bool isLandSapces = false;

        /// <summary>
        /// 用来提供Stream对象的函数，用于LocalReport对象的Render方法的第三个参数。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileNameExtension"></param>
        /// <param name="encoding"></param>
        /// <param name="mimeType"></param>
        /// <param name="willSeek"></param>
        /// <returns></returns>
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        /// <summary>
        /// 为Report.rdlc创建本地报告加载数据,输出报告到.emf文件,并打印,同时释放资源
        /// </summary>
        /// <param name="rv">参数:ReportViewer.LocalReport</param>
        public void PrintStream(LocalReport rvDoc)
        {
            //获取LocalReport中的报表页面方向
            isLandSapces = rvDoc.GetDefaultPageSettings().IsLandscape;
            Export(rvDoc);
            PrintSetting();
            Dispose();
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
            @"<DeviceInfo>
                 <OutputFormat>EMF</OutputFormat>
             </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //将报表的内容按照deviceInfo指定的格式输出到CreateStream函数提供的Stream中。
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintSetting()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("错误:没有检测到打印数据流");
            //声明PrintDocument对象用于数据的打印
            PrintDocument printDoc = new PrintDocument();
            //判断指定的打印机是否可用
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("错误:找不到打印机");
            }
            else
            {
                //设置打印机方向遵从报表方向
                printDoc.DefaultPageSettings.Landscape = isLandSapces;
                //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                //执行打印操作，Print方法将触发PrintPage事件。
                printDoc.Print();
            }
        }

        /// <summary>
        /// 处理程序PrintPageEvents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            //Metafile对象用来保存EMF或WMF格式的图形，
            //我们在前面将报表的内容输出为EMF图形格式的数据流。
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            //调整打印机区域的边距www.it165.net
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            //绘制一个白色背景的报告
            //ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            //获取报告内容
            //这里的Graphics对象实际指向了打印机
            ev.Graphics.DrawImage(pageImage, adjustedRect);
            //ev.Graphics.DrawImage(pageImage, ev.PageBounds);

            // 准备下一个页,已确定操作尚未结束
            m_currentPageIndex++;

            //设置是否需要继续打印
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}