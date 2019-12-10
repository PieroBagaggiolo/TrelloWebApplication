using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloWebApplication.Models
{
    public class Webhook
    {
        public string Id { get; set; }
        public bool active { get; set; }
    }
}