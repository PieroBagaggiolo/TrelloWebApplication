﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TrelloUtilities.Models;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;

namespace TrelloUtilities
{
    public static class ReportMethods
    {
        private static DatabaseContext db = new DatabaseContext();
        public static ExcelPackage ExportExcelTotal(List<Card> model)
        {
            
            //creazione di un foglio EXCEL
            var SheetName = "Foglio";
            ExcelPackage ex = CreazioneFoglio(SheetName);

            var workSheet = ex.Workbook.Worksheets[SheetName];
            int recordIndex = 1;
            int fine = 0;
            foreach (var card in model)
            {
                fine = CalcolateDimensionMax(recordIndex, card)+1;
                PopolateExl.Riempimento(card, workSheet, recordIndex,fine);
                recordIndex = fine;
                recordIndex += 4;
            }
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            return ex;
        }
        public static void DelegateMethod(ref ExcelPackage ex)
        {
              // elementi neccessari per fare le chiamate le informazioni sono prese da db prende la prima sequenza
             ApiModel[] apiArrey = db.ApiModels.ToArray();
             ApiModel apiCredentials = apiArrey[0];
             // creazione del mio modello di api per le chiamate
             Api myApi = new Api(apiCredentials.Key, apiCredentials.Token, apiCredentials.IdBoard);
             //creazione del modello di liste di card
             List<Card> model = PopolateModel.Popola();
             ex = ReportMethods.ExportExcelTotal(model);
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

            int maxGrow = CalcolateDimensionMax(1, model)+1;
            ExcelPackage ex = CreazioneFoglio(SheetName);
            var workSheet = ex.Workbook.Worksheets[SheetName];
            int recordIndex = 1;
            PopolateExl.Riempimento(model, workSheet, recordIndex,maxGrow);
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
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
