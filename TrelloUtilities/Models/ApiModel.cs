using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloUtilities.Models
{
    [Table("Api")]
    public class ApiModel
    {
        /// <summary>
        /// Api's properties
        /// </summary>
        [Key]
        [Required(ErrorMessage = "IdBoard is required!")]
        public string IdBoard { get; set; }
        [Required(ErrorMessage = "Token is required!")]
        public string Token { get; set; }
        [Required(ErrorMessage = "Key is required!")]
        public string Key { get; set; }

    }
}
