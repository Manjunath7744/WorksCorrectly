namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("appointmentbooked")]
    public partial class appointmentbooked
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public appointmentbooked()
        {
            callinteractions = new HashSet<callinteraction>();
        }

        [Key]
        public long appointmentId { get; set; }

        // [StringLength(200)]
        [AllowHtml]

        public string addressOfVisit { get; set; }

        //[StringLength(100)]
        public string appointeeName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? appointmentDate { get; set; }

        //[StringLength(100)]
        public string appointmentFromTime { get; set; }

        //[StringLength(100)]
        public string appointmentToTime { get; set; }

        //[StringLength(60)]
        public string dsa { get; set; }

        //[StringLength(100)]
        public string insuranceAgentData { get; set; }

        [StringLength(1073741823)]
        public string insuranceCompany { get; set; }

        //[StringLength(100)]
        public string nomineeAge { get; set; }

        //[StringLength(100)]
        public string nomineeName { get; set; }

        //[StringLength(100)]
        public string nomineeRelationWithOwner { get; set; }

        public double premiumwithTax { get; set; }

       //[StringLength(60)]
        public string renewalMode { get; set; }

       //[StringLength(60)]
        public string renewalType { get; set; }

       //[StringLength(60)]
        public string typeOfPickup { get; set; }

        public long? insuranceAgent_insuranceAgentId { get; set; }

        public long? insuranceDispositionId { get; set; }

        public long? showRooms_showRoom_id { get; set; }

       //[StringLength(255)]
        public string PremiumYes { get; set; }

        public long? customer_id { get; set; }

        public long? insurance_id { get; set; }

        public long? insuranceBookStatus_id { get; set; }

        public long? fieldWalkinLocation { get; set; }

        public long? pickupDrop_id { get; set; }

        public long? vehicle_id { get; set; }

        public long? wyzuser_id { get; set; }

        //[StringLength(100)]
        public string closed_insurance_id { get; set; }

        public long? chaserId { get; set; }

        ////[StringLength(100)]
        public string purpose { get; set; }

        //[StringLength(100)]
        public string discountValue { get; set; }

        //public long? discount { get; set; }

        ////[StringLength(100)]
        //public string coupons { get; set; }


       //[StringLength(150)]
        public string pincodeValue { get; set; }
        //[StringLength(50)]
        public string feFireBaseKey { get; set; }
        //[StringLength(100)]
        public string coupon { get; set; }
        public long lastinsuranceagentid { get; set; }
        //By Nisarga
        public string pincode { get; set; }
        //

        public string fieldsummary { get; set; }
        public long premiumwithdiscount { get; set; }

        public virtual insurance insurance { get; set; }

        public virtual insurancedisposition insurancedisposition { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual customer customer { get; set; }

        public virtual pickupdrop pickupdrop { get; set; }

        public virtual calldispositiondata calldispositiondata { get; set; }

        public virtual insuranceagent insuranceagent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        public virtual showroom showroom { get; set; }
    }
}
