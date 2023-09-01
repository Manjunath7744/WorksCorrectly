using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("postsalescompinteraction")]
    public class postsalescompinteraction
    {
        [Key]
        public long Id { get; set; }
        public string Benefits { get; set; }
        public long DissatStatus_id { get; set; }
        public long campaign_id { get; set; }
        public long complaint_creid { get; set; }
        public long customer_id { get; set; }
        public long vehicle_id { get; set; }
       // public bool isReworkAvailable { get; set; }
        public DateTime issueDateTime { get; set; }
        public long? location_id { get; set; }
        public long psfassignedInteraction_id { get; set; }
        public long calldisposition_id { get; set; }
        public long callinteraction_id { get; set; }
     //   public DateTime? resolutionDateAndTime { get; set; }
        public string resolutionMode { get; set; }
        public string resolvedBy { get; set; }
       // public string reworkAddress { get; set; }
        //public DateTime? reworkDateAndTime { get; set; }
        //public string reworkMode { get; set; }
        //public string attendedBy { get; set; }
        public long? workshop_id { get; set; }

        //public long discount { get; set; }

        public DateTime? resolvedOn { get; set; }
        //public string natureOfComplaint { get; set; }
        //public bool isRMComplaintRaised { get; set; }
        //public string demandedrepairsCompleted { get; set; }
        //public string promiseddeliveryTime { get; set; }
        //public string washingQuality { get; set; }
        //public string vehicleserviceCharges { get; set; }
        //public string visittype { get; set; }
        public int bucket_id { get; set; }
        public bool isDailed { get; set; }
        public long? Compliant_Category_id { get; set; }

        public long compRaisedCreId { get; set; }

        public string compRaisedCreName { get; set; }

    }
}