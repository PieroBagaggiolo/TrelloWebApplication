using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloUtilities.Models
{
    [Table("Tracing")]
    public class Tracing
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("FKboardID")]
        public ApiModel Board { get; set; }
        public string Event { get; set; }
        public bool Check { get; set; }
    }
}
