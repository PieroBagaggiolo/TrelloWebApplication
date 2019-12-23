using OfficeOpenXml;
using Rotativa;
using System.Collections.Generic;
using System.Web.Mvc;
using TrelloWebApplication.Models;
using TrelloUtilities;
using TrelloWebApplication.Utiliti;
using TrelloMailReporter;
using TrelloMailReporter.MailScheduledJob;

namespace TrelloWebApplication.Controllers
{
    public class CardController : Controller
    {
        // elementi neccessari per fare le chiamate in caso di neccessita di potrebbe fare una view che le chieda al utente
        static string Key = "9936fabac5fdc5f00e46ff3a454e9feb";
        static string Token = "27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01";
        static string IdBoard = "5ddd5dad735c842669b7b819";
        // creazione del mio modello di api per le chiamate
        static Api myApi = new Api(Key, Token, IdBoard);
        //creazione del modello di liste di card
        List<Card> model = PopolateModel.Popola(myApi);

        /// <summary>
        /// visualizzia la lista di card predenti nella pagina trello
        /// </summary>
        /// <returns>ritorna una view</returns>
        public ActionResult Index(string stato)
        {

            List<Card> cards = new  List<Card>();
            ViewBag.Stato = new SelectList(myApi.GetState(), "Name", "Name");
            if (stato != null && stato != "")
            {
                foreach (var card in model)
                {
                    if (card.IdList== stato)
                    {
                        cards.Add(card);
                    }
                }
                return View(cards);
            }           
            return View(model);
        }
        /// <summary>
        /// crazione di una view senza il css da salvare al interno del file
        /// </summary>
        /// <returns></returns>
        public ActionResult PdfIndex()
        {
            return View(model);
        }

        /// <summary>
        /// creazione di un file pdf con la lista di card con i propi stati
        /// </summary>
        /// <returns>file pdf</returns>
        public ActionResult ExportPDFIndex()
        {
            ActionAsPdf result = new ActionAsPdf("PdfIndex",model)
            {
                FileName = Server.MapPath("Index.pdf")
            };
            return result;
        }
        /// <summary>
        /// per eliminare la card
        /// </summary>
        /// <param name="id">id della card da eliminare</param>
        /// <returns></returns>
        public ActionResult Delete(string id = null)
        {
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == id)

                {
                    card = item;
                }
            }
            myApi.DelateCard(card);
            return RedirectToAction("Index", model);
        }
        /// <summary>
        /// visualizazione dei dettagli di una card richiesti nella consegna
        /// </summary>
        /// <param name="id">id della card</param>
        /// <returns>ritorna una view</returns>
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
        public ActionResult newCard()
        {
            Card card = new Card();           
            var stato = myApi.GetState();
            ViewBag.Stato = new SelectList(stato, "Id", "Name", card.IdList);
            return View(card);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult newCard(Card card)
        {
            int i = 0;
            foreach (var list in myApi.GetState())
            {
                if (i==0)
                {
                    card.IdList = list.Id;
                }
                if (card.IdList.ToUpper() == list.Name.ToUpper())
                {                   
                    card.IdList = list.Id;
                    break;
                }
                i++;
            }
            myApi.PostCard(card);
            return RedirectToAction("Index", model);
        }
            /// <summary>
            /// Pagina di modifica card
            /// </summary>
            /// <param name="id">id card </param>
            /// <returns></returns>
            public ActionResult Edit(string id = null)
        {
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == id)

                {
                    card = item;
                }
            }
            var stato=myApi.GetState();
            ViewBag.Stato = new SelectList(stato, "Id", "Name",card.IdList);
            return View(card);
        }

        /// <summary>
        /// prende i dati di modifica controlla se sono uguali o inseribili e gli inserisce
        /// </summary>
        /// <param name="card">card con i nuovi dat</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Card card)
        {
            Card cardVecchia = null;
            foreach (var item in model)
            {
                if (item.Id == card.Id)

                {
                    cardVecchia = item;
                }
            }
            if (cardVecchia.Closed.ToUpper()!=card.Closed.ToUpper())
            {
                if (card.Closed.ToUpper()=="TRUE")
                {
                    myApi.PutClosed("true",card);
                }
                else
                {
                    myApi.PutClosed("false",card);
                }
            }
            if (cardVecchia.Name != card.Name)
            {
                myApi.PutName(card.Name,card);
            }
            foreach (var list in myApi.GetState())
            {
                if (card.IdList.ToUpper() == list.Name.ToUpper())
                {
                    myApi.PutList(list.Id,card);
                }
            }
            if (card.DueDate!= cardVecchia.DueDate)
            {
                myApi.PutDueDate(card.DueDate, card);
            }
            return RedirectToAction("Index",model);
        }

        /// <summary>
        /// crazione di una view senza il css da salvare al interno del file
        /// </summary>
        /// <returns></returns>
        public ActionResult PdfDetails(string id = null)
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
        /// <summary>
        /// creazione file exl con i dati di una card
        /// </summary>
        /// <param name="id">id della card</param>
        /// <returns>l view di prima</returns>
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
            CreazioneExl.CreazioneFile(ex, "Details");
            return View(card);
        }

        /// <summary>
        /// creazione di un file exl con tutti i datti di tutte le card
        /// </summary>
        /// <returns>ritorna la view</returns>
        public ActionResult ExcelExIndex()
        { 
            ExcelPackage ex = ReportMethods.ExportExcelTotal(model);
            CreazioneExl.CreazioneFile(ex, "Index");
            return View();
        }

        /// <summary>
        /// creazione di un file pdf con i dettagli di una card 
        /// </summary>
        /// <param name="id">id card</param>
        /// <returns>file pdf</returns>
        public ActionResult ExportPDFDetalis(string id=null)
        {
            Card card = null;
            foreach (var item in model)
            {
                if (item.Id == id)

                {
                    card = item;
                }
            }
            ActionAsPdf result = new ActionAsPdf("PdfDetails", card)
            {
                FileName = Server.MapPath("Details.pdf")
            };
            return result;
        }
        /// <summary>
        /// invio commento sulla card selezionata 
        /// </summary>
        /// <param name="pro">modello della card selezionata</param>
        /// <returns>ritorna alla stessa pagina con un alert di successo o insucesso del operazione</returns>
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
            card.CommentTemp = pro.CommentTemp;
            if (searchTerm!=null)
            {
                myApi.AddComment(searchTerm, pro);
            }
            return View("Details", card);
        }
    }
}