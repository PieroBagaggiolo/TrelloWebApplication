using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloWebApplication.Models
{
    public class ChekedList
    {
        public string IdCard { get; set; }
        public string Name { get; set; }
        public List<CheckItem> CheckItems { get; set; }
    }
}