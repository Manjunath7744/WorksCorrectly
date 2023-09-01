using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("dataremoval")]
    public class dataremoval
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string firebasekey { get; set; }

        public bool removed { get; set; }

        public long vehicle_id { get; set; }

        public long customer_id { get; set; }

        public long insAssign_id { get; set; }

        public long appointmentId { get; set; }

        public bool updatedByApp { get; set; }

        public bool updatedByDb { get; set; }

        public bool isSynched { get; set; }

        public long inspolicydrop_id { get; set; }

        public DateTime? updatedDateTime { get; set; }
    }
}