namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insurance")]
    public partial class insurance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public insurance()
        {
            appointmentbookeds = new HashSet<appointmentbooked>();
            insuranceassignedinteractions = new HashSet<insuranceassignedinteraction>();
        }

        public long id { get; set; }

        public double? ODpremium { get; set; }

        [StringLength(30)]
        public string add_ON_Premium { get; set; }

        [StringLength(50)]
        public string coverageFromTo { get; set; }

        [StringLength(50)]
        public string coveragePeriod { get; set; }

        [StringLength(30)]
        public string discountPercentage { get; set; }

        public double idv { get; set; }

        [StringLength(100)]
        public string insuranceCompanyName { get; set; }

        [Column(TypeName = "bit")]
        public bool? isCurrent { get; set; }

        [StringLength(50)]
        public string lastCoverNoteNumber { get; set; }

        [StringLength(30)]
        public string lastRenewalBy { get; set; }

        public double liabilityPremium { get; set; }

        public float  ncBAmount { get; set; }

        [StringLength(30)]
        public string ncBPercentage { get; set; }

        public double? odAmount { get; set; }

        [StringLength(30)]
        public string odPercentage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyDueDate { get; set; }

        [StringLength(60)]
        public string policyNo { get; set; }

        public double premiumAmountAfterTax { get; set; }

        public double premiumAmountBeforeTax { get; set; }

        [StringLength(100)]
        public string renewalType { get; set; }

        [StringLength(30)]
        public string serviceTax { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        public long? customer_id { get; set; }

        public long? vehicle_id { get; set; }

        [StringLength(30)]
        public string add_ON_CoverPercentage { get; set; }

        [StringLength(30)]
        public string cubicCapacity { get; set; }

        [StringLength(60)]
        public string discValue { get; set; }

        public double exShowroomPrice { get; set; }

        public double totalODpremium { get; set; }

        [StringLength(30)]
        public string typeCode { get; set; }

        [StringLength(30)]
        public string vehicleAge { get; set; }

        [StringLength(30)]
        public string vehicleCity { get; set; }

        [StringLength(30)]
        public string vehicleZone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyIssueDate { get; set; }

        [StringLength(100)]
        public string policyType { get; set; }

        [StringLength(100)]
        public string classType { get; set; }

        [StringLength(100)]
        public string aPPremium { get; set; }

        [StringLength(100)]
        public string pAPremium { get; set; }

        [StringLength(100)]
        public string legalLiability { get; set; }

        [StringLength(100)]
        public string netLiability { get; set; }

        [StringLength(100)]
        public string addOn { get; set; }

        [StringLength(100)]
        public string otherPremium { get; set; }

        [StringLength(100)]
        public string netPremium { get; set; }

        [StringLength(100)]
        public string grossPremium { get; set; }

        [StringLength(100)]
        public string dealer { get; set; }

        [StringLength(45)]
        public string zone { get; set; }

        [StringLength(45)]
        public string reportedlocation { get; set; }

        [StringLength(45)]
        public string paymentType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? reneweddate { get; set; }

        [StringLength(100)]
        public string lastServiceType { get; set; }

        [StringLength(100)]
        public string paymentReference { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RiskInceptionDate { get; set; }

        public double InsuredDeclaredValue { get; set; }

        [StringLength(45)]
        public string AntitheftDeviceInstalled { get; set; }

        public double TPPDExtnPremium { get; set; }

        public double DriverCoverPremium { get; set; }
        [StringLength(60)]
        public string pacover { get; set; }
        [StringLength(60)]
        public string addOnOpted { get; set; }
        [StringLength(60)]
        public string totalIdv { get; set; }
        [StringLength(60)]
        public string vehicleClass { get; set; }
        [StringLength(60)]
        public string thirdPartyLiability { get; set; }
        public double ElectricalAccessoriesPremium { get; set; }

        public double NonElectricalAccessoriesPremium { get; set; }

        public double CngPremium { get; set; }

        public double ZeroDepPremium { get; set; }

        public double EngineCoverPremium { get; set; }

        public double RTIPremium { get; set; }

        public double PartAPremium { get; set; }

        public double PartBPremium { get; set; }
        //public double cancellation_uploadid { get; set; }

        public long addons { get; set; }

        [StringLength(45)]
        public string phonenumber { get; set; }

        [StringLength(255)]
        public string reportedlocname { get; set; }

        public string autodebitstatus { get; set; }

        public long cancellation_uploadid { get; set; }


        //chethan added for pullout
        public string cancelstatus { get; set; }
        public string cancelledremarks { get; set; }
        public string odcancelledremarks { get; set; }
        public string cancellationremarksbydealer { get; set; }
        public string dealerscancellationexecutive { get; set; }
        public string dealeroutletcity { get; set; }
        public string dealersexecutive { get; set; }
        public string proposalnumber { get; set; }
        public string financed { get; set; }
        public string voluntaryexcesslimit { get; set; }
        public string panoofperson { get; set; }
        public string paSumInsPerson { get; set; }
        public string Premium { get; set; }
        public string financecompany { get; set; }
        public string tppremium { get; set; }
        public string sixtyfourvbverifiedstatus { get; set; }
        public DateTime? cancelleddate { get; set; }
        public DateTime? odcancellationdate { get; set; }
        public long? showroomprice { get; set; }
        public long? tppdextnsumassured { get; set; }
        public string malusncbvalue { get; set; }
        public string consumablepremium { get; set; }
        public string rsapremium { get; set; }
        public string keyprotectpremium { get; set; }
        //Added for crm standardiazation
        public string pacoverpersonscovered { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }

        public virtual customer customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insuranceassignedinteraction> insuranceassignedinteractions { get; set; }

        public virtual vehicle vehicle { get; set; }
    }
}
