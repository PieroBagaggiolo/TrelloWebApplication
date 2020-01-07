using OfficeOpenXml;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TrelloUtilities
{
    
    public static class CreazioneExl
    {

        public static void CreazioneFile(ExcelPackage ex, string title)
        {
            using (var memoryStream = new MemoryStream())
            {
                HttpContext cur = HttpContext.Current;
                cur.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                cur.Response.AddHeader("content-disposition", "attachment ; filename=" + title + ".xlsx");
                ex.SaveAs(memoryStream);
                memoryStream.WriteTo(cur.Response.OutputStream);
                cur.Response.Flush();
                cur.Response.End();
            }
        }
    }
    
}
