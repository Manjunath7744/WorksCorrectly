using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class Email
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }

        public string Attachments { get; set; }

        public string EmailFrom { get; set; }

        public string Password { get; set; }

        public string UploadedUser { get; set; }

        public string EmailFor { get; set; } //Customer{or}Doc
        [NotMapped]
        public string EmailCC { get; set; } 
    }
}