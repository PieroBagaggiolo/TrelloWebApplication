
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
using TrelloWebApplication.Controllers;


namespace TrelloWebApplication.Controllers
{

    public class CardController : Controller
    {
        List<Card> model = PopolateModel.Popola();
        
        public ActionResult Index()
        {
             
            return View(model);
        }

        public ActionResult ExportPDF()
        {
            ActionAsPdf result = new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("../Content/Details.pdf")
            };
            return result;
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

        public ActionResult ExcelEx(string id = null)
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

        public ActionResult ExportPDFp(string id=null)
        {
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == id)

                {
                    card = item;
                }
            }
            ActionAsPdf result = new ActionAsPdf("Details",card)
            {
                FileName = Server.MapPath("../Content/Details.pdf")
            };
            return result;
        }
    }

}
