using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    
    public class ChassiesNo
    {
        public long callinteraction_id { get; set; }
        public string agentName { get; set; }
        public DateTime callDate { get; set; }
        public string callDuration { get; set; }
        public string callTime { get; set; }
        public string callType { get; set; }
        public string customerPhone { get; set; }
        public string filePath { get; set; }
        public string ringTime { get; set; }
        public string uniqueIdForCallSync { get; set; }
        public string company_id { get; set; }
    }
}