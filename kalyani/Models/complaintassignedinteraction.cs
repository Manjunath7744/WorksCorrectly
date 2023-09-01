namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("complaintassignedinteraction")]
    public partial class complaintassignedinteraction
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assignedDate { get; set; }

        [StringLength(255)]
        public string callMade { get; set; }

        [StringLength(20)]
        public string complaintStatus { get; set; }

        [StringLength(255)]
        public string isAssigned { get; set; }

        public long? assignedTo_id { get; set; }

        public long? complaint_id { get; set; }

        public virtual complaint complaint { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
