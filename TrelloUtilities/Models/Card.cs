using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TrelloWebApplication.Models
{
    public class Card
    {
        public string Id { get; set; }
        public Badge Badges { get; set; }
        public string Closed { get; set; }
        public string Due { get; set; }
        public string IdList { get; set; }
        public List<Label> Labels { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<ChekedList> ChekedLists { get; set; }
        public string CommentTemp { get; set; }
        public bool prova200 { get; set; }
        public int NumberLabels
        {
            get
            {
                int i = 0;
                if (Labels!=null)
                {
                    foreach (var label in Labels)
                    {
                        i++;
                    }
                }
                
                return i;
            }
        }
        public int NumberChekItem
        {
            get
            {
                int i = 0;
                if (ChekedLists!=null)
                {
                    foreach (var cheked in ChekedLists)
                    {
                        foreach (var chekedItem in cheked.CheckItems)
                        {
                            i++;
                        }
                    }
                }
                return i;
            }
        }
    }
}