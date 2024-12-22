using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GmailHelp.Models
{
    public class MailDBContext : DbContext
    {
        public MailDBContext() : base("ConStr")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MailDBContext>());
        }
        public DbSet<Gmail> gmail { get; set; }

    }
}