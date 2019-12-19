using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrelloWebApplication.Models
{
    public class Closed
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public Closed(string id)
        {
            if (id == "False")
            {
                this.Name = "No";
            }
            else
            {
                this.Name = "Si";
            }
            this.Id = id;
        }
    }
}