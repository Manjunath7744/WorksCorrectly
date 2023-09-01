using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AutoSherpa_project.Models
{
   
    [Table("callsyncdata")]
    public partial class callsyncdata
    {
        public long id { get; set; }

        [StringLength(255)]
        public string callDuration { get; set; }

        [Column(TypeName = "bit")]
        public bool callDurationUpdated { get; set; }

        public DateTime? callMadeDateAndTime { get; set; }

        [StringLength(255)]
        public string callType { get; set; }

        public long callinteraction_id { get; set; }

        [StringLength(255)]
        public string dailedNumber { get; set; }

        [StringLength(500)]
        public string filepath { get; set; }

        [Column(TypeName = "bit")]
        public bool isComplaintCall { get; set; }

        public long moduletype { get; set; }

        [StringLength(255)]
        public string ringTime { get; set; }

        [StringLength(255)]
        public string uniqueidForCallSync { get; set; }

        public DateTime? updatedDate { get; set; }

        public long? wyzuser_id { get; set; }

        public long? csidmap { get; set; }

        [Column(TypeName = "bit")]
        public bool? isgsmsdata { get; set; }
    }
}
