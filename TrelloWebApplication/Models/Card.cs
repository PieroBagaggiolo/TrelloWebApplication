using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloWebApplication.Models
{
    public class Card
    {
        public string id { get; set; }
        public string status { get; set; }
        public DateTime scadenza { get;set; }

        public List<string> etichette { get; set; }
        public List<CheckElements> opzioni { get; set; }
        //label campi: name e color(?)
        
    }
}