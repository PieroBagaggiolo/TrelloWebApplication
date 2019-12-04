
﻿using OfficeOpenXml;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;


namespace TrelloWebApplication.Controllers
{
    public class CardController : Controller
    {
        List<Card> model = PopolateModel.Popola();
        public ActionResult Index()
        {
            var model = PopolateModel.Popola();
            return View(model);
        }

        public ActionResult Details(string id = null)
        {
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == id)

                {
                    card = item;
                }
            }
            return View(card);
        }

        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("../Content/Details.pdf")
            };

        }
    }
}