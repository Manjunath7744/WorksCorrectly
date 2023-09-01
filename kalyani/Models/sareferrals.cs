using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace AutoSherpa_project.Models
{
    [Table("sareferrals")]
    public class sareferrals
    {
        public int id { get; set; }
        public DateTime calldate { get; set; }
        public string calltime { get; set; }
        public long wyzuser_id { get; set; }
        public string saname { get; set; }
        public DateTime? callmadeon { get; set; }
        public long roassignid { get; set; }
        public long vehicle_id { get; set; }
        public long customer_id { get; set; }
        public bool insurance { get; set; }
        public DateTime? policyexpirydate { get; set; }
        public long lastdiv { get; set; }
        public string inurancecompany { get; set; }
        public long lastncb { get; set; }
        public string comprehensive { get; set; }
        public string nilldip { get; set; }
        public string haanonhaa { get; set; }
        public bool new_car { get; set; }
        public string intrestedmodel { get; set; }
        public string intrestedvariant { get; set; }
        public string fueltype { get; set; }
        public string intrestedcolor { get; set; }
        public string exchange { get; set; }
        public string preferedoutlet { get; set; }
        public bool rsa { get; set; }
        public string rsacoverage { get; set; }
        public string rsaconverted { get; set; }
        public bool ew { get; set; }
        public string ewcoverage { get; set; }
        public string ewconverted { get; set; }
        //public bool self_referred { get; set; }
        public bool otherservice { get; set; }
        public string otherservicetype { get; set; }
        public string othercurrentmileage { get; set; }
        public bool referred_other { get; set; }
        public string customer_name { get; set; }
        public string mobile_number { get; set; }
        //public string car_model { get; set; }
        //public DateTime? othersaledate { get; set; }
        public string referral_remarks { get; set; }
    }
}