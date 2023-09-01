namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insurancedisposition")]
    public partial class insurancedisposition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public insurancedisposition()
        {
            appointmentbookeds = new HashSet<appointmentbooked>();
            upsellleads = new HashSet<upselllead>();
        }

        public long id { get; set; }

        [StringLength(70)]
        public string DistancefromDealerLocationRadio { get; set; }

        [StringLength(30)]
        public string InOutCallName { get; set; }

        public long assignedToSA { get; set; }

        [StringLength(70)]
        public string cityName { get; set; }

        [StringLength(500)]
        public string complaintOrFB_TagTo { get; set; }

        [StringLength(70)]
        public string coverNoteNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfRenewal { get; set; }

        [StringLength(70)]
        public string dealerName { get; set; }

        [StringLength(100)]
        public string departmentForFB { get; set; }

        [Column(TypeName = "date")]
        public DateTime? followUpDate { get; set; }

        [StringLength(30)]
        public string followUpTime { get; set; }

        [StringLength(100)]
        public string inBoundLeadSource { get; set; }

        [StringLength(500)]
        public string insuranceProvidedBy { get; set; }

        [StringLength(30)]
        public string isFollowUpDone { get; set; }

        [StringLength(30)]
        public string isFollowupRequired { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastRenewalDate { get; set; }

        [StringLength(70)]
        public string lastRenewedLocation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

       // [StringLength(70)]
        public string noServiceReasonTaggedTo { get; set; }

        //[StringLength(70)]
        public string noServiceReasonTaggedToComments { get; set; }

        [StringLength(30)]
        public string optforMMS { get; set; }

        [StringLength(70)]
        public string pinCodeOfCity { get; set; }

        [StringLength(70)]
        public string premimum { get; set; }

        [StringLength(500)]
        public string reason { get; set; }

        //[StringLength(500)]
        public string remarksOfFB { get; set; }

        [StringLength(70)]
        public string renewalDoneBy { get; set; }

        //[StringLength(500)]
        public string renewalNotRequiredReason { get; set; }

        [StringLength(70)]
        public string serviceAdvisorID { get; set; }

        [StringLength(70)]
        public string transferedCity { get; set; }

        [StringLength(70)]
        public string typeOfAutherization { get; set; }

        [StringLength(30)]
        public string typeOfDisposition { get; set; }

        [StringLength(70)]
        public string typeOfInsurance { get; set; }

        public long upsellCount { get; set; }

        public long? callDispositionData_id { get; set; }

        public long? callInteraction_id { get; set; }

        [StringLength(1073741823)]
        public string addOnsPrefered_OtherOptionsData { get; set; }

        [StringLength(1073741823)]
        public string addOnsPrefered_PopularOptionsData { get; set; }

        //[StringLength(1073741823)]
        public string comments { get; set; }

        //[StringLength(1073741823)]
        public string other { get; set; }

        [StringLength(70)]
        public string lastServiceType { get; set; }

        [StringLength(105)]
        public string paymentReference { get; set; }

        [StringLength(105)]
        public string paymentType { get; set; }

        [StringLength(500)]
        public string addons { get; set; }

        [StringLength(100)]
        public string appointeeName { get; set; }

        [StringLength(100)]
        public string nomineeAge { get; set; }

        [StringLength(100)]
        public string nomineeName { get; set; }

        //[StringLength(100)]
        public string nomineeRelationWithOwner { get; set; }

        //[StringLength(45)]
        public string othersINS { get; set; }

        public long supCREId { get; set; }

        //[StringLength(150)]
        public string policyDropAddress { get; set; }

        [StringLength(150)]
        public string policyPincode { get; set; }

        //[StringLength(500)]
        public string customerComments { get; set; }
        public long locationId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? policayDropDate { get; set; }
        [StringLength(45)]
        public string policayDropTime { get; set; }
        [StringLength(45)]
        public string feFireBaseKey { get; set; }
        public long insAgentId { get; set; }
        public long pickupId { get; set; }
        public string followupreasons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }

        public virtual calldispositiondata calldispositiondata { get; set; }

        public virtual callinteraction callinteraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upselllead> upsellleads { get; set; }
    }
}
