namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrdataerror")]
    public partial class smrdataerror
    {
        public long id { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        [StringLength(1073741823)]
        public string address { get; set; }

        [StringLength(100)]
        public string chassis { get; set; }

        [StringLength(100)]
        public string currentJC { get; set; }

        [StringLength(100)]
        public string custCat { get; set; }

        [StringLength(100)]
        public string custContactStatus { get; set; }

        [StringLength(100)]
        public string custEmailIdStatus { get; set; }

        [StringLength(100)]
        public string custName { get; set; }

        [StringLength(100)]
        public string custPickUpType { get; set; }

        [StringLength(100)]
        public string deliveryDate { get; set; }

        [StringLength(100)]
        public string dueDate { get; set; }

        [StringLength(100)]
        public string dueService { get; set; }

        [StringLength(1073741823)]
        public string errorInformation { get; set; }

        [StringLength(100)]
        public string extWarranty { get; set; }

        [StringLength(100)]
        public string followUpNew { get; set; }

        [StringLength(100)]
        public string followUpNum { get; set; }

        [StringLength(100)]
        public string followUpOld { get; set; }

        [StringLength(100)]
        public string followUpRemarks { get; set; }

        [StringLength(100)]
        public string followUpResponse { get; set; }

        [StringLength(100)]
        public string followUpType { get; set; }

        [StringLength(100)]
        public string fuelType { get; set; }

        [Column(TypeName = "bit")]
        public bool isError { get; set; }

        [StringLength(100)]
        public string lastServiceDate { get; set; }

        [StringLength(100)]
        public string lastServiceType { get; set; }

        [StringLength(100)]
        public string lastfollowUpRemarks { get; set; }

        [StringLength(100)]
        public string mileage { get; set; }

        [StringLength(100)]
        public string mobile { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(100)]
        public string nextFollowupDate { get; set; }

        [StringLength(100)]
        public string pickupDrop { get; set; }

        [StringLength(100)]
        public string regNo { get; set; }

        public int rowNumber { get; set; }

        [StringLength(100)]
        public string saleDate { get; set; }

        [StringLength(100)]
        public string serviceType { get; set; }

        [StringLength(100)]
        public string telephone { get; set; }

        [StringLength(100)]
        public string deliveryDateStr { get; set; }

        [StringLength(100)]
        public string dueDateStr { get; set; }

        [StringLength(100)]
        public string followUpNewStr { get; set; }

        [StringLength(100)]
        public string followUpOldStr { get; set; }

        [StringLength(100)]
        public string lastServiceDateStr { get; set; }

        [StringLength(100)]
        public string mileageStr { get; set; }

        [StringLength(100)]
        public string saleDateStr { get; set; }

        [StringLength(100)]
        public string upload_id { get; set; }
    }
}
