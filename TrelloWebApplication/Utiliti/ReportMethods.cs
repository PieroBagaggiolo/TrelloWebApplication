using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrelloWebApplication.Models;

namespace TrelloWebApplication.Utiliti
{
    public static class ReportClass
    {
        public static void ExportSingleExcel(Card model)
        {
            ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Details");

            ws.Cells["A1"].Value = "ID";
            ws.Cells["B1"].Value = "Stato";
            //ws.Cells["C1"].Value = "Label";
            //ws.Cells["D1"].Value = "Due Date";
           // ws.Cells["E1"].Value = "CheckList";
            int row = 2;
            ws.Cells[string.Format("A{0}", row)].Value = model.Id;
            ws.Cells[string.Format("B{0}", row)].Value = model.IdList;
           // ws.Cells[string.Format("C{0}", row)].Value = model.Labels;
            //ws.Cells
        }
    }
}