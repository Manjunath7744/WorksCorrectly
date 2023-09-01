namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insuranceexcelerror")]
    public partial class insuranceexcelerror
    {
        public long id { get; set; }

        public double ODpremium { get; set; }

        [StringLength(255)]
        public string ODpremiumStr { get; set; }

        [StringLength(255)]
        public string add_ON_Premium { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string chassisNo { get; set; }

        [StringLength(255)]
        public string city { get; set; }

        [StringLength(255)]
        public string color { get; set; }

        [StringLength(255)]
        public string contactNo { get; set; }

        [StringLength(255)]
        public string coveragePeriod { get; set; }

        [StringLength(255)]
        public string customerName { get; set; }

        [StringLength(255)]
        public string discountPercentage { get; set; }

        [StringLength(255)]
        public string doa { get; set; }

        [StringLength(255)]
        public string dob { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string engineNo { get; set; }

        [StringLength(1073741823)]
        public string errorInformation { get; set; }

        public double idv { get; set; }

        [StringLength(255)]
        public string idvStr { get; set; }

        [StringLength(255)]
        public string insuranceCompanyName { get; set; }

        [Column(TypeName = "bit")]
        public bool isError { get; set; }

        [StringLength(255)]
        public string lastMileage { get; set; }

        [StringLength(255)]
        public string lastRenewalBy { get; set; }

        public double liabilityPremium { get; set; }

        [StringLength(255)]
        public string liabilityPremiumStr { get; set; }

        [StringLength(255)]
        public string model { get; set; }

        public float ncBAmount { get; set; }

        [StringLength(255)]
        public string ncBAmountStr { get; set; }

        [StringLength(255)]
        public string ncBPercentage { get; set; }

        public double odAmount { get; set; }

        [StringLength(255)]
        public string odAmountStr { get; set; }

        [StringLength(255)]
        public string odPercentage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyDueDate { get; set; }

        [StringLength(255)]
        public string policyDueDateStr { get; set; }

        [StringLength(255)]
        public string policyNo { get; set; }

        public double premiumAmountAfterTax { get; set; }

        [StringLength(255)]
        public string premiumAmountAfterTaxStr { get; set; }

        public double premiumAmountBeforeTax { get; set; }

        [StringLength(255)]
        public string premiumAmountBeforeTaxStr { get; set; }

        [StringLength(255)]
        public string renewalType { get; set; }

        public int rowNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

        [StringLength(255)]
        public string saleDateStr { get; set; }

        [StringLength(255)]
        public string serviceTax { get; set; }

        [StringLength(255)]
        public string state { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [StringLength(255)]
        public string variant { get; set; }

        [StringLength(255)]
        public string vehicleRegNo { get; set; }

        [StringLength(255)]
        public string dobStr { get; set; }
    }
}
