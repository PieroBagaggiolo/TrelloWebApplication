
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
        static string Key = "9936fabac5fdc5f00e46ff3a454e9feb";
        static string Token = "27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01";
        static string IdBoard = "5ddd5dad735c842669b7b819";

        static Api myApi = new Api(Key, Token, IdBoard);
        List<Card> model = PopolateModel.Popola(myApi);
        
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
            ExcelPackage ex = ReportMethods.ExportSingleExcel(card);
            CreazioneExl.CreazioneFile(ex, "foglio1");
            return View(card);
        }
        public ActionResult ExcelExIndex()
        { 
            ExcelPackage ex = ReportMethods.ExportExcelTotal(myApi);
            CreazioneExl.CreazioneFile(ex, "foglio1");
            return View();
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
            var searchTerm = pro.CommentTemp;
            if (searchTerm!=null)
            {
                myApi.AddComment(searchTerm, pro);
                ViewBag.Message = "Comment added succesfully";
                return View("Details", card);
            }
            ViewBag.Message = "Write something";
            return View("Details", card);
        }

    }

}
