using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TrelloWebApplication.Models;

namespace TrelloUtilities
{
    public class Api
    {
        public string Key;
        public string Token;
        public string IdBoard;
        static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Api(string key, string token, string idBoard)
        {
            Key = key;
            Token = token;
            IdBoard = idBoard;
        }

        public List<Card> GetCard()
        {
            string cardN = ChiamataApi(" https://api.trello.com/1/boards/" + IdBoard + "/cards?key=" + Key + "&token=" + Token, "GET");
            string cardArchiviate = ChiamataApi("https://trello.com/1/boards/" + IdBoard + "/cards/closed?key=" + Key + "&token=" + Token, "GET");
            var cardTot = serializer.Deserialize<List<Card>>(cardN);
            cardTot.AddRange(serializer.Deserialize<List<Card>>(cardArchiviate));
            return cardTot;
        }
        public List<List> GetList()
        {
            string nomeList = ChiamataApi("https://api.trello.com/1/boards/" + IdBoard + "/lists?key=" + Key + "&token=" + Token, "GET");
            var listC = serializer.Deserialize<List<List>>(nomeList);
            return listC;
        }

        public List<Attachment> GetAttachment(string cardId)
        {
            string url = ChiamataApi("https://api.trello.com/1/cards/" + cardId + "/attachments?key=" + Key + "&token=" + Token, "GET");
            var allegato = serializer.Deserialize<List<Attachment>>(url);
            return allegato;
        }

        public List<ChekedList> GetChecked(string cardId)
        {
            string check = ChiamataApi("https://api.trello.com/1/cards/" + cardId + "/checklists?key=" + Key + "&token=" + Token, "GET");
            var checklist = serializer.Deserialize<List<ChekedList>>(check);
            return checklist;
        }

        public void AddComment(string comment, Card model)
        {
            string url = "https://api.trello.com/1/cards/" + model.Id + "/actions/comments?text=" + comment + "&key=" + Key + "&token=" + Token;
            ChiamataApi(url, "POST");
        }

        private string ChiamataApi(string prov, string metodo)
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
                sr.Close();
            }
            return result;
        }
    }
}
