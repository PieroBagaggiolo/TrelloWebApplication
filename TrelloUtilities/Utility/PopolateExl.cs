using OfficeOpenXml;
using OfficeOpenXml.Style;
using TrelloWebApplication.Models;

namespace TrelloUtilities
{
    public class PopolateExl
    {
        public static void Riempimento(Card model, ExcelWorksheet workSheet, int recordIndex)
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

            if (model.ChekedLists != null)
                foreach (var item in model.ChekedLists)
                {
                    workSheet.Cells[i, 6].Value = item.Name;
                    foreach (var sol in item.CheckItems)
                    {

                        workSheet.Cells[i, 7].Value = sol.Name + " (" + sol.State + ")  ";
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
                    workSheet.Cells[i, 8].Value = item.Name + " Url: ( " + item.Url + " )";
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