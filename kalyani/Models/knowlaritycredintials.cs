using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Windows.Input;

namespace AutoSherpa_project.Models
{
    [Table("knowlaritycredintials")]
    public class knowlaritycredintials
    {
        [Key]
            public int id { get; set; }
            public string apiType { get; set; }
            public string apiurl { get; set; }
            public string authorizationkey { get; set; }
            public string acceskey { get; set; }
            public string Channel { get; set; }
            public string knumber { get; set; }
    }
}