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
    public class Api
    {
        static string key = "9936fabac5fdc5f00e46ff3a454e9feb";
        static string token = "27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01";
        static string idBrod = "5ddd5dad735c842669b7b819";
        static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public static List<Card> Card()
        {
            string cardN = ApiRest(" https://api.trello.com/1/boards/" + idBrod + "/cards?key=" + key + "&token=" + token);
            string cardArchiviate = ApiRest("https://trello.com/1/boards/" + idBrod + "/cards/closed?key=" + key + "&token=" + token);        
            var Cardtot = serializer.Deserialize<List<Card>>(cardN);
            Cardtot.AddRange(serializer.Deserialize<List<Card>>(cardArchiviate));
            return Cardtot;
        }
        public static List<List> List()
        {
            string nomeList = ApiRest("https://api.trello.com/1/boards/" + idBrod + "/lists?key=" + key + "&token=" + token);
            var listC = serializer.Deserialize<List<List>>(nomeList);
            return listC;
        }

        public static List<Attachment> Img(string cardId)
        {
            string url = ApiRest("https://api.trello.com/1/cards/" + cardId + "/attachments?key=" + key + "&token=" + token);
            var allegato = serializer.Deserialize<List<Attachment>>(url);
            return allegato;
        }

        public static List<ChekedList> Checked(string cardId)
        {
            string check = ApiRest("https://api.trello.com/1/cards/" + cardId + "/checklists?key=" + key + "&token=" + token);
            var checklist = serializer.Deserialize<List<ChekedList>>(check);
            return checklist;
        }

        private static string ApiRest(string prov)
        {
            WebRequest requestObj = WebRequest.Create(prov);
            requestObj.Method = "GET";
            HttpWebResponse responseObj = null;
            responseObj = (HttpWebResponse)requestObj.GetResponse();
            string result = null;

            using (Stream stream = responseObj.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}