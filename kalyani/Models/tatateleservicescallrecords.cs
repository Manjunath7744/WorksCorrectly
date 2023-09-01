using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("tatateleservicescallrecords")]
    public class tatateleservicescallrecords
    {
        [Key]
        public int recordid { get; set; }
        public string id { get; set; }
        public string call_id { get; set; }
        public string uuid { get; set; }
        public string direction { get; set; }
        public string description { get; set; }
        public string detailed_description { get; set; }
        public string status { get; set; }
        public string blocked_number_id { get; set; }
        public string recording_url { get; set; }
        public string service { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string end_stamp { get; set; }
        public string broadcast_id { get; set; }
        public string dtmf_input { get; set; }
        public string call_duration { get; set; }
        public string answered_seconds { get; set; }
        public string minutes_consumed { get; set; }
        public string charges { get; set; }
        public string department_name { get; set; }
        public string agent_number { get; set; }
        public string agent_name { get; set; }
        public string client_number { get; set; }
        public string did_number { get; set; }
        public string reason { get; set; }
        public string hangup_cause { get; set; }
        public string notes { get; set; }
        public string contact_details { get; set; }
        public long callinteractionId { get; set; }
        public long callFor { get; set; }

    }
}