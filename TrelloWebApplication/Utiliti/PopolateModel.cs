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
                if (card.Due != null)
                {
                    string newData = "";
                    foreach (var temp in card.Due)
                    {
                        if (temp.ToString() == ".")
                        {
                            break;
                        }

                        if (temp.ToString() != "T")
                        {
                            newData = newData + temp.ToString();
                        }
                        else
                        {
                            newData = newData + " Alle ore : ";
                        }

                    }

                    card.Due = newData;
                }
            }

         return cardtot;           
        }

    }
}