using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("emailsender")]
    public class EmailSender
    {
        [Key]
        public long Id { get; set; }

        public string HostAPI { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        [Column(TypeName ="bit")]
        public bool IsActive { get; set; }

        public long PortNumber { get; set; }
    }
}