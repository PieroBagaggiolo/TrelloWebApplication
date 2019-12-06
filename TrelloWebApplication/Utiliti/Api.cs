using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using TrelloWebApplication.Models;

namespace TrelloWebApplication.Utiliti
{
    public  class Api
    {
        public  string key ;
        public  string token ;
        public  string idBrod;
        public string urlBoards = " https://api.trello.com/1/boards/";
        public string urlCard = "https://api.trello.com/1/cards/";
        public static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public  Api(string key, string token, string idBrod)
        {
            this.key = key;
            this.idBrod = idBrod;
            this.token = token;
        }
        public  List<Card> GetCard()
        {
            string cardN = ChiamataApi(urlBoards + idBrod + "/cards?key=" + key + "&token=" + token,"GET");
            string cardArchiviate = ChiamataApi(urlBoards + idBrod + "/cards/closed?key=" + key + "&token=" + token,"GET");        
            var Cardtot = serializer.Deserialize<List<Card>>(cardN);
            Cardtot.AddRange(serializer.Deserialize<List<Card>>(cardArchiviate));
            return Cardtot;
        }
        public  List<List> GetState()
        {
            string nomeList = ChiamataApi(urlBoards + idBrod + "/lists?key=" + key + "&token=" + token,"GET");
            var listC = serializer.Deserialize<List<List>>(nomeList);
            return listC;
        }

        public  List<Attachment> GetAttachment(string cardId)
        {
            string url = ChiamataApi(urlCard + cardId + "/attachments?key=" + key + "&token=" + token,"GET");
            var allegato = serializer.Deserialize<List<Attachment>>(url);
            return allegato;
        }

        public  List<ChekedList> GetCheckedList(string cardId)
        {
            string check = ChiamataApi(urlCard + cardId + "/checklists?key=" + key + "&token=" + token,"GET");
            var checklist = serializer.Deserialize<List<ChekedList>>(check);
            return checklist;
        }

        public  void AddComment(string comment, Card model)
        {
            string url = urlCard + model.Id+"/actions/comments?text="+comment+ "&key=" + key + "&token=" + token;
            ChiamataApi(url,"POST");
        }

        public static string ChiamataApi(string prov,string metodo)
        {
            WebRequest requestObj = WebRequest.Create(prov);
            requestObj.Method =metodo;
            HttpWebResponse responseObj = null;
            responseObj = (HttpWebResponse)requestObj.GetResponse();
            string result = null;
            using (Stream stream = responseObj.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                if (metodo=="GET")
                {
                    result = sr.ReadToEnd();
                }
               
                sr.Close();
            }
            return result;
        }
    }
}