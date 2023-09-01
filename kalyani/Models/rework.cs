namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rework")]
    public partial class rework
    {
        public long id { get; set; }

        [StringLength(150)]
        public string Benefits { get; set; }

        public long Compliant_Category_id { get; set; }

        public long DissatStatus_id { get; set; }

        public long campaign_id { get; set; }

        public long complaint_creid { get; set; }

        public long rm_creid { get; set; }

        public long customer_id { get; set; }

        public long discount { get; set; }

        [Column(TypeName = "bit")]
        public bool isReworkAvailable { get; set; }

        [Column(TypeName = "date")]
        public DateTime? issuedate { get; set; }

        public long location_id { get; set; }

        public long psfassignedInteraction_id { get; set; }

        public DateTime? resolutionDateAndTime { get; set; }

        [StringLength(150)]
        public string resolutionMode { get; set; }

        public DateTime? resolvedOn { get; set; }

        [StringLength(150)]
        public string resolvedBy { get; set; }

        [StringLength(150)]
        public string attendedBy { get; set; }

        public string VOC { get; set; }

        [StringLength(500)]
        public string reworkAddress { get; set; }

        public DateTime? reworkDateAndTime { get; set; }

        [StringLength(150)]
        public string reworkMode { get; set; }

        public string natureOfComplaint { get; set; }
        
        public long reworkStatus_id { get; set; }

        public long reworks_id { get; set; }

        public long vehicle_id { get; set; }

        public long workshop_id { get; set; }

        //For RM

        public bool isRMComplaintRaised { get; set; }

        public string RMResolutionStatus { get; set; }

        public string RMRemarks { get; set; }

        public DateTime? rmRaisedDateTime { get; set; }

        public long rmAttempts { get; set; }

        //For hans
        public bool isEscalatedToCEO { get; set; }

        public string visittype { get; set; }
    }
}
