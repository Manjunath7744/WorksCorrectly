using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace AutoSherpa_project.Models
{
    [Table("changeAssignmentPending")]
    public class changeAssignmentPending
    {
        [Key]
        public long id { get; set; }
        public long assignedInteractionId { get; set; }
        public long serviceBookedId { get; set; }
        public long moduleType { get; set; }
        public long newWyzuserId { get; set; }
        public long updatedById { get; set; }//logged in wyzuser id
        public long uploadId{ get; set; }
        public DateTime updatedDate { get; set; }
        public bool updatedStatus { get; set; }

    }
}