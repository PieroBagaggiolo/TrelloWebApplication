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
    public class PopolateModel
    {
        public static List<Card> Popola()
        {
            var cardtot = Api.Card();
            var listC = Api.List();
            foreach (var card in cardtot)
            {
                foreach (var list in listC)
                {
                    if (list.Id == card.IdList)
                    {
                        card.IdList = list.Name;
                    }
                }
            }
            foreach (var card in cardtot)
            {
                if (card.Badges.Attachments != "0")
                {

                    var allegato = Api.Img(card.Id);

                    card.Attachments = allegato;
                }
                int m = Int32.Parse(card.Badges.CheckItems);
                if (m > 0)
                {
                    var checklist = Api.Checked(card.Id);
                    card.ChekedLists = checklist;

                }
            }

         return cardtot;           
        }
    }
}