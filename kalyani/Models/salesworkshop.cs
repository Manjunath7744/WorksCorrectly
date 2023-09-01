using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("salesworkshop")]
    public class salesworkshop
    {
        [Key]
        public int id { get; set; }
        public string salesWorkshopname { get; set; }
        public long workshop_id { get; set; }
        public string dlrworkshop { get; set; }
        public string escalationMails { get; set; }
        public string escalationCC { get; set; }

    }
}