using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("gsmsynch")]
    public class gsmsynch
    {
        [Key]
        public int Id { get; set; }

        public long Callinteraction_id { get; set; }

        public string UniqueGsmId { get; set; }

        public DateTime CallMadeDateTime { get; set; }
    }
}