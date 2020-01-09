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
        public int Id { get; set; }

        [Required(ErrorMessage = "Sender's email required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string SenderEmail { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        
        public string Password { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
