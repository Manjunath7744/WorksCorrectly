namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("service")]
    public partial class service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public service()
        {
            psfassignedinteractions = new HashSet<psfassignedinteraction>();
        }

        public long id { get; set; }

        public double? billAmt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? billDate { get; set; }

        [StringLength(20)]
        public string billNo { get; set; }

        [StringLength(30)]
        public string billNumber { get; set; }

        [StringLength(30)]
        public string circularNo { get; set; }

        [StringLength(30)]
        public string createdBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdDate { get; set; }

        [StringLength(30)]
        public string custCat { get; set; }

        [StringLength(100)]
        public string customerName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfOdometerReading { get; set; }

        public double? estLabAmt { get; set; }

        public double? estPartAmt { get; set; }

        [StringLength(30)]
        public string groupData { get; set; }

        [StringLength(30)]
        public string isAssigned { get; set; }

        public DateTime? jobCardDate { get; set; }

        [StringLength(30)]
        public string jobCardNumber { get; set; }

        [StringLength(30)]
        public string jobCardStatus { get; set; }

        public double? labAmt { get; set; }

        public double? labourBasic { get; set; }

        public double? labourCharges { get; set; }

        public double? labourDiscount { get; set; }

        [StringLength(30)]
        public string lastPSFStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

        [StringLength(30)]
        public string lastServiceMeterReading { get; set; }

        [StringLength(30)]
        public string lastServiceType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastVisitDate { get; set; }

        [StringLength(10)]
        public string lastVisitMeterReading { get; set; }

        [StringLength(30)]
        public string lastVisitMileage { get; set; }

        [StringLength(30)]
        public string lastVisitType { get; set; }

        [StringLength(30)]
        public string mcpNo { get; set; }

        [StringLength(30)]
        public string messageSentStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServiceDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServiceDue { get; set; }

        [StringLength(30)]
        public string nextServiceType { get; set; }

        public double? partBasic { get; set; }

        public double? partCharges { get; set; }

        public double? partDiscount { get; set; }

        public double? partsAmt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? pickupDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? promiseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psf1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psf2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psf3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psf4 { get; set; }

        [StringLength(10)]
        public string psfStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? readyDate { get; set; }

        [StringLength(30)]
        public string repeat_revisit { get; set; }

        [Column(TypeName = "date")]
        public DateTime? revisedPromiseDate { get; set; }

        public double? roundOffamt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

        [StringLength(30)]
        public string selling { get; set; }

        public int? serviceNoShowPeriod { get; set; }

        [StringLength(10)]
        public string serviceOdometerReading { get; set; }

        [StringLength(30)]
        public string serviceType { get; set; }

        [StringLength(30)]
        public string status { get; set; }

        [StringLength(30)]
        public string technician { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updatedDate { get; set; }

        [StringLength(25)]
        public string uploadDataFiletype { get; set; }

        [StringLength(30)]
        public string visitSuccessRate { get; set; }

        public long? serviceAdvisor_advisorId { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? workshop_id { get; set; }

        public long? upload_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? ftps { get; set; }

        public double? inhouseLabourBasic { get; set; }

        public double? inhouseLabourDiscount { get; set; }

        public double? inhouseLabourTax { get; set; }

        public double? inhouseLabourTotal { get; set; }

        public double? localAccessoriesBasic { get; set; }

        public double? localAccessoriesDiscount { get; set; }

        public double? localAccessoriesTax { get; set; }

        public double? localAccessoriesTotal { get; set; }

        public double? localPartsBasic { get; set; }

        public double? localPartsDiscount { get; set; }

        public double? localPartsTax { get; set; }

        public double? localPartsTotal { get; set; }

        public double? maxiCareBasic { get; set; }

        public double? maxiCareDiscount { get; set; }

        public double? maxiCareTax { get; set; }

        public double? maxiCareTotal { get; set; }

        public double? maximileBasic { get; set; }

        public double? maximileDiscount { get; set; }

        public double? maximileTax { get; set; }

        public double? maximileTotal { get; set; }

        public double? mfgAccessoriesBasic { get; set; }

        public double? mfgAccessoriesDiscount { get; set; }

        public double? mfgAccessoriesTax { get; set; }

        public double? mfgAccessoriesTotal { get; set; }

        public double? mfgPartsBasic { get; set; }

        public double? mfgPartsDiscount { get; set; }

        public double? mfgPartsTax { get; set; }

        public double? mfgPartsTotal { get; set; }

        public double? oilConsumablesBasic { get; set; }

        public double? oilConsumablesDiscount { get; set; }

        public double? oilConsumablesTax { get; set; }

        public double? oilConsumablesTotal { get; set; }

        public double? outLabourBasic { get; set; }

        public double? outLabourDiscount { get; set; }

        public double? outLabourTax { get; set; }

        public double? outLabourTotal { get; set; }

        [StringLength(45)]
        public string billCreatedBy { get; set; }

        [StringLength(45)]
        public string billType { get; set; }

        [StringLength(20)]
        public string jobcardLocation { get; set; }

        [StringLength(30)]
        public string saName { get; set; }

        [StringLength(30)]
        public string serviceCategory { get; set; }

        [StringLength(30)]
        public string suprevisor { get; set; }

        [StringLength(50)]
        public string OEMprivilageCust { get; set; }

        [StringLength(50)]
        public string RoSource { get; set; }

        [StringLength(50)]
        public string billStatus { get; set; }

        [StringLength(50)]
        public string campaignCode { get; set; }

        [StringLength(50)]
        public string dropType { get; set; }

        [StringLength(50)]
        public string expressService { get; set; }

        [StringLength(50)]
        public string loyaltyCardNo { get; set; }

        [StringLength(100)]
        public string menuCodeDesc { get; set; }

        [StringLength(50)]
        public string originalWarrantystartDate { get; set; }

        [StringLength(10)]
        public string uploadType1 { get; set; }

        [StringLength(10)]
        public string uploadType2 { get; set; }

        [StringLength(10)]
        public string uploadType3 { get; set; }

        [StringLength(50)]
        public string pickUpType { get; set; }

        [StringLength(50)]
        public string purpleCouponNo { get; set; }

        [StringLength(50)]
        public string purpleCouponYesNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? rsaExpiryDate { get; set; }

        [StringLength(50)]
        public string rsaSchemeCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? shieldExpiryDate { get; set; }

        [StringLength(50)]
        public string shieldschemeCode { get; set; }

        [StringLength(50)]
        public string taxiNonTaxi { get; set; }

        [StringLength(50)]
        public string udayNo { get; set; }

        [StringLength(20)]
        public string originalWarrantyDate { get; set; }

        [StringLength(45)]
        public string phonenumber { get; set; }

        [StringLength(50)]
        public string serviceDealerName { get; set; }

        [StringLength(500)]
        public string defectDetails { get; set; }

        [StringLength(500)]
        public string laborDetails { get; set; }

        [StringLength(1500)]
        public string jobCardRemarks { get; set; }
        public string Lastservicemeterreading { get; set; }
        //sureshaddes for katariatruck
        public string kminhours { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfassignedinteraction> psfassignedinteractions { get; set; }

        public virtual vehicle vehicle { get; set; }

        public virtual serviceadvisor serviceadvisor { get; set; }

        public virtual workshop workshop { get; set; }
    }
}
