using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TrelloWebApplication.Utiliti
{
    public static class CreazioneExl
    {
        public static void CreazioneFile(ExcelPackage ex, string title)
        {
            using (var memoryStream = new MemoryStream())
            {
                HttpContext cur = HttpContext.Current;
                cur.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //cur.Response.AddHeader("content-disposition", "attachment; filename=" + title + ".xlsx");
                ex.SaveAs(memoryStream);
                memoryStream.WriteTo(cur.Response.OutputStream);
                cur.Response.Flush();
                cur.Response.End();
            }
        }

    }
}