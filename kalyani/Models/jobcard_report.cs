namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobcard_report")]
    public partial class jobcard_report
    {
        [Key]
        public int serial_no { get; set; }

        [StringLength(100)]
        public string addressLine1 { get; set; }

        [StringLength(100)]
        public string addressLine2 { get; set; }

        [StringLength(100)]
        public string addressLine3 { get; set; }

        [StringLength(100)]
        public string advisorName { get; set; }

        [StringLength(100)]
        public string anniversary_date { get; set; }

        [StringLength(100)]
        public string billAmt { get; set; }

        [StringLength(100)]
        public string billDate { get; set; }

        [StringLength(100)]
        public string billNo { get; set; }

        [StringLength(100)]
        public string category_name { get; set; }

        [StringLength(45)]
        public string chassisNo { get; set; }

        [StringLength(100)]
        public string circularNo { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(100)]
        public string color { get; set; }

        [StringLength(100)]
        public string colorCode { get; set; }

        [StringLength(100)]
        public string country { get; set; }

        [StringLength(100)]
        public string customer_id { get; set; }

        [StringLength(100)]
        public string customerName { get; set; }

        [StringLength(100)]
        public string dob { get; set; }

        [StringLength(100)]
        public string engineNo { get; set; }

        [StringLength(100)]
        public string enquiryNo { get; set; }

        [StringLength(100)]
        public string estLabAmt { get; set; }

        [StringLength(100)]
        public string estPartAmt { get; set; }

        [StringLength(100)]
        public string fuelType { get; set; }

        [StringLength(100)]
        public string groupData { get; set; }

        [StringLength(100)]
        public string jobCardDate { get; set; }

        [StringLength(100)]
        public string jobCardNumber { get; set; }

        [StringLength(100)]
        public string jobCardStatus { get; set; }

        [StringLength(100)]
        public string labAmt { get; set; }

        [StringLength(100)]
        public string lastServiceDate { get; set; }

        [StringLength(100)]
        public string lastServiceType { get; set; }

        [StringLength(100)]
        public string mileage { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(100)]
        public string partsAmt { get; set; }

        [StringLength(100)]
        public string phoneNumber { get; set; }

        [StringLength(100)]
        public string pickupDate { get; set; }

        [StringLength(100)]
        public string pincode { get; set; }

        [StringLength(100)]
        public string promiseDate { get; set; }

        [StringLength(100)]
        public string psfStatus { get; set; }

        [StringLength(100)]
        public string readyDate { get; set; }

        [StringLength(100)]
        public string repeat_revisit { get; set; }

        [StringLength(100)]
        public string revisedPromiseDate { get; set; }

        [StringLength(100)]
        public string saleDate { get; set; }

        [StringLength(100)]
        public string serviceAdvisor_advisorId { get; set; }

        [StringLength(100)]
        public string state { get; set; }

        [StringLength(100)]
        public string status { get; set; }

        [StringLength(100)]
        public string technician { get; set; }

        [StringLength(100)]
        public string variant { get; set; }

        [StringLength(100)]
        public string Variant_Desc { get; set; }

        [StringLength(100)]
        public string variantCode { get; set; }

        [StringLength(100)]
        public string veh_customer_id { get; set; }

        [StringLength(100)]
        public string vehicle_id { get; set; }

        [StringLength(100)]
        public string vehicle_vehicle_id { get; set; }

        [StringLength(100)]
        public string vehicleRegNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? record_inserted_date { get; set; }

        public TimeSpan? record_inserted_time { get; set; }
    }
}
