using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;

using TrelloWebApplication.Models;

namespace TrelloUtilities
{
    public class PopolateExl
    {
        public void Riempimento(Card model, ExcelWorksheet workSheet, int recordIndex, int fine)
        {
            //intestazione
            workSheet.Row(recordIndex).Height = 20;
            workSheet.Row(recordIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(recordIndex).Style.Font.Bold = true;
            workSheet.Row(recordIndex + 1).Height = 15;
            workSheet.Row(recordIndex + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(recordIndex + 1).Style.Font.Bold = true;
            //Corpo della table
            workSheet.Cells[recordIndex, 1].Value = "#";
            workSheet.Cells[recordIndex, 2].Value = "ID";
            workSheet.Cells[recordIndex, 3].Value = "NAME CARD";
            workSheet.Cells[recordIndex, 4].Value = "STATO";
            workSheet.Cells[recordIndex, 5].Value = "ARCHIVIATA";
            workSheet.Cells[recordIndex, 6].Value = "LABEL";
            using (ExcelRange LisTit = workSheet.Cells[recordIndex, 7, recordIndex, 8])
            {
                LisTit.Value = "CHECKLIST";
                LisTit.Merge = true;
            }
            workSheet.Cells[recordIndex + 1, 7].Value = "Titolo";
            workSheet.Cells[recordIndex + 1, 8].Value = "Opzioni";
            workSheet.Cells[recordIndex, 9].Value = "ATTACHMENTS";
            workSheet.Cells[recordIndex, 10].Value = "EXPIRE TIME";


            int inizio = recordIndex;

            using (ExcelRange Titles = workSheet.Cells[recordIndex, 1, recordIndex + 1, 10])
            {
                Titles.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Titles.Style.Fill.BackgroundColor.SetColor(Color.DarkGreen);
                Titles.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                Titles.Style.Border.Right.Color.SetColor(Color.Black);
                Titles.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                Titles.Style.Border.Top.Color.SetColor(Color.Black);
                Titles.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                Titles.Style.Border.Bottom.Color.SetColor(Color.Black);
                Titles.Style.Font.Color.SetColor(Color.WhiteSmoke);
            }

            recordIndex += 2;
            int i = recordIndex;
            workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
            workSheet.Cells[recordIndex, 2].Value = model.Id;
            workSheet.Cells[recordIndex, 3].Value = model.Name;
            workSheet.Cells[recordIndex, 4].Value = model.IdList;

            if (model.Closed.ToUpper()=="FALSE")
            {
                workSheet.Cells[recordIndex, 5].Value = "NO";
            }
            else
            {
                workSheet.Cells[recordIndex, 5].Value = "SI";
            }
            if (model.Labels.Count > 0)
                foreach (var item in model.Labels)
                {
                    workSheet.Cells[i, 6].Value = item.Name + " (" + item.Color + ")";
                    i++;
                }
            else
            {
                workSheet.Cells[i, 6].Value = "no Labels";
            }

            i = recordIndex;
            var j = 0;

            if (model.ChekedLists != null)
                foreach (var item in model.ChekedLists)
                {

                    workSheet.Cells[i, 7, fine, 7].Value = item.Name;
                    j = i;

                    foreach (var sol in item.CheckItems)
                    {

                        workSheet.Cells[i, 8].Value = sol.Name + " (" + sol.State + ")  ";
                        i++;
                    }
                    VerticalTitle(workSheet, 7, i - 1, j);
                }
            else
            {
                workSheet.Cells[i, 8].Value = "no ChekedLists";
                VerticalTitle(workSheet, 8, fine, i);
            }
            i = recordIndex;
            if (model.Attachments != null)
                foreach (var item in model.Attachments)
                {
                    workSheet.Cells[i, 9].Value = item.Name;
                    Uri url = new Uri(item.Url);
                    workSheet.Cells[i, 9].Hyperlink = url;
                    i++;
                }

            else
            {
                workSheet.Cells[i, 9].Value = "no Attachments";
                VerticalTitle(workSheet, 9, fine, i);
            }
            if (model.Due!=null)
            {
                workSheet.Cells[recordIndex, 10].Value = model.Due;
            }
            else
            {
                workSheet.Cells[recordIndex, 10].Value = "no data di scadenza";
            }

            using (ExcelRange Titles = workSheet.Cells[inizio + 2, 1, fine, 10])
            {
                Titles.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                Titles.Style.Border.Right.Color.SetColor(Color.Black);
                Titles.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                Titles.Style.Border.Top.Color.SetColor(Color.Black);
                Titles.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                Titles.Style.Border.Bottom.Color.SetColor(Color.Black);
                Titles.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Titles.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            }
            //VerticalTitle(workSheet, 1, fine, i);
            //VerticalTitle(workSheet, 2, fine, i);
            //VerticalTitle(workSheet, 3, fine, i);
            //VerticalTitle(workSheet, 4, fine, i);
            //VerticalTitle(workSheet, 5, fine, i);
            //VerticalTitle(workSheet, 6, fine, i);
            //VerticalTitle(workSheet, 10, fine, i);
        }

        private void VerticalTitle(ExcelWorksheet workSheet, int col, int fine, int i)
        {
            using (var title = workSheet.Cells[i, col, fine, col]) //funzione per unire più celle verticalmente
            {
                title.Merge = true;
                title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }
    }
}
