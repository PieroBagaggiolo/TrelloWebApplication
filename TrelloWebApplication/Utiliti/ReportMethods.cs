using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrelloWebApplication.Models;

namespace TrelloWebApplication.Utiliti
{
    public static class ReportMethods
    {

        public static void ExportExcelTotal()
        {
            List<Card> model = PopolateModel.Popola();
            //creazione di un foglio EXCEL
            var sheetName = "Foglio1";
            ExcelPackage ex = CreazioneFoglio2(sheetName);
            var workSheet = ex.Workbook.Worksheets[sheetName];
            int recordIndex = 1;
            //int chek = 0;

            foreach (var card in model)
            {
                Riempimento(card, workSheet, recordIndex);
                recordIndex += 9;
                //if (card.ChekedLists != null)
                //    foreach (var cheked in card.ChekedLists)
                //    {
                //         chek += cheked.CheckItems.Count+1;
                //    }
                //if (card.Attachments != null)
                //    if (chek>card.Attachments.Count && chek>card.Labels.Count)
                //    {
                //        recordIndex += chek+3;
                //    }
                //    else if (card.Attachments.Count>card.Labels.Count)
                //    {
                //        recordIndex += card.Attachments.Count+3;
                //    }
                //    else
                //    {
                //    recordIndex += card.Labels.Count + 3;
                //    }
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
            ExcelPackage ex;
            ExcelWorksheet workSheet;
            CreazioneFoglio(out ex, out workSheet);
            int recordIndex = 1;
            Riempimento(model, workSheet, recordIndex);

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

        private static void CreazioneFoglio(out ExcelPackage ex, out ExcelWorksheet workSheet)
        {
            ex = new ExcelPackage();
            workSheet = ex.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
        }

        private static ExcelPackage CreazioneFoglio2(string sheetName)
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

        private static void Riempimento(Card model, ExcelWorksheet workSheet,int recordIndex)
        {
            //intestazione
            workSheet.Row(recordIndex).Height = 20;
            workSheet.Row(recordIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(recordIndex).Style.Font.Bold = true;
            workSheet.Row(recordIndex+1).Height = 15;
            workSheet.Row(recordIndex+1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(recordIndex+1).Style.Font.Bold = true;
            //Corpo della table
            workSheet.Cells[recordIndex, 1].Value = "#";
            workSheet.Cells[recordIndex, 2].Value = "ID";
            workSheet.Cells[recordIndex, 3].Value = "NAME CARD";
            workSheet.Cells[recordIndex, 4].Value = "STATO";
            workSheet.Cells[recordIndex, 5].Value = "LABEL";
            workSheet.Cells[recordIndex, 6].Value = "CHECKLIST";
            workSheet.Cells[recordIndex+1, 6].Value = "Titolo";
            workSheet.Cells[recordIndex+1, 7].Value = "Opzioni";
            workSheet.Cells[recordIndex, 8].Value = "ATTACHMENTS";
            workSheet.Cells[recordIndex, 9].Value = "EXPIRE TIME";
            recordIndex += 3;
            int i = recordIndex;
            workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
            workSheet.Cells[recordIndex, 2].Value = model.Id;
            workSheet.Cells[recordIndex, 3].Value = model.Name;
            workSheet.Cells[recordIndex, 4].Value = model.IdList;
            if (model.Labels.Count > 0)
                foreach (var item in model.Labels)
                {
                    workSheet.Cells[i, 5].Value = item.Name + "(" + item.Color + ")";
                    i++;
                }
            else
            {
                workSheet.Cells[i, 5].Value = "no Labels";
            }

            i = recordIndex;

            if (model.ChekedLists != null)
                foreach (var item in model.ChekedLists)
                {
                    workSheet.Cells[i, 6].Value = item.Name;
                    foreach (var sol in item.CheckItems)
                    {

                        workSheet.Cells[i, 7].Value = sol.Name + "(" + sol.State + ")  ";
                        i++;
                    }
                }
            else
            {
                workSheet.Cells[i, 7].Value = "no ChekedLists";
            }
            i = recordIndex;
            if (model.Attachments != null)
                foreach (var item in model.Attachments)
                {
                    workSheet.Cells[i, 8].Value = item.Name + "Url :(" + item.Url + ")";
                    i++;
                }
            else
            {
                workSheet.Cells[i, 8].Value = "no Attachments";
            }

            workSheet.Cells[recordIndex, 9].Value = model.Due;
        }
    }
}