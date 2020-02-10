using System;
using System.Collections.Generic;
using System.Linq;
using TrelloUtilities.Models;
using TrelloWebApplication.Models;

namespace TrelloWebApplication.Utiliti
{
    public class PopolateModel
    {
        /// <summary>
        /// popolazione modello con i modelli presi con le api
        /// </summary>
        /// <param name="myApi">Il mio collegamento con le api trello</param>
        /// <returns>ritorno la lista di card con le opportune modifiche</returns>
        public List<Card> Popola()
        {
            var myApi = Crea();
            //lista di card presenti
            var cardtot = myApi.GetCards();
            //lista di stati delle card
            var listC = myApi.GetState();
            //giro la lista per sistemare uno alla volta tutte le card
            foreach (var card in cardtot)
            {
                
                //gioro la lista di stati per assegare alla card il suo stato tramite idlist  
                foreach (var list in listC)
                {
                    //controllo se closed ovvero l'atributo che fa capire se una chat è archiavita o meno nel caso che sia archoviata assegno alla 
                    //varibaile Idlist il valore "archiviata"

                        if (list.Id == card.IdList)
                        {
                            card.IdList = list.Name;
                        }
                }
                //controllo se sono presnti allegati 
                if (card.Badges.Attachments != "0")
                {
                    //se presenti creo una lista di allegati che assegno alla card
                    var allegato = myApi.GetAttachment(card.Id);

                    card.Attachments = allegato;
                }
                //trasformo il numero la quantità di checkitem 
                int m = Int32.Parse(card.Badges.CheckItems);
                //controllo se è maggiore di 0 in quel caso creo una lista di check list da assegnare alla card
                if (m > 0)
                {
                    var checklist = myApi.GetCheckedList(card.Id);
                    card.ChekedLists = checklist;

                }
                //controllo che la data di scadenza ci sia o nemo 
                // nel che ci sia gestisco la stringa rendendola datatime ed formatandola meglio
                if (card.Due != null)
                {
                    DateTime newDATA = DateTime.Parse(card.Due);
                    card.DueDate = newDATA;
                    string newDue = newDATA.ToString("F");
                    card.Due = newDue;
                }
            }
            //ritorno la lista di card con le modifiche 
            return cardtot;
        }
        public Api Crea()
        {
            DatabaseContext db = new DatabaseContext();
            ApiModel[] apiArrey = db.ApiModels.OrderBy(g => g.Primo).ToArray();
            ApiModel apiCredentials = apiArrey[0];
            // creazione del mio modello di api per le chiamate
            return new Api(apiCredentials.Key, apiCredentials.Token, apiCredentials.IdBoard);
        }
        
    }
}
