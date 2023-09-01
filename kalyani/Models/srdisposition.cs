namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("srdisposition")]
    public partial class srdisposition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public srdisposition()
        {
            upsellleads = new HashSet<upselllead>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string DistancefromDealerLocationRadio { get; set; }

        [StringLength(30)]
        public string InOutCallName { get; set; }

        [StringLength(255)]
        public string ServicedAtOtherDealerRadio { get; set; }

        [StringLength(255)]
        public string assignedToSA { get; set; }

        [StringLength(255)]
        public string checkedwithDMS { get; set; }

        [StringLength(255)]
        public string cityName { get; set; }

        //[StringLength(500)]
        public string comments { get; set; }

        [StringLength(100)]
        public string complaintOrFB_TagTo { get; set; }

        [StringLength(255)]
        public string dateOfService { get; set; }

        [StringLength(255)]
        public string dealerName { get; set; }

        //[StringLength(100)]
        public string departmentForFB { get; set; }

        [Column(TypeName = "date")]
        public DateTime? followUpDate { get; set; }

        [StringLength(255)]
        public string followUpTime { get; set; }

        [StringLength(100)]
        public string inBoundLeadSource { get; set; }

        [StringLength(255)]
        public string isFollowUpDone { get; set; }

        [StringLength(255)]
        public string isFollowupRequired { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDateOfDWPS { get; set; }

        [StringLength(255)]
        public string mileageAsOnDate { get; set; }

        [StringLength(255)]
        public string mileageAtService { get; set; }

        //[StringLength(70)]
        public string noServiceReason { get; set; }

        [StringLength(70)]
        public string noServiceReasonTaggedTo { get; set; }

        //[StringLength(500)]
        public string noServiceReasonTaggedToComments { get; set; }

        //[StringLength(255)]
        public string optforMMS { get; set; }

        //[StringLength(500)]
        public string other { get; set; }

        [StringLength(255)]
        public string pinCodeOfCity { get; set; }

        //[StringLength(255)]
        public string reason { get; set; }

        //[StringLength(70)]
        public string reasonForHTML { get; set; }

        //[StringLength(250)]
        public string remarksOfFB { get; set; }

        [StringLength(255)]
        public string serviceAdvisorID { get; set; }

        [StringLength(255)]
        public string serviceLocation { get; set; }

        [StringLength(255)]
        public string serviceType { get; set; }

        [StringLength(255)]
        public string transferedCity { get; set; }

        [StringLength(30)]
        public string typeOfDisposition { get; set; }

        public long? callDispositionData_id { get; set; }

        public long? callInteraction_id { get; set; }

        public long upsellCount { get; set; }

        public double currentMileage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expectedVisitDate { get; set; }

        //[StringLength(500)]
        public string remarks { get; set; }

        [StringLength(45)]
        public string cityOfDistanceFromDealerLocation { get; set; }

        [StringLength(100)]
        public string followUpReason { get; set; }

        public long supCREId { get; set; }

        //[StringLength(45)]
        public string othersSMR { get; set; }
        public string authorised_Or_Not { get; set; }
        public string stdremarks { get; set; }

        public virtual calldispositiondata calldispositiondata { get; set; }

        public virtual callinteraction callinteraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upselllead> upsellleads { get; set; }
    }
}
