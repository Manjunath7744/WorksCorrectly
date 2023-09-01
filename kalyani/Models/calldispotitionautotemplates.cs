using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("calldispotitionautotemplates")]
    public class calldispotitionautotemplates
    {
        [Key]
        public int id { get; set; }
        public int dispositionid_fk { get; set; }
        public int smstemplateid { get; set; }
        public int emailtemplateid { get; set; }
        public int whatsapptemplateid { get; set; }
        public bool isenabledsmstemplate { get; set; }
        public bool isenabledemailtemplate { get; set; }
        public bool isenabledwhatsapptemplate { get; set; }
        public int roletype { get; set; }
    }
}