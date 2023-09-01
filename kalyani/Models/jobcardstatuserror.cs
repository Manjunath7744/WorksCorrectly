namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobcardstatuserror")]
    public partial class jobcardstatuserror
    {
        public long id { get; set; }

        [StringLength(200)]
        public string address1 { get; set; }

        [StringLength(200)]
        public string address2 { get; set; }

        [StringLength(200)]
        public string address3 { get; set; }

        [StringLength(100)]
        public string billAmtStr { get; set; }

        [StringLength(100)]
        public string billDateStr { get; set; }

        [StringLength(100)]
        public string billNo { get; set; }

        [StringLength(100)]
        public string centre { get; set; }

        [StringLength(100)]
        public string chassis { get; set; }

        [StringLength(100)]
        public string circularNo { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(100)]
        public string color { get; set; }

        [StringLength(100)]
        public string custCat { get; set; }

        [StringLength(100)]
        public string custName { get; set; }

        [StringLength(100)]
        public string doaStr { get; set; }

        [StringLength(100)]
        public string dobStr { get; set; }

        [StringLength(100)]
        public string engineNo { get; set; }

        [StringLength(1073741823)]
        public string errorInformation { get; set; }

        [StringLength(100)]
        public string estLabAmtStr { get; set; }

        [StringLength(100)]
        public string estPartAmtStr { get; set; }

        [StringLength(100)]
        public string groupData { get; set; }

        [Column(TypeName = "bit")]
        public bool isError { get; set; }

        [StringLength(100)]
        public string jobCardDateStr { get; set; }

        [StringLength(100)]
        public string jobCardNo { get; set; }

        [StringLength(100)]
        public string labAmtStr { get; set; }

        [StringLength(100)]
        public string mi { get; set; }

        [StringLength(100)]
        public string mileageStr { get; set; }

        [StringLength(100)]
        public string mobile { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(100)]
        public string partsAmtStr { get; set; }

        [StringLength(100)]
        public string phone { get; set; }

        [StringLength(100)]
        public string pickupDateStr { get; set; }

        [StringLength(100)]
        public string pickupLoc { get; set; }

        [StringLength(100)]
        public string pickupRequired { get; set; }

        [StringLength(100)]
        public string pincodeStr { get; set; }

        [StringLength(100)]
        public string promiseDtStr { get; set; }

        [StringLength(100)]
        public string psfStatus { get; set; }

        [StringLength(100)]
        public string readyDateandTimeStr { get; set; }

        [StringLength(100)]
        public string regNo { get; set; }

        [StringLength(100)]
        public string repeatRevisit { get; set; }

        [StringLength(100)]
        public string revPromisedDtStr { get; set; }

        public int rowNumber { get; set; }

        [StringLength(100)]
        public string saleDateStr { get; set; }

        [StringLength(100)]
        public string serviceAdvisor { get; set; }

        [StringLength(100)]
        public string serviceType { get; set; }

        [StringLength(100)]
        public string slNo { get; set; }

        [StringLength(100)]
        public string status { get; set; }

        [StringLength(100)]
        public string technician { get; set; }

        [StringLength(100)]
        public string upload_id { get; set; }

        [StringLength(100)]
        public string variant { get; set; }

        [StringLength(100)]
        public string yearStr { get; set; }
    }
}
