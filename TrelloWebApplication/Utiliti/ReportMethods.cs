using OfficeOpenXml;
using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using TrelloWebApplication.Models;
using IronPdf;

namespace TrelloWebApplication.Utiliti
{
    public static class ReportMethods
    {

        public static void ExportExcelTotal(Api myApi)
        {
            List<Card> model = PopolateModel.Popola(myApi);
            //creazione di un foglio EXCEL
            var sheetName = "Foglio";
            ExcelPackage ex = CreazioneFoglio(sheetName);
            var workSheet = ex.Workbook.Worksheets[sheetName];
            int recordIndex = 1;
            //int chek = 0;

            foreach (var card in model)
            {

                PopoateExl.Popola(card, workSheet, recordIndex);
                recordIndex += 9;
            }


            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();
            workSheet.Column(9).AutoFit();

            string title = "Total";
            CreazioneFile(ex, title);
        }

        public static void ExportSingleExcel(Card model)
        {
            //creazione di un foglio EXCEL
            var sheetName = "Foglio";
            ExcelPackage ex = CreazioneFoglio(sheetName);
            int recordIndex = 1;
            var workSheet = ex.Workbook.Worksheets[sheetName];
            PopoateExl.Popola(model, workSheet, recordIndex);

            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();
            workSheet.Column(9).AutoFit();


            string title = model.Name;
            CreazioneFile(ex, title);
        }

        private static ExcelPackage CreazioneFoglio(string sheetName)
        {
            ExcelPackage ex = new ExcelPackage();
            var worksheet = ex.Workbook.Worksheets.Add(sheetName);
            worksheet.TabColor = System.Drawing.Color.Black;
            worksheet.DefaultRowHeight = 12;
            return ex;
        }

        private static void CreazioneFile(ExcelPackage ex, string title)
        {
            using (var memoryStream = new MemoryStream())
            {

                HttpContext cur = HttpContext.Current;
                cur.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                cur.Response.AddHeader("content-disposition", "attachment; filename=" + title + ".xlsx");
                ex.SaveAs(memoryStream);
                memoryStream.WriteTo(cur.Response.OutputStream);
                cur.Response.Flush();
                cur.Response.End();

            }
        }
        public static void GeneratePDF()
        {

        }
        public static void ExportListPDF()
        {
            string code = Api.ChiamataApi("http://localhost:53250/card ", "GET");
            var List = new HtmlToPdf();
            List.PrintOptions.CreatePdfFormsFromHtml = false;
            List.PrintOptions.EnableJavaScript = true;
            List.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            var pdf = List.RenderHtmlAsPdf(code);
            
            var name = "CardList.pdf";
            pdf.SaveAs(name);

            System.Diagnostics.Process.Start(name);
            //Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
        }

        //public static void ExportDetailsPDF()
        //{
        //    HtmlToPdf List = new HtmlToPdf();
        //    var pdf = List.RenderHtmlAsPdf(code);
        //    pdf.SaveAs("Details.pdf");
        //}
    }
}