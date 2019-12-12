using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using TrelloWebApplication.Models;

namespace TrelloUtilities
{
    public class PopolateExl
    {
        public static void Riempimento(Card model, ExcelWorksheet workSheet, int recordIndex, int fine)
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
            workSheet.Cells[recordIndex, 5].Value = "LABEL";
            using (ExcelRange LisTit = workSheet.Cells[recordIndex, 6, recordIndex, 7])
            {
                LisTit.Value = "CHECKLIST";
                LisTit.Merge = true;
            }
            workSheet.Cells[recordIndex + 1, 6].Value = "Titolo";
            workSheet.Cells[recordIndex + 1, 7].Value = "Opzioni";
            workSheet.Cells[recordIndex, 8].Value = "ATTACHMENTS";
            workSheet.Cells[recordIndex, 9].Value = "EXPIRE TIME";


            int inizio = recordIndex;

            using (ExcelRange Titles = workSheet.Cells[recordIndex, 1, recordIndex + 1, 9])
            {
                //Titles.Style.Border.Right.Style = ExcelBorderStyle.MediumDashed;
                //Titles.Style.Border.Bottom.Color.SetColor(Color.Black);
                //Titles.Style.Border.Bottom.Style = ExcelBorderStyle.MediumDashed;
                //Titles.Style.Border.Bottom.Color.SetColor(Color.Black);
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

            recordIndex += 3;
            int i = recordIndex;
            workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
            workSheet.Cells[recordIndex, 2].Value = model.Id;
            workSheet.Cells[recordIndex, 3].Value = model.Name;
            workSheet.Cells[recordIndex, 4].Value = model.IdList;
            if (model.Labels.Count > 0)
                foreach (var item in model.Labels)
                {
                    workSheet.Cells[i, 5].Value = item.Name + " (" + item.Color + ")";
                    i++;
                }
            else
            {
                workSheet.Cells[i, 5].Value = "no Labels";
            }

            i = recordIndex;
            var j = 0;

            if (model.ChekedLists != null)
                foreach (var item in model.ChekedLists)
                {

                    workSheet.Cells[i, 6, fine, 6].Value = item.Name;
                    j = i;

                    foreach (var sol in item.CheckItems)
                    {

                        workSheet.Cells[i, 7].Value = sol.Name + " (" + sol.State + ")  ";
                        i++;
                    }
                    VerticalTitle(workSheet, i - 1, j);
                }
            else
            {
                workSheet.Cells[i, 7].Value = "no ChekedLists";
            }
            i = recordIndex;
            if (model.Attachments != null)
                foreach (var item in model.Attachments)
                {
                    workSheet.Cells[i, 8].Value = item.Name;
                    Uri url = new Uri(item.Url);
                    workSheet.Cells[i, 8].Hyperlink = url;
                    i++;
                }
            else
            {
                workSheet.Cells[i, 8].Value = "no Attachments";
            }
            workSheet.Cells[recordIndex, 9].Value = model.Due;
            using (ExcelRange Titles = workSheet.Cells[inizio + 2, 1, fine, 9])
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
        }

        private static void VerticalTitle(ExcelWorksheet workSheet, int fine, int i)
        {
            using (var title = workSheet.Cells[i, 6, fine, 6]) //funzione per unire più celle verticalmente
            {
                title.Merge = true;
                title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
        }
    }
}
}