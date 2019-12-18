using OfficeOpenXml;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrelloUtilities;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;

namespace TrelloWebApplication.Controllers
{
    public class SelectController : Controller
    {
        static string Key = "9936fabac5fdc5f00e46ff3a454e9feb";
        static string Token = "27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01";
        static string IdBoard = "5ddd5dad735c842669b7b819";
        // creazione del mio modello di api per le chiamate
        static Api myApi = new Api(Key, Token, IdBoard);
        //creazione del modello di liste di card
        List<Card> model = PopolateModel.Popola(myApi);
        // GET: Select
        public ActionResult prova(string stato,List<Card> prov,string closed)
        {
            List<Card> cards = new List<Card>();
             
            List<Closed> closedList = new List<Closed>();
            closedList.Add(new Closed("False"));
            closedList.Add(new Closed("True"));
            ViewBag.Stato = new SelectList(myApi.GetState(), "Name", "Name");
            ViewBag.Closed = new SelectList(closedList, "Id", "Name");

            if ((stato != null && stato != "")||(closed!=null && closed!=""))
            {
                 foreach (var card in model)
                 {
                      if (card.IdList == stato || stato=="")
                      {
                           if (card.Closed == closed|| closed=="")
                           {
                                  cards.Add(card);
                           }
                      }
                 }
                return View(cards);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JavaScriptResult ProvaRequestUpdateListino( string lstString)
        {

            var script = string.Format("PageReload()");
            return JavaScript(script);
        }

        public ActionResult PdfIndex(string newModel)
        {
            List<String> result = System.Web.Helpers.Json.Decode<List<String>>(newModel);
            List<Card> cards = new List<Card>();
            foreach (var card in model)
            {
                foreach (var value in result)
                {
                    if (value == card.Id)
                    {
                        cards.Add(card);
                    }
                }
            }
            return View(cards);
        }


        public ActionResult ExportPDFIndex(string lstString)
        {
        
            ActionAsPdf ris = new ActionAsPdf("PdfIndex", new { newModel= lstString })
            {
                FileName = Server.MapPath("Index.pdf")
            };
            return ris;
        }
        /// <summary>
        /// creazione di un file exl con tutti i datti di tutte le card
        /// </summary>
        /// <returns>ritorna la view</returns>


        public ActionResult ExcelExIndex(string lstString)
        {
            List<String> result = System.Web.Helpers.Json.Decode<List<String>>(lstString);

            List<Card> cards = new List<Card>();
            foreach (var card in model)
            {
                foreach (var value in result)
                {
                    if (value==card.Id)
                    {
                        cards.Add(card);
                    }
                }
            }

            ExcelPackage ex = ReportMethods.ExportExcelTotal(cards);
            CreazioneExl.CreazioneFile(ex, "Index");
            return View();
        }
        public ActionResult View()
        {
            Card card = new Card();
            var stato = myApi.GetState();
            return View(card);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult View(Card card, string stato)
        {
            
            return RedirectToAction("Index", model);
        }


    }

}