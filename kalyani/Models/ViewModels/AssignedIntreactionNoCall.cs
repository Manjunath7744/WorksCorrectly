using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class AssignedIntreactionNoCall
    {
        public string wyzUserName { get; set; }
        public string locations { get; set; }
        public string campaignName { get; set; }
        public string customerName { get; set; }
        public string chassisNo { get; set; }
        public string model { get; set; }
        public string vehicleRegNo { get; set; }
        public DateTime? billDate { get; set; }
        public DateTime? policyDueDate { get; set; }
        public DateTime? uploadDate { get; set; }
        public long noCallAssignId { get; set; }
    }
}