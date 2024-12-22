using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GmailHelp.Models
{
    public class Gmail
    {
        [Key]
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string PName { get; set; }
        public string CompanyName { get; set; }
    }
}