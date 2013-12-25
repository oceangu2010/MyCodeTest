using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyTest.MyClassTest;
//using org.apache.pdfbox.pdmodel;
//using org.apache.pdfbox.util;
using org.pdfbox.util;
using org.pdfbox.pdmodel;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace MyTest.PageTest.ReflectandReadPDF
{
    public partial class ReflactTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aspose.Pdf.Text.TextOptions.TextOptions dd = new Aspose.Pdf.Text.TextOptions.TextOptions();

                //object obj = TestReflact.OutputReflact();
                string pdfPath = Server.MapPath("~/PageTest/ReflectandReadPDF/121.pdf");
                //pdfbox方式
                //PDDocument doc = PDDocument.load(pdfPath);
                //PDFTextStripper stripper = new PDFTextStripper();

                //pdf itextsharp方式
                string content = string.Empty;
                //content = stripper.getText(doc);
                //content = ReadPdfFile(pdfPath);
               ReadByAsposePDF(pdfPath);
            }
        }

        public string ReadPdfFile(string fileName)
        {
            StringBuilder text = new StringBuilder();

            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            return text.ToString();
        }

        private void ReadByAsposePDF(string fileName)
        {
            //open document
            Document pdfDocument = new Document(fileName);
            //create TextAbsorber object to extract text
            TextAbsorber textAbsorber = new TextAbsorber();
            //accept the absorber for all the pages
            pdfDocument.Pages.Accept(textAbsorber);
            /*
            for (var i=0;i<pdfDocument.Pages.Count;i++)
            { 
                var page = pdfDocument.Pages[i];

            }*/
            //get the extracted text
            string extractedText = textAbsorber.Text;
            // create a writer and open the file
            TextWriter tw = new StreamWriter("d:/pdftest/extracted-text.txt");
            // write a line of text to the file
            tw.WriteLine(extractedText);
            // close the stream
            tw.Close();
            tw.Dispose();
        }
    }
}