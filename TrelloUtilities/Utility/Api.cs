using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using TrelloWebApplication.Models;

namespace TrelloWebApplication.Utiliti
{
    public class Api
    {
        public string key;
        public string token;
        public string idBrod;
        public string IdWebhook;
        public string urlBoards = " https://api.trello.com/1/boards/";
        public string urlCard = "https://api.trello.com/1/cards/";
        public static JavaScriptSerializer serializer = new JavaScriptSerializer();
        /// <summary>
        /// Costruttore del mio collegamento alle api
        /// </summary>
        /// <param name="key">La key per accedere alla berdobord di trello </param>
        /// <param name="token">il token per accedere alla berdobord di trello</param>
        /// <param name="idBrod">Id della berdobord dalla quale si vogliono prndere le informazioni</param>
        public Api(string key, string token, string idBrod)
        {
            this.key = key;
            this.idBrod = idBrod;
            this.token = token;
            //var Id = CreateWebhook();
            //foreach (var item in Id)
            //{
            //    this.IdWebhook = item.Id;
            //}
        }
        /// <summary>
        /// Manda le api alla funzione crea gli oggetti con quelle informazioni tramite il json deserializato
        /// </summary>
        /// <returns>Ritorna una lista completa di card comprese le archiviate</returns>
        public List<Card> GetCards()

        {
            //craazione stringhe json
            string cardN = ChiamtaApi(urlBoards + idBrod + "/cards?key=" + key + "&token=" + token, "GET");
            string cardArchiviate = ChiamtaApi(urlBoards + idBrod + "/cards/closed?key=" + key + "&token=" + token, "GET");
            //creazione di oggetti tramite il json
            var Cardtot = serializer.Deserialize<List<Card>>(cardN);
            Cardtot.AddRange(serializer.Deserialize<List<Card>>(cardArchiviate));
            return Cardtot;
        }
        /// <summary>
        /// Manda le api alla funzione crea gli oggetti con quelle informazioni tramite il json deserializato
        /// </summary>
        /// <returns>Ritorna una lista completa di stati </returns>
        public List<List> GetState()
        {
            string nomeList = ChiamtaApi(urlBoards + idBrod + "/lists?key=" + key + "&token=" + token, "GET");
            var listC = serializer.Deserialize<List<List>>(nomeList);
            return listC;
        }

        /// <summary>
        /// Manda le api alla funzione crea gli oggetti con quelle informazioni tramite il json deserializato
        /// </summary>
        /// <returns>Ritorna una lista completa di allegati </returns>
        public List<Attachment> GetAttachment(string cardId)
        {
            string url = ChiamtaApi(urlCard + cardId + "/attachments?key=" + key + "&token=" + token, "GET");
            var allegato = serializer.Deserialize<List<Attachment>>(url);
            return allegato;
        }

        /// <summary>
        /// Manda le api alla funzione crea gli oggetti con quelle informazioni tramite il json deserializato
        /// </summary>
        /// <returns>Ritorna una lista completa di cheked list </returns>
        public List<ChekedList> GetCheckedList(string cardId)
        {
            string check = ChiamtaApi(urlCard + cardId + "/checklists?key=" + key + "&token=" + token, "GET");
            var checklist = serializer.Deserialize<List<ChekedList>>(check);
            return checklist;
        }
        // Parte per crare un wobhook che mi averte di variazione nella berdbord

        //public  List<Webhook> CreateWebhook()
        //{
        //    string url = ("https://api.trello.com/1/webhooks/?idModel=" + idBrod + "&description='MyW'&callbackURL=http://localhost:53250/");
        //    string webh = ChiamtaApi(url, "POST");
        //    var webhook = serializer.Deserialize<List<Webhook>>(webh);
        //    return webhook;
        //}


        /// <summary>
        /// funzione che lancia il mettodo post per aggiungere un comento a una card
        /// </summary>
        /// <param name="comment">commento da inserire </param>
        /// <param name="model">card nella quale si deve inserire</param>
        public void AddComment(string comment, Card model)
        {
            string url = urlCard + model.Id + "/actions/comments?text=" + comment + "&key=" + key + "&token=" + token;
            ChiamtaApi(url, "POST");
        }

        /// <summary>
        /// Chiamata alle api trello con il salvataggio al interno di una stringa di un json contente tutte le informazioni della risposta alla richiesta 
        /// </summary>
        /// <param name="prov">chiamata da fare</param>
        /// <param name="metodo">tipo di chiamata che si desidera fare</param>
        /// <returns></returns>
        private static string ChiamtaApi(string prov, string metodo)
        {
            WebRequest requestObj = WebRequest.Create(prov);
            requestObj.Method = metodo;
            HttpWebResponse responseObj = null;
            responseObj = (HttpWebResponse)requestObj.GetResponse();
            string result = null;
            using (Stream stream = responseObj.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                if (metodo == "GET")
                {
                    result = sr.ReadToEnd();
                }
                //result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}