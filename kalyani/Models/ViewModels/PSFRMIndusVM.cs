using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class PSFRMIndusVM
    {
        public string customerName { get; set; }
        public string vehicleRegNo { get; set; }
        public string chassisno { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? billdate { get; set; }
        public string jobcardnumber { get; set; }//job card number
        public DateTime? issuedate { get; set; }
        public string last_comp_manager { get; set; }
        public string callid { get; set; }
        public DateTime? followupdate { get; set; }
        public string FollowUptime { get; set; }
        public long psfassignedInteraction_id { get; set; }
        public long vehicle_id { get; set; }
        public long customer_id { get; set; }
        


        public string workshop { get; set; }
        public string Last_disposition { get; set; }
        public string sub_disposition{ get; set; }
        public string campaignName { get; set; }
        public string complaint_crename { get; set; }

        public string saName { get; set; }


    }
    public class PSFRMfilters
    {
        public DateTime? fromIssueDate { get; set; }
        public DateTime? toIssueDate { get; set; }
        public int lastDisposition { get; set; }
        public string workshop { get; set; }
        public string complaintCreId { get; set; }
        public bool isFiltered { get; set; }
    }
}