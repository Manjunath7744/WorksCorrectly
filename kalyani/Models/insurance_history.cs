namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insurance_history")]
    public partial class insurance_history
    {
        public int id { get; set; }

        [StringLength(100)]
        public string insuredName { get; set; }

        [StringLength(100)]
        public string campaignToDate { get; set; }

        [StringLength(100)]
        public string campaignFromDate { get; set; }

        [StringLength(100)]
        public string grossPremium { get; set; }

        [StringLength(100)]
        public string tax { get; set; }

        [StringLength(100)]
        public string netPremium { get; set; }

        [StringLength(100)]
        public string otherPremium { get; set; }

        [StringLength(100)]
        public string addOnPremium { get; set; }

        [StringLength(100)]
        public string addOn { get; set; }

        [StringLength(100)]
        public string netLiability { get; set; }

        [StringLength(100)]
        public string legalLiability { get; set; }

        [StringLength(100)]
        public string paPremium { get; set; }

        [StringLength(100)]
        public string tpPremium { get; set; }

        [StringLength(100)]
        public string oDPremium { get; set; }

        [StringLength(100)]
        public string discount { get; set; }

        [StringLength(100)]
        public string nCBValue { get; set; }

        [StringLength(100)]
        public string nCBpercentage { get; set; }

        [StringLength(100)]
        public string basicOD { get; set; }

        [StringLength(100)]
        public string oDPercentage { get; set; }

        [StringLength(100)]
        public string iDV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dueDate { get; set; }

        [Column("class")]
        [StringLength(100)]
        public string _class { get; set; }

        [StringLength(100)]
        public string policytype { get; set; }

        [StringLength(100)]
        public string insuranceCompany { get; set; }

        [StringLength(100)]
        public string policyNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyIssueDate { get; set; }

        [StringLength(100)]
        public string color { get; set; }

        [StringLength(100)]
        public string vehicleRegNo { get; set; }

        [StringLength(100)]
        public string saleDate { get; set; }

        [StringLength(100)]
        public string fuelType { get; set; }

        [StringLength(100)]
        public string vehicleType { get; set; }

        [StringLength(100)]
        public string variant { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(100)]
        public string engineNo { get; set; }

        [StringLength(100)]
        public string chassisNo { get; set; }

        [StringLength(100)]
        public string dob { get; set; }

        [StringLength(100)]
        public string doa { get; set; }

        [StringLength(100)]
        public string eMailID { get; set; }

        [StringLength(100)]
        public string landlineNo { get; set; }

        [StringLength(100)]
        public string sTDCode { get; set; }

        [StringLength(100)]
        public string mobileNo { get; set; }

        [StringLength(100)]
        public string state { get; set; }

        [StringLength(250)]
        public string city { get; set; }

        [StringLength(250)]
        public string pincode { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        [StringLength(100)]
        public string dealer { get; set; }

        [StringLength(250)]
        public string location_id { get; set; }

        [StringLength(250)]
        public string workshop_id { get; set; }

        public long? uploadid { get; set; }
    }
}
