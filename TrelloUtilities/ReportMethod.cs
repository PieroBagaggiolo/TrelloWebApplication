using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using TrelloWebApplication.Models;

namespace TrelloUtilities
{
    public static class ReportMethods
    {

        public static ExcelPackage ExportExcelTotal(Api myApi)
        {
            List<Card> model = PopolateModel.Popola(myApi);
            //creazione di un foglio EXCEL
            var SheetName = "Foglio";
            ExcelPackage ex = CreazioneFoglio(SheetName);
            var workSheet = ex.Workbook.Worksheets[SheetName];
            int recordIndex = 1;
            //int chek = 0;

            foreach (var card in model)
            {

                PopolateExl.Riempimento(card, workSheet, recordIndex);
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
            CreazioneExl.CreazioneFile(ex, title);
            return ex;
        }

        public static ExcelPackage ExportSingleExcel(Card model)
        {
            //creazione di un foglio EXCEL
            var SheetName = "Foglio";
            ExcelPackage ex = CreazioneFoglio(SheetName);
            var workSheet = ex.Workbook.Worksheets[SheetName];
            int recordIndex = 1;
            PopolateExl.Riempimento(model, workSheet, recordIndex);

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
            CreazioneExl.CreazioneFile(ex, title);
            return ex;
        }

        private static ExcelPackage CreazioneFoglio(string sheetName)
        {
            ExcelPackage ex = new ExcelPackage();
            var workSheet = ex.Workbook.Worksheets.Add(sheetName);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            return ex;
        }
    }
}
