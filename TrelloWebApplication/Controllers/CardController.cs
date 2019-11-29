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
// api tutte card : https://api.trello.com/1/boards/5ddd5dad735c842669b7b819/cards?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01
namespace TrelloWebApplication.Controllers
{
    public class CardController : Controller
    {
        public ActionResult Index()
        {

            string prov = ("https://api.trello.com/1/card/5ddd60afceb892734d8d82cc?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01");
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

            string prov1 = ("https://api.trello.com/1/card/5ddd60afceb892734d8d82cc/attachments?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01");
            WebRequest requestObj1 = WebRequest.Create(prov1);
            requestObj1.Method = "GET";
            HttpWebResponse responseObj1 = null;
            responseObj1 = (HttpWebResponse)requestObj1.GetResponse();
            string result1 = null;

            using (Stream stream = responseObj1.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result1 = sr.ReadToEnd();
                sr.Close();
            }

            // deserialize data. After deserialization, our object json will be 
            // populated with information from JSON file
            var serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<Card>(result);
            var j = serializer.Deserialize<List<Attachment>>(result1);
            return View();
        }

    }
}