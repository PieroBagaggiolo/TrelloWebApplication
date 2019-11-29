using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloWebApplication.Models
{
    public class ChecList
    {
        public bool attivo { get; set; }
        public string descrizione { get; set; }
        public List<string> nomi { get; set; }
    }
}