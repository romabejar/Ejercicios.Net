using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFWithLabelGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1) {
                Console.WriteLine("Usage:\r\n\t PDFWithLabelGenerator <labelText>");
            }
            string filename = "output.pdf";
            Document pdfDoc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(@filename, FileMode.OpenOrCreate));
            pdfDoc.Open();
            pdfDoc.Add(new Paragraph(args[0]));
            pdfDoc.Close();
        }
    }
}
