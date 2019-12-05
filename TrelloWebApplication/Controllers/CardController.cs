
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
            ActionAsPdf result = new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("../Content/Details.pdf")
            };
            return result;
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

        public ActionResult Prova(string id=null, string commentoProva = null)
        {
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == id)

                {
                    card = item;
                }
            }
             Api.AddComment(commentoProva,card);
            return View();
        }


        [HttpPost]
        public ActionResult Details(Card pro)
        {         
            
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == pro.Id)

                {
                    card = item;
                }
            }
            var searchTerm = card.CommentTemp;
            if (searchTerm!=null)
            {
                Api.AddComment(searchTerm, pro);
                return View("Details", card);
            }

            return View("Details", card);
        }

    }
}