using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("policyrenewallist")]

    public class policyrenewallist
    {
        public long? id { get; set; }
        public string sno { get; set; }
        public string dealer { get; set; }
        public string insurancecompany { get; set; }
        public string policytype { get; set; }
        public string mfgyear { get; set; }
        public DateTime ? policyexpirydate { get; set; }
        public string policyno { get; set; }
        public long? vehicleidv { get; set; }
        public long? totalpremium { get; set; }
        public string vehicleclass { get; set; }
        public string hypothecationdetail { get; set; }
        public string modelcode { get; set; }
        public string engineno { get; set; }
        public string chassisno { get; set; }
        public string vehregno { get; set; }
        public string insuredname { get; set; }
        public string address { get; set; }
        public string statename { get; set; }
        public string cityname { get; set; }
        public long? pin { get; set; }
        public long? mobilecountrycode { get; set; }
        public string mobileno { get; set; }
        public long? stdcode { get; set; }
        public string landlineno { get; set; }
        public string emailid { get; set; }
        public string producttype { get; set; }
        public long? istyrealloyoptd { get; set; }
        public long? isclaim { get; set; }
        public long? pendingclaim { get; set; }
        public long? paidclaim { get; set; }
        public long? rejectedclaims { get; set; }
        public long? totalclaims { get; set; }
        public long? claimamount { get; set; }
        public string renewed { get; set; }
        public string renewalpolicyno { get; set; }
        public long? renewalvehicleidv { get; set; }
        public long? renewaltotalpremium { get; set; }
        public string renewalinsurancecompany { get; set; }
        public string renewalproducttype { get; set; }
        public string renewalvehicleclass { get; set; }
        public long? renewalidv { get; set; }
        public long? essential { get; set; }
        public long? advance { get; set; }
        public long? premium { get; set; }
        public long? luxury { get; set; }
        public string workshop_id { get; set; }
        public string location { get; set; }
        public string campaignFromDate { get; set; }
        public string campaignToDate { get; set; }
        public string campaignName { get; set; }
        public string locType { get; set; }
        public string userid { get; set; }
        public long? uploadid { get; set; }

    }
}