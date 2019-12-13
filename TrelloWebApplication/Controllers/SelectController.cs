﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Filter(string stato)
        {
            List<Card> cards = new List<Card>();
            ViewBag.Stato = new SelectList(myApi.GetState(), "Name", "Name");
            if (stato != null && stato != "")
            {
                foreach (var card in model)
                {
                    if (card.IdList == stato)
                    {
                        cards.Add(card);
                    }
                }
                return View(cards);
            }
            return View(model);
        }
    }
}