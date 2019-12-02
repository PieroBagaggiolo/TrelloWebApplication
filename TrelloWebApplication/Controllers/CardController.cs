using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TrelloWebApplication.Models;

//api neccesarie card : https://api.trello.com/1/card/[idcard]?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01

//allegati    https://api.trello.com/1/card/5ddd60afceb892734d8d82cc/attachments?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01
// api tutte card : https://api.trello.com/1/boards/5ddd5dad735c842669b7b819/cards?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01
//api card archiviate: https://trello.com/1/boards/5ddd5dad735c842669b7b819/cards/closed?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01
namespace TrelloWebApplication.Controllers
{
    public class CardController : Controller
    {
        public ActionResult Index()
        {
            string key = "9936fabac5fdc5f00e46ff3a454e9feb";
            string token = "27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01";
            string idBrod = "5ddd5dad735c842669b7b819";
            string cardN = ApiRest(" https://api.trello.com/1/boards/"+idBrod+"/cards?key="+key+"&token="+token);

            string cardArchiviate = ApiRest("https://trello.com/1/boards/"+ idBrod + "/cards/closed?key="+key+"&token="+token);

            string nomeList = ApiRest("https://api.trello.com/1/boards/"+idBrod+"/lists?key="+key+"&token="+token);
           // string chek = ApiRest("https://api.trello.com/1/boards/" + idBrod + "/customFields?key=" + key + "&token=" + token);


            var serializer = new JavaScriptSerializer();
            var listC = serializer.Deserialize<List<List>>(nomeList);
            var Cardtot = serializer.Deserialize<List<Card>>(cardN);
           // var cheked = serializer.Deserialize<List<Card>>(cardN);

            Cardtot.AddRange(serializer.Deserialize<List<Card>>(cardArchiviate));
            foreach (var card in Cardtot)
            {
                foreach (var list in listC)
                {
                    if (list.Id == card.IdList)
                    {
                        card.IdList = list.Name;
                    }
                }
            }
            foreach (var card in Cardtot)
            {
                int m = Int32.Parse(card.Badges.Attachments);
                if (m!=0)
                {
                    string url = ApiRest("https://api.trello.com/1/cards/" + card.Id + "/attachments?key="+key+ "&token=" + token);

                    var allegato = serializer.Deserialize<List<Attachment>>(url);
                    
                        card.Attachments=allegato;
                  
                    

                }
            }

            var x = 0;
            return View();
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