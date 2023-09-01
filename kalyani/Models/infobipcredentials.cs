using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("infobipcredentials")]
    public class infobipcredentials
    {
        [Key]
        public int id { get; set; }
        public string infoBip_baseurl { get; set; }
        public string infoBip_key { get; set; }
        public string infoBip_username { get; set; }
        public string infoBip_password { get; set; }
        public bool isActive { get; set; }
    }
}