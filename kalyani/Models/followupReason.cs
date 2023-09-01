using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
  
    [Table("followupreason")]
    public class followupReason
    {
        [Key]

        public int id { get; set; }
        public string followUpReason { get; set; }
        public bool isActive { get; set; }

    }
}