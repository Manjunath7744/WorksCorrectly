namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assignedcallsreport")]
    public partial class assignedcallsreport
    {
        public long id { get; set; }

        public long assignInteractionID { get; set; }

        public DateTime? assignedDate { get; set; }

        [StringLength(255)]
        public string assignmentType { get; set; }

        public long? moduletypeId { get; set; }

        public long? dueType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dueDate { get; set; }

        [StringLength(255)]
        public string uploadId { get; set; }

        public long vehicleId { get; set; }

        public long wyzuserId { get; set; }

        public long? campaignId { get; set; }

        public long? assigned_manager_id { get; set; }
        public bool isautoassigned { get; set; }
    }
}
