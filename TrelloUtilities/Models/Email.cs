using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloUtilities.Models
{
    [Table("Emails")]
    public class Email
    {
        /// <summary>
        /// Email's properties
        /// </summary>
        [Key]
        public string SenderEmail { get; set; }
        [Required(ErrorMessage = "Sender's email required!")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 char")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 char")]
        public string ReceiverEmail { get; set; }

    }
}
