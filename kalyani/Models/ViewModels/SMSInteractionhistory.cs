using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class SMSInteractionhistory
    {

        public string interactionDate { get; set; }
        public string interactionTime { get; set; }
        public string smsMessage { get; set; }
        public bool smsStatus { get; set; }
        public string CustomerName { get; set; }
        public string VehicleRegNo { get; set; }
        public string WyzuserName { get; set; }
        public string mobileNumber { get; set; }
        public string reason { get; set; }
        public string smsType { get; set; }
        public string toEmailAddress { get; set; }
        public string fromEmailAddress { get; set; }
        public string ccEmailAddress { get; set; }
        public string assignedId { get; set; }
        public DateTime? assignDate { get; set; }
        public string assignedBy { get; set; }
        // Chethan aded for new addon in insurance and smr assignment history
        public string reassignhistory { get; set; }
        public string reassign { get; set; }
        public string campaign { get; set; }
        public string emailsubject { get; set; }
        public string serviceTypes { get; set; }
        public string resonforDrop { get; set; }
        public DateTime? dateofdrop { get; set; }

    }

}