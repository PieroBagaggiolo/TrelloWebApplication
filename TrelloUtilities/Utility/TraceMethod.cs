using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloUtilities.Models;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;

namespace TrelloUtilities.Utility
{
    public  class TraceMethod
    {
        private static DatabaseContext db = new DatabaseContext();

        public static void FillTracing(string text)
        {
            var myApi = PopolateModel.Crea();
            Tracing modifica = new Tracing();
           
            modifica.id = db.Tracings.Count();
            modifica.Board.IdBoard = myApi.idBrod;
            modifica.Event = text;
            //AGGIUNGO IL TRACING PER L'AZIONE DELETE DELLE CARD 
            db.Tracings.Add(modifica);
            db.SaveChanges();
        }
    }
}
