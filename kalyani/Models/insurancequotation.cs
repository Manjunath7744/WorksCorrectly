namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insurancequotation")]
    public partial class insurancequotation
    {
        public long id { get; set; }

        public double addOn { get; set; }

        public double addOnPercentage { get; set; }

        [StringLength(200)]
        public string commentIQ { get; set; }

        [StringLength(30)]
        public string createdCRE { get; set; }

        public long customer_id { get; set; }

        public double discountPercentage { get; set; }

        public double discountValue { get; set; }

        public double idv { get; set; }

        [StringLength(200)]
        public string insuranceCompany { get; set; }

        public double liabilityPremium { get; set; }

        public double ncbPercentage { get; set; }

        public double ncbRupees { get; set; }

        public double oDPremium { get; set; }

        public double odPercentage { get; set; }

        public double odRupees { get; set; }

        public double paPremium { get; set; }

        [Column(TypeName = "date")]
        public DateTime? qt_Date { get; set; }

        public double serviceTax { get; set; }

        public double thirdPartyPremium { get; set; }

        public double totalODPremium { get; set; }

        public double totalPremiumWithOutTax { get; set; }

        public double totalPremiumWithTax { get; set; }

        public long vehicle_id { get; set; }

        public DateTime? createdDate { get; set; }

        [StringLength(200)]
        public string insuranceCompanyQuotated { get; set; }

        [StringLength(1000)]
        public string msgURL { get; set; }

        [StringLength(45)]
        public string emailURL { get; set; }

        public double addon4 { get; set; }

        public double antitheft { get; set; }

        public double cng { get; set; }

        public double elec { get; set; }

        public double ep { get; set; }

        public double imt23 { get; set; }

        public double lp3 { get; set; }

        public double lp4 { get; set; }

        public double od3 { get; set; }

        public double od4 { get; set; }

        public double od5 { get; set; }

        public double otherAmt { get; set; }

        public double rti { get; set; }

        public double zp { get; set; }
        [NotMapped]
        public string status { get; set; }
        [NotMapped]
        public string response_transaction_id { get; set; }
    }
}
