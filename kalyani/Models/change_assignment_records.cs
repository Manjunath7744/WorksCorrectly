namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("change_assignment_records")]
    public partial class change_assignment_records
    {
        public long id { get; set; }

        public long assignedinteraction_id { get; set; }

        public long campaign_id { get; set; }

        public long last_wyzuserId { get; set; }

        public long new_wyzuserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime updatedDate { get; set; }

        public long? moduletypeIs { get; set; }

        [StringLength(100)]
        public string updatedType { get; set; }

        public long? assigned_manager_id { get; set; }
    }
}
