using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("gsmcallsynchdetails")]
    public class gsmcallsynchdetails
    {
        [Key]
        public int id { get; set; }

        public int limit { get; set; }

        [Column(TypeName = "date")]
        public DateTime? synchdate { get; set; }

        [Column(TypeName = "bit")]
        public bool status { get; set; }

        public string gsmapi { get; set; }
    }
}