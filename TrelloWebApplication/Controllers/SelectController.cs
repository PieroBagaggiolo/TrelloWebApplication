using Newtonsoft.Json;
using OfficeOpenXml;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        

        /// <summary>
        /// Pagina di filtraggio dati stato e closed sono le DropDownList dove viene scelto per cosa filtrare
        /// </summary>
        /// <param name="stato">DropDownList della lista degli stati </param>
        /// <param name="closed">DropDownList del archivazione se si o no </param>
        /// <returns></returns>
        public ActionResult Filter(string stato,  string closed)
        {
            List<Card> cards = new List<Card>();

            List<Closed> closedList = new List<Closed>();
            closedList.Add(new Closed("False"));
            closedList.Add(new Closed("True"));
            ViewBag.stato = new SelectList(myApi.GetState(), "Name", "Name");
            ViewBag.closed = new SelectList(closedList, "Id", "Name");

            if ((stato != null && stato != "") || (closed != null && closed != ""))
            {
                foreach (var card in model)
                {
                    if (card.IdList == stato || stato == "")
                    {
                        if (card.Closed == closed || closed == "")
                        {
                            cards.Add(card);
                        }
                    }
                }
                return View(cards);
            }
            return View(model);
        }




        /// <summary>
        /// Salvataggio e creazoione del pdf di una lista di elmenti filtrata con i campi qui sopra elencati  
        /// </summary>
        /// <param name="newModel">json di una lista di id degli elmenti filtrati</param>
        /// <returns>ritorna la view</returns>
        public ActionResult PdfIndex(string newModel)
        {
            // rinconversione da json a lista di stringhe
            List<String> result = System.Web.Helpers.Json.Decode<List<String>>(newModel);
            List<Card> cards = new List<Card>();
            //popolamento di una lista di card con id uguale a quelli presenti nella varibile result
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
        /// creazione di un file exl con tutti i datti di tutte le card filtrate
        /// </summary>
        /// <param name="lstString">json di una lista di id degli elmenti filtrati</param>
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
        /// <summary>
        /// View per spostamenti di massa tra le vari stati
        /// </summary>
        /// <param name="lstString">json di una lista di id degli elmenti filtrati</param>
        /// <returns></returns>
        public ActionResult Sposta(string lstString)
        {
            List<String> result = System.Web.Helpers.Json.Decode<List<String>>(lstString);
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
            ViewBag.Stato = new SelectList(myApi.GetState(), "Name", "Name");
            ViewBag.stato = new SelectList(myApi.GetState(), "Name", "Name");
            return View(cards);
        }

        /// <summary>
        /// Grazie alle due variabili passate del jquarry fa lo spostemnto delle card nel campo selezionato in precedenza
        /// </summary>
        /// <param name="idlistino">id new stato dove spostare le card</param>
        /// <param name="jsonids">json di liste di id delle card da spostare</param>
        /// <returns>ritorna un refrech della pagina </returns>
        [HttpPost]
        public JavaScriptResult SpostaCard(string idlistino, string jsonids)
        {
            //deserilizazione della varibile jsonids
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            MemoryStream stream = new MemoryStream(uniEncoding.GetBytes(jsonids));
            stream.Position = 0;
            List<String> result = System.Web.Helpers.Json.Decode<List<String>>(jsonids);
            List<Card> cards = new List<Card>();
            //creazione lista di card con id presennte e con stato differnte al nuovo
            foreach (var value in result)
            {
                cards.AddRange(model.Where(g =>  g.Id == value & g.IdList!=idlistino));
            }
            //recupero id del idListino
            string idList = "";
            foreach (var item in myApi.GetState())
            {
                if (item.Name == idlistino)
                {
                    idList = item.Id;
                }
            }
            //lancio la funzione spostemnto di massa 
            myApi.PutMassa(cards, idList);
            var script = string.Format("PageReload()");
            return JavaScript(script);
        }
    }

}