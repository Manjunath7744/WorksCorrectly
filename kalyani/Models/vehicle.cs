namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO.Packaging;

    [Table("vehicle")]
    public partial class vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vehicle()
        {
            assignedinteractions = new HashSet<assignedinteraction>();
            callinteractions = new HashSet<callinteraction>();
            complaints = new HashSet<complaint>();
            complaintinteractions = new HashSet<complaintinteraction>();
            driverinteractions = new HashSet<driverinteraction>();
            insurances = new HashSet<insurance>();
            insuranceassignedinteractions = new HashSet<insuranceassignedinteraction>();
            psf_qt_history = new HashSet<psf_qt_history>();
            psfassignedinteractions = new HashSet<psfassignedinteraction>();
            services = new HashSet<service>();
            serviceadvisorinteractions = new HashSet<serviceadvisorinteraction>();
            servicebookeds = new HashSet<servicebooked>();
            smsinteractions = new HashSet<smsinteraction>();
            upsellleads = new HashSet<upselllead>();
        }

        [Key]
        public long vehicle_id { get; set; }

        [StringLength(255)]
        public string Variant_Desc { get; set; }

        public double? addtional_Tax { get; set; }

        public long age_of_vehicle { get; set; }

        public double averageRunningPerDay { get; set; }

        public double basic_Price { get; set; }

        public DateTime? cancel_Date { get; set; }

        [StringLength(255)]
        public string cancel_No { get; set; }

        [StringLength(50)]
        public string chassisNo { get; set; }

        [StringLength(255)]
        public string city { get; set; }

        [StringLength(255)]
        public string clr_Desc { get; set; }

        [StringLength(100)]
        public string color { get; set; }

        [StringLength(60)]
        public string colorCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdDate { get; set; }

        public double csdEx_showroomDiscount { get; set; }

        [StringLength(60)]
        public string customerId { get; set; }

        public long daysBetweenVisit { get; set; }

        [StringLength(30)]
        public string dealershipName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? deliveryDate_Sale { get; set; }

        public double discount { get; set; }

        [StringLength(50)]
        public string district { get; set; }

        [StringLength(255)]
        public string dsa { get; set; }

        [StringLength(255)]
        public string dse { get; set; }

        [StringLength(255)]
        public string email_Id { get; set; }

        [StringLength(30)]
        public string engineNo { get; set; }

        [StringLength(60)]
        public string enquiryNo { get; set; }

        [StringLength(255)]
        public string enquirySource { get; set; }

        [StringLength(50)]
        public string evaluator_name_MSPIN { get; set; }

        [StringLength(255)]
        public string ewType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? exch_Canc_Date { get; set; }

        [StringLength(50)]
        public string exch_Canc_Reason { get; set; }

        [StringLength(30)]
        public string exchange { get; set; }

        [StringLength(255)]
        public string extWarrantyType { get; set; }

        [StringLength(40)]
        public string extendedWarentyDue { get; set; }

        [Column(TypeName = "date")]
        public DateTime? followUpDateSMR { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ispdate { get; set; }

        [StringLength(30)]
        public string forecastLogic { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fs1ExpiryDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fs2ExpiryDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fs3ExpiryDate { get; set; }

        [StringLength(50)]
        public string fuelType { get; set; }

        [StringLength(255)]
        public string hyp_Address { get; set; }

        public double? hyp_Amount { get; set; }

        [StringLength(255)]
        public string hypo { get; set; }

        public double? institutionalCustomer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? invDate { get; set; }

        [StringLength(10)]
        public string isAssigned { get; set; }

        [StringLength(30)]
        public string jobCardNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

        [StringLength(100)]
        public string lastServiceType { get; set; }

        public double? loyalty_bonusDiscount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? miDate { get; set; }

        [StringLength(50)]
        public string miFlag { get; set; }

        [StringLength(50)]
        public string miName { get; set; }

        [StringLength(255)]
        public string mobile1 { get; set; }

        [StringLength(255)]
        public string mobile2 { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        public DateTime? mul_Inv_Dt { get; set; }

        [StringLength(255)]
        public string mul_Inv_No { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServicedate { get; set; }

        [StringLength(100)]
        public string nextServicetype { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServiseDateBasedon_Running { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServisedateBasedon_Tenure { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServisedueBasedon_Tenure { get; set; }

        [StringLength(50)]
        
        public string odometerReading { get; set; }

        [Column(TypeName = "date")]
        public DateTime? odometerReadingDate { get; set; }

        [StringLength(255)]
        public string off_Phone { get; set; }

        [StringLength(50)]
        public string oldCar_regNum { get; set; }

        [StringLength(50)]
        public string old_Car_Relation { get; set; }

        [StringLength(50)]
        public string oldcar_Manufacturer { get; set; }

        [StringLength(50)]
        public string oldcar_Owner { get; set; }

        [StringLength(50)]
        public string oldcar_Status { get; set; }

        [StringLength(50)]
        public string oldcar_modelCode { get; set; }

        [StringLength(255)]
        public string pin_Desc { get; set; }
        [StringLength(255)]
        public string odpolicyno { get; set; }
        [StringLength(255)]
        public string tppolicyno { get; set; }
        [StringLength(255)]
        public string odpolicyexpiryDate { get; set; }
        [StringLength(255)]
        public string tppolicyexpirydate { get; set; }
        [StringLength(255)]
        public string pincode { get; set; }
        [StringLength(255)]
        public string producttype { get; set; }
        [StringLength(255)]
        public string vehicleClass { get; set; }
        [StringLength(255)]
        public string claimamount { get; set; }
        [StringLength(255)]
        public string hypothecationdetail { get; set; }
        [StringLength(255)]
        public string istyrealloyoptd { get; set; }

        [StringLength(50)]
        public string refVehicle_regnNo { get; set; }
        [StringLength(50)]
        public string totalclaims { get; set; }

        [StringLength(50)]
        public string ref_MobileNo { get; set; }

        [StringLength(50)]
        public string referenceType { get; set; }

        [StringLength(50)]
        public string referredBy { get; set; }

        [StringLength(50)]
        public string refferenceNo { get; set; }

        [StringLength(255)]
        public string res_Phone { get; set; }

        public double runningBetweenvisits { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

        [StringLength(30)]
        public string salesChannel { get; set; }

        public double? schemeof_GOI { get; set; }

        [StringLength(50)]
        public string stateDesc { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        [StringLength(255)]
        public string stdCode_comp { get; set; }

        [StringLength(255)]
        public string std_code { get; set; }

        [StringLength(255)]
        public string teamLead { get; set; }

        [StringLength(50)]
        public string tehsilDesc { get; set; }

        public double? valueadded_Tax { get; set; }

        [StringLength(60)]
        public string variant { get; set; }

        [StringLength(60)]
        public string variantCode { get; set; }

        [StringLength(50)]
        public string vehicleNumber { get; set; }

        [StringLength(50)]
        public string vehicleRegNo { get; set; }

        [StringLength(50)]
        public string villageDesc { get; set; }

        public long? customer_id { get; set; }

        public long? location_cityId { get; set; }

        public long? outlets_id { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? forecasted_duedate { get; set; }

        [StringLength(100)]
        public string forecasted_duetype { get; set; }

        [StringLength(100)]
        public string system_forecast_logic { get; set; }

        [StringLength(100)]
        public string currentPriority { get; set; }

        public double dealerDisount { get; set; }

        [StringLength(100)]
        public string delivaryNoteNumber { get; set; }

        public double finanaceAmount { get; set; }

        [StringLength(100)]
        public string finanaceBranch { get; set; }

        [StringLength(100)]
        public string financier { get; set; }

        [StringLength(100)]
        public string initialPriority { get; set; }

        public double invoiceAmount { get; set; }

        [StringLength(100)]
        public string kitNo { get; set; }

        [StringLength(100)]
        public string modelFamilyCode { get; set; }

        [StringLength(100)]
        public string modelGroup { get; set; }

        public DateTime? oTFDate { get; set; }

        [StringLength(100)]
        public string oTFNo { get; set; }

        public DateTime? oemInvoiceDate { get; set; }

        [StringLength(100)]
        public string oemInvoiceNo { get; set; }

        public double oemSchemaDiscount { get; set; }

        [StringLength(100)]
        public string priceType { get; set; }

        public long seating { get; set; }

        public double sellingPrice { get; set; }

        public double totalCashRecieved { get; set; }

        [Column(TypeName = "date")]
        public DateTime? purchaseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyDueDate { get; set; }

        [StringLength(100)]
        public string nextRenewalType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyFollowUpDue { get; set; }

        [StringLength(255)]
        public string insuranceCompanyName { get; set; }

        [StringLength(100)]
        public string branchName { get; set; }

        [StringLength(100)]
        public string corporateType { get; set; }

        [StringLength(100)]
        public string typeOfProgram { get; set; }

        [StringLength(100)]
        public string regno_chassisNo { get; set; }

        [Column(TypeName = "bit")]
        public bool commercialVehicle { get; set; }

        [StringLength(50)]
        public string averageRunning { get; set; }

        [StringLength(100)]
        public string brand { get; set; }

        public int insduelocation_id { get; set; }

        [StringLength(45)]
        public string insfcupload_id { get; set; }

        [StringLength(100)]
        public string hotAlertOpenDate { get; set; }

        [StringLength(100)]
        public string contractEndDate { get; set; }

        [StringLength(100)]
        public string esbProduct { get; set; }

        [StringLength(100)]
        public string isSalesDone { get; set; }

        public long previouslastMileage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? previouslastvisitdate { get; set; }

        [StringLength(100)]
        public string systemDueType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? systemForecastedDate { get; set; }

        [Column(TypeName = "bit")]
        public bool uploadType3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? forecastUpdatedData { get; set; }

        [StringLength(100)]
        public string vipDealer { get; set; }

        [Column(TypeName = "bit")]
        public bool? uploadType1 { get; set; }

        [Column(TypeName = "bit")]
        public bool? uploadType2 { get; set; }

        public long? vehicleWorkshop_id { get; set; }

        public int? yearofmanufacture { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FSA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PMS_missed { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Non_Retained { get; set; }

        public long? smrfcupload_id { get; set; }

        //by nisarga fro pull factor
        public string mcpStatus { get; set; }
        public string loyaltycard1 { get; set; }

        public long Modelcat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OEMWarrentyDate { get; set; }

        public string LMC { get; set; }

        [StringLength(45)]
        public string loyaltycardnumber { get; set; }

        public DateTime? loyaltybalancedate { get; set; }

        public long claimcount { get; set; }

        //chethan aded
        public DateTime? policy_updateddate { get; set; }
        public long policy_updatedby { get; set; }
        public string tpstatus { get; set; }
        public DateTime? duedatetenure { get; set; }
        public DateTime? duedatemileage { get; set; }
        //Chenna Added
        [Column(TypeName = "date")]
        public DateTime? PolicyDueDateEdited { get; set; }
        [StringLength(50)]
        public string chassis { get; set; }
        public DateTime? updateddate { get; set; }
        public string updatecrename { get; set; }
        public DateTime? ewenddate { get; set; }
        public DateTime? amcenddate { get; set; }
        public string emissionnorms { get; set; }
        public string warrantyexpirykm { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignedinteraction> assignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaint> complaints { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintinteraction> complaintinteractions { get; set; }

        public virtual customer customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverinteraction> driverinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insurance> insurances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insuranceassignedinteraction> insuranceassignedinteractions { get; set; }

        public virtual location location { get; set; }

        public virtual outlet outlet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psf_qt_history> psf_qt_history { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfassignedinteraction> psfassignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<service> services { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisorinteraction> serviceadvisorinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<smsinteraction> smsinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upselllead> upsellleads { get; set; }
    }
}
