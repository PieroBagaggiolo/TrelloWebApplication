using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//api neccesarie : https://api.trello.com/1/card/[idcard]/checklists?key=9936fabac5fdc5f00e46ff3a454e9feb&token=27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01
namespace TrelloWebApplication.Controllers
{
    public class CardController : Controller
    {
        public ActionResult Index()
        {
            

            return View(cards);
        }

    }
}