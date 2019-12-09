using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;

namespace TrelloWebApplication.Controllers
{
    public class ReportController : Controller
    {
        List<Card> model = PopolateModel.Popola();
        public ActionResult ExportPDF()
        {
            ActionAsPdf result = new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("../Content/Details.pdf")
            };
            return result;
        }

        public ActionResult ExportExcel(string id = null)
        {
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == id)
                {
                    card = item;
                }
            }
            ReportMethods.ExportSingleExcel(card);
            return View(card);
        }
    }
}
