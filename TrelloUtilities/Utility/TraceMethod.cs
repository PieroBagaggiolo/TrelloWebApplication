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
        private DatabaseContext db = new DatabaseContext();
        PopolateModel popMod = new PopolateModel();
        //PopolateModel popMod;
        //private DatabaseContext db;

        //public TraceMethod(PopolateModel popMod, DatabaseContext db)
        //{
        //    this.popMod = popMod;
        //    this.db = db;

        //}

        public void FillTracing(string text)
        {
            Api myApi = popMod.Crea();
            Tracing modifica = new Tracing();         
            modifica.id = db.Tracings.Count();
            modifica.FKboardID = myApi.idBrod;
            modifica.Event = text;
            //AGGIUNGO IL TRACING PER L'AZIONE DELETE DELLE CARD 
            db.Tracings.Add(modifica);
            db.SaveChanges();
        }

        public void CheckEvents()
        {
            Api myApi = popMod.Crea();
            //variabile controllo che le modifiche inerenti alla board su cui mi trovo per non inviare 
            //la mail in caso ci siano modifiche su una board non inerente
            var checkkiamo = db.Tracings.ToList().Where(g => g.FKboardID == myApi.idBrod && g.Check == false);

            //Una volta inviata la mail i valori check della tabella tracing diventato Chekkate (non verranno più inviate)
            foreach (var chk in checkkiamo)
            {
                chk.Check = true;
                db.SaveChanges();
            }
        }
    }
}
