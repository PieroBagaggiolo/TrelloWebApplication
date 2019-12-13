using System;
using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;

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
            int fine = 0;
            foreach (var card in model)
            {
                fine = CalcolateDimensionMax(recordIndex, card)+2;
                PopolateExl.Riempimento(card, workSheet, recordIndex,fine);
                recordIndex = fine;
                recordIndex += 4;
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
            workSheet.Column(10).AutoFit();
            return ex;
        }
        public static int CalcolateDimensionMax(int recordIndex, Card card)
        {
            int NumberAttachment;
            try
            {
                NumberAttachment = Int32.Parse(card.Badges.Attachments);
            }
            catch (FormatException)
            {
                NumberAttachment = 0;
            }
            
            if (!(card.NumberChekItem==0 &&card.NumberLabels==0 && NumberAttachment==0))
            {
                if (card.NumberChekItem >= card.NumberLabels && card.NumberChekItem >= NumberAttachment)
                {
                    recordIndex += card.NumberChekItem ;
                }
                else if (card.NumberLabels > NumberAttachment)
                {
                    recordIndex += card.NumberLabels ;
                }
                else
                {
                    recordIndex += NumberAttachment ;
                }
            }
            else
            {
                recordIndex++;
            }
            
            
            return recordIndex;
        }
        public static ExcelPackage ExportSingleExcel(Card model)
        {
            //creazione di un foglio EXCEL
            var SheetName = "Foglio";

            int maxGrow = CalcolateDimensionMax(1, model)+2;
            ExcelPackage ex = CreazioneFoglio(SheetName);
            var workSheet = ex.Workbook.Worksheets[SheetName];
            int recordIndex = 1;
            PopolateExl.Riempimento(model, workSheet, recordIndex,maxGrow);
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();
            workSheet.Column(9).AutoFit();
            workSheet.Column(10).AutoFit();
            return ex;
        }

        private static ExcelPackage CreazioneFoglio(string sheetName)
        {
            ExcelPackage ex = new ExcelPackage();
            var workSheet = ex.Workbook.Worksheets.Add(sheetName);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            //using (ExcelRange Rng = workSheet.Cells[1, 1, fullDim, 9])
            //{
            //    Rng.Style.Border.Top.Style = ExcelBorderStyle.Medium;
            //    Rng.Style.Border.Top.Color.SetColor(Color.Black);
            //    Rng.Style.Border.Left.Style = ExcelBorderStyle.Medium;
            //    Rng.Style.Border.Left.Color.SetColor(Color.Black);
            //    Rng.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            //    Rng.Style.Border.Right.Color.SetColor(Color.Black);
            //    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            //    Rng.Style.Border.Bottom.Color.SetColor(Color.White);
            //}


            return ex;
        }

    }
}
