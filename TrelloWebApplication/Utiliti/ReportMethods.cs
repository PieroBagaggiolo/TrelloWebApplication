using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrelloWebApplication.Models;

namespace TrelloWebApplication.Utiliti
{
    public class ReportClass
    {
        public static void ExportExcel(List<Card> model)
        {
            Api.Json(model);
            ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("Details");

            ws.Cells["A1"].Value = "";

        }
    }
}