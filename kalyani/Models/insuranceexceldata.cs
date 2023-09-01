namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insuranceexceldata")]
    public partial class insuranceexceldata
    {
        public long id { get; set; }

        public double? ODpremium { get; set; }

        [StringLength(250)]
        public string add_ON_Premium { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        [StringLength(250)]
        public string chassisNo { get; set; }

        [StringLength(250)]
        public string city { get; set; }

        [StringLength(250)]
        public string color { get; set; }

        [StringLength(250)]
        public string contactNo { get; set; }

        [StringLength(250)]
        public string coveragePeriod { get; set; }

        [StringLength(250)]
        public string customerName { get; set; }

        [StringLength(250)]
        public string discountPercentage { get; set; }

        [StringLength(250)]
        public string doa { get; set; }

        [StringLength(250)]
        public string dob { get; set; }

        [StringLength(250)]
        public string email { get; set; }

        [StringLength(250)]
        public string engineNo { get; set; }

        public double? idv { get; set; }

        [StringLength(250)]
        public string insuranceCompanyName { get; set; }

        [StringLength(250)]
        public string lastMileage { get; set; }

        [StringLength(250)]
        public string lastRenewalBy { get; set; }

        public double? liabilityPremium { get; set; }

        [StringLength(250)]
        public string model { get; set; }

        public double? ncBAmount { get; set; }

        [StringLength(250)]
        public string ncBPercentage { get; set; }

        public double? odAmount { get; set; }

        [StringLength(250)]
        public string odPercentage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyDueDate { get; set; }

        [StringLength(250)]
        public string policyNo { get; set; }

        public double? premiumAmountAfterTax { get; set; }

        public double? premiumAmountBeforeTax { get; set; }

        [StringLength(250)]
        public string renewalType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

        [StringLength(250)]
        public string serviceTax { get; set; }

        [StringLength(250)]
        public string state { get; set; }

        [StringLength(250)]
        public string variant { get; set; }

        [StringLength(250)]
        public string vehicleRegNo { get; set; }

        [StringLength(250)]
        public string location { get; set; }

        public long? uploadid { get; set; }

        [StringLength(45)]
        public string campaignName { get; set; }

        [StringLength(45)]
        public string workshop_id { get; set; }

        [StringLength(45)]
        public string campaignToDate { get; set; }

        [StringLength(45)]
        public string campaignFromDate { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }
    }
}
