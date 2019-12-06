using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using IronPdf;

namespace TrelloServices
{
    class ReportMethods
    {
        public void ExportDetPDF(string url)
        {
            IronPdf.HtmlToPdf details = new IronPdf.HtmlToPdf();

            details.RenderHtmlAsPdf("<h1>Hello</h1>").SaveAs("Details.html");

            // Advanced: 
            // Set a "base url" or file path so that images, javascript and CSS can be loaded 
            var file = details.RenderHtmlAsPdf("");
            file.SaveAs("details.pdf");
        }
    

        public ExcelPackage CreateSheet(string name)
        {
            ExcelPackage ex = new ExcelPackage();
            ExcelWorksheet workSheet = ex.Workbook.Worksheets.Add(name);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            return ex;
        }

    }
}
