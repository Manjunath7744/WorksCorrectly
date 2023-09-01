using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("emailreceipient")]
    public class EmailReceipient
    {
        [Key]
        public long Id { get; set; }

        public string ToEmailAddress { get; set; }

        public string CCEmailAddress { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }
    }
}