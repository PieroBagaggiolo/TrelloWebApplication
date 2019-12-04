using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
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

            //creazione di un foglio EXCEL
            ExcelPackage ex = new ExcelPackage();
            var workSheet = ex.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //intestazione
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
        }
    }
}