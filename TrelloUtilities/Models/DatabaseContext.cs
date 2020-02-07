using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloUtilities.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ApiModel>ApiModels { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Tracing> Tracings { get; set; }
        public DatabaseContext() :
            base("DefaultConnection") { }
    }
}
