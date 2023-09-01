using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("eventemailinteraction")]
    public class EventEmailInteraction
    {
        [Key]
        public long Id { get; set; }

        public DateTime SentDate { get; set; }

        public string SentTime { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        [Column(TypeName = "bit")]
        public bool SentStatus { get; set; }
        
        public string GateWayResponse { get; set; }

        public string ToEmailAddress { get; set; }

        public string CCEmailAddress { get; set; }

        public string FromEmailAddress { get; set; }
    }
}