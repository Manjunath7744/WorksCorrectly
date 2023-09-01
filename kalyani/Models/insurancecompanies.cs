using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("insurancecompanies")]
    public class insurancecompanies
    {
        [Key]
        public int id { get; set; }

        [StringLength(225)]
        public string companyName { get; set; }
    }
}