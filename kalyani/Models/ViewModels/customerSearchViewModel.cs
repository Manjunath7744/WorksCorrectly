using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    public class customerSearchViewModel
    {
        public long cid { get; set; }
        public string vehicle_id { get; set; }
        public string customerName { get; set; }
        public string vehicleRegNo { get; set; }
        public string customerCategory { get; set; }
        public string phoneNumber { get; set; }
        public string nextServicedate { get; set; }
        public string model { get; set; }
        public string variant { get; set; }
        public string serviceId { get; set; }
        public string chassisNo { get; set; }
        public string smr_finaldispo { get; set; }
        public string insu_finaldispo { get; set; }
        public string smr_assignId { get; set; }
        public string ins_assignId { get; set; }
        public string policyDueDate { get; set; }
        public string smr_assignedcre { get; set; }
        public string ins_assignedcre { get; set; }
        public string userControl { get; set; }

        public string userrole { get; set; }
        public string assignSearch { get; set; }

        //new for PolicyNumber search
       public string customer_id { get; set; }
        public string policyexpirydate { get; set; }
        public string renewalType { get; set; }
        public string lastpolicynumber { get; set; }
        public string lastinsuranceCompanyName { get; set; }
    }

    public class CustomerSearchPSF
    {
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string vehicleRegNo { get; set; }
        public string chassisNo { get; set; }
        public long vehicleid { get; set; }
        public long customer_id { get; set; }
        public long psfassignedInteraction_id { get; set; }
        public string billdate { get; set; }
        public string jobcardnumber { get; set; }
        public string lastdisposition { get; set; }
        public long finalDisposition_id { get; set; }
        public long wyzUser_id { get; set; }
        public long complaint_creid { get; set; }
        public string escallationdispo { get; set; }
        public long DissatStatus_id { get; set; }
        public long reworkStatus_id { get; set; }
        public long callinteraction_id { get; set; }

        public long campaignId { get; set; }

        public string campaignName { get; set; }
        public string AssignCre { get; set; }
        public string routingUrl { get; set; }
        public string escalationTo { get; set; }
        public string saleDate { get; set; }
        public string modelcat { get; set; }
    }
    public class SAcustomerSearch
    {
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string vehiclereg_no { get; set; }
        public string chassisno { get; set; }
        public string vehicleid { get; set; }
        public string customer_id { get; set; }
        public string roassignmentid { get; set; }
        public string rodate { get; set; }
        public string rostatus { get; set; }
        public string serviceadvisor_name { get; set; }
        public string wyzuser_id { get; set; }
        public string sainteraction_id { get; set; }
    }

}