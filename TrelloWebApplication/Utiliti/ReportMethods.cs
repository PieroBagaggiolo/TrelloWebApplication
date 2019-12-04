﻿using OfficeOpenXml;
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
        public static void ExportSingleExcel(Card model)
        {
            string labels = "";
            
            foreach (var item in model.Labels)
            {
                labels = labels + item.Name + "(" + item.Color + ");/";
            }
            //creazione di un foglio EXCEL
            ExcelPackage ex = new ExcelPackage();
            var workSheet = ex.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //intestazione
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;


            workSheet.Cells[1, 1].Value = "#";
            workSheet.Cells[1, 2].Value = "ID";
            workSheet.Cells[1, 3].Value = "STATO";
            workSheet.Cells[1, 4].Value = "LABEL";
            workSheet.Cells[1, 5].Value = "CHECKLIST";
            workSheet.Cells[2, 5].Value = "Titolo";
            workSheet.Cells[2, 6].Value = "Opzioni";
            workSheet.Cells[1, 7].Value = "ATTACHMENTS";
            workSheet.Cells[1, 8].Value = "EXPIRE TIME";

            //Corpo della table
            int recordIndex = 3;
            int i = recordIndex;
            workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
            workSheet.Cells[recordIndex, 2].Value = model.Id;
            workSheet.Cells[recordIndex, 3].Value = model.IdList;
            if (model.Labels.Count > 0)
            foreach (var item in model.Labels)
            {
                workSheet.Cells[i, 4].Value = item.Name+"("+item.Color+")";
                i++;
            }
            
            i = recordIndex;
            
            if (model.ChekedLists != null)
            foreach (var item in model.ChekedLists)
            {
                workSheet.Cells[i, 5].Value = item.Name;
                foreach (var sol in item.CheckItems)
                {
                    
                    workSheet.Cells[i, 6].Value = sol.Name + "(" + sol.State + ")  ";
                    i++;
                }               
            }
            i = recordIndex;
            if (model.Attachments != null)
            foreach (var item in model.Attachments)
            {
                workSheet.Cells[i, 7].Value = item.Name+"Url :("+item.Url+")";
                i++;
            }
            
            workSheet.Cells[recordIndex, 8].Value = model.Due;

            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();

            string title = "Details";
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
    }
}