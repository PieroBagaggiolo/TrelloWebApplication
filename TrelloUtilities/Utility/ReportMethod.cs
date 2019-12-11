﻿using System;
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
            foreach (var card in model)
            {

                PopolateExl.Riempimento(card, workSheet, recordIndex);
                int NumberAttachment ;
                try
                {
                     NumberAttachment = Int32.Parse(card.Badges.Attachments);
                 
                }
                catch (FormatException)
                {
                    NumberAttachment = 0;
                }
                if (card.NumberChekItem>=card.NumberLabels && card.NumberChekItem>=NumberAttachment)
                {
                    recordIndex += card.NumberChekItem;
                }
                else if (card.NumberLabels>NumberAttachment)
                {
                    recordIndex += card.NumberLabels;
                }
                else
                {
                    recordIndex += NumberAttachment;
                }
                recordIndex+=4;
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
            return ex;
        }

        private static ExcelPackage CreazioneFoglio(string sheetName, int maxDim)
        {
            ExcelPackage ex = new ExcelPackage();
            var workSheet = ex.Workbook.Worksheets.Add(sheetName);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            using (ExcelRange Rng = workSheet.Cells[1, 1, 9, maxDim])
            {
                Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Top.Color.SetColor(Color.Red);
                Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Left.Color.SetColor(Color.Green);
                Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Right.Color.SetColor(Color.Green);
                Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Bottom.Color.SetColor(Color.AliceBlue);
            }
            return ex;
        }

    }
}
