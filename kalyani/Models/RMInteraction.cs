using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("rminteraction")]
    public class RMInteraction
    {
        [Key]
        public long Id { get; set; }
        public long CompInteraction_id { get; set; }
        public long regionalmgr_id { get; set; }
        public string RMResolutionStatus { get; set; }
        public string RMRemarks { get; set; }
        public DateTime rmRaisedDateTime { get; set; }
        public int rmAttempts { get; set; }
        public long psfassignedinteraction_id { get; set; }
        public long calldisposition_id { get; set; }
        public long callinteraction_id { get; set; }
        public long customer_id { get; set; }
        public long vehicle_id { get; set; }

        public DateTime? resolved_datetime { get; set; }
        public long rmRaisedCompMgrId { get; set; }
        public string rmRaisedCompMgrName { get; set; }

        [Column(TypeName ="date")]
        public DateTime? FollowUpDate { get; set; }

        public string FollowUpTime { get; set; }

        public long workshopId { get; set; }
    }
}