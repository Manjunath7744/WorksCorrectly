using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{

    public class PSFFilterVM
    {
        public DateTime? ExFromBillDate { get; set; }
        public DateTime? ExToBillDate { get; set; }
        public string Ageing { get; set; }
        public string Attempts { get; set; }
        public string ReworkMode { get; set; }
        public string Workshop { get; set; }
        public string bucketId { get; set; }
        public string campaigntypeTypeDDL { get; set; }
        public bool isFiltered { get; set; }
        public string serviceType { get; set; }
        public string customerCategory { get; set; }
    }

    public class PSFIndusVM
    {
        public long DissatStatus_id { get; set; }
        public string campaignName { get; set; }
        public string customer_name { get; set; }
        public string Mobile_number   { get; set; }
        public string chassisno { get; set; }
        public string vehicle_RegNo { get; set; }
        public string workshop { get; set; }
        public string RONumber { get; set; }//job card number
        public DateTime? issuedate { get; set; }
        public int ageing   { get; set; }
        public DateTime? psffollowupdate { get; set; }
        public string psffollowuptime { get; set; }
        public long calldispositiondata_id { get; set; }
        public DateTime? aptdate { get; set; }
        public string apttime { get; set; }
        public string reworkMode  { get; set; }
        public long attempts { get; set; }
        public DateTime? BillDate { get; set; }
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public long complaint_creid { get; set; }
        public long reworkStatus_id { get; set; }
        public long callinteraction_id { get; set; }
        public string psfassignedInteraction_id { get; set; }
        public string aptstatus { get; set; }
        public string Last_disposition { get; set; }
        public string servicetype { get; set; }
        public string customercategory { get; set; }
        public string saname { get; set; }

    }
}