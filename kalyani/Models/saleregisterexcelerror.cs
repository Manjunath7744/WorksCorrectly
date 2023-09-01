namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("saleregisterexcelerror")]
    public partial class saleregisterexcelerror
    {
        public long id { get; set; }

        [StringLength(100)]
        public string additionalTaxStr { get; set; }

        [StringLength(100)]
        public string address1 { get; set; }

        [StringLength(100)]
        public string address2 { get; set; }

        [StringLength(100)]
        public string address3 { get; set; }

        [StringLength(100)]
        public string allotmentStr { get; set; }

        [StringLength(100)]
        public string area { get; set; }

        [StringLength(100)]
        public string areaType { get; set; }

        [StringLength(100)]
        public string basicPriceStr { get; set; }

        [StringLength(100)]
        public string buyerType { get; set; }

        [StringLength(100)]
        public string ceName { get; set; }

        [StringLength(100)]
        public string chassis { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(100)]
        public string colorDesc { get; set; }

        [StringLength(100)]
        public string colorcode { get; set; }

        [StringLength(100)]
        public string corporate { get; set; }

        [StringLength(100)]
        public string csdExShowRoomStr { get; set; }

        [StringLength(100)]
        public string customerId { get; set; }

        [StringLength(100)]
        public string customerName { get; set; }

        [StringLength(100)]
        public string delDateStr { get; set; }

        [StringLength(100)]
        public string discountStr { get; set; }

        [StringLength(100)]
        public string district { get; set; }

        [StringLength(100)]
        public string dsa { get; set; }

        [StringLength(100)]
        public string dse { get; set; }

        [StringLength(100)]
        public string emailId { get; set; }

        [StringLength(100)]
        public string engine { get; set; }

        [StringLength(100)]
        public string enquirSubSource { get; set; }

        [StringLength(100)]
        public string enquiryNo { get; set; }

        [StringLength(100)]
        public string enquirySources { get; set; }

        [StringLength(1073741823)]
        public string errorInformation { get; set; }

        [StringLength(100)]
        public string evaluatorMSPIN { get; set; }

        [StringLength(100)]
        public string exWarrantyType { get; set; }

        [StringLength(100)]
        public string exchCancReason { get; set; }

        [StringLength(100)]
        public string exchangeCancDateStr { get; set; }

        [StringLength(100)]
        public string extWarrantyStr { get; set; }

        [StringLength(100)]
        public string fuelType { get; set; }

        [StringLength(100)]
        public string hypAmtStr { get; set; }

        [StringLength(100)]
        public string hyp_address { get; set; }

        [StringLength(100)]
        public string hypthecation { get; set; }

        [StringLength(100)]
        public string indivScheme { get; set; }

        [StringLength(100)]
        public string instiCustStr { get; set; }

        [StringLength(100)]
        public string insuranceStr { get; set; }

        [StringLength(100)]
        public string invCancelDateStr { get; set; }

        [StringLength(100)]
        public string invCancelNo { get; set; }

        [StringLength(100)]
        public string invDateStr { get; set; }

        [StringLength(100)]
        public string invNo { get; set; }

        [Column(TypeName = "bit")]
        public bool isError { get; set; }

        [StringLength(100)]
        public string ivnAmtStr { get; set; }

        [StringLength(100)]
        public string locCode { get; set; }

        [StringLength(100)]
        public string loyaltyBonusDiscStr { get; set; }

        [StringLength(100)]
        public string mgaSoldAmtStr { get; set; }

        [StringLength(100)]
        public string miDateStr { get; set; }

        [StringLength(100)]
        public string miFlag { get; set; }

        [StringLength(100)]
        public string miname { get; set; }

        [StringLength(100)]
        public string mobile1 { get; set; }

        [StringLength(100)]
        public string mobile2 { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(100)]
        public string mulInvDateStr { get; set; }

        [StringLength(100)]
        public string mulInvNo { get; set; }

        [StringLength(100)]
        public string offPhone { get; set; }

        [StringLength(100)]
        public string oldCarMfg { get; set; }

        [StringLength(100)]
        public string oldCarModelCode { get; set; }

        [StringLength(100)]
        public string oldCarOwner { get; set; }

        [StringLength(100)]
        public string oldCarRegNo { get; set; }

        [StringLength(100)]
        public string oldCarRelation { get; set; }

        [StringLength(100)]
        public string oldCarStatus { get; set; }

        [StringLength(100)]
        public string orderDateStr { get; set; }

        [StringLength(100)]
        public string orderNum { get; set; }

        [StringLength(100)]
        public string outlet { get; set; }

        [StringLength(100)]
        public string panNoStr { get; set; }

        [StringLength(100)]
        public string picDesc { get; set; }

        [StringLength(100)]
        public string pincodeStr { get; set; }

        [StringLength(100)]
        public string promisedDelvDateStr { get; set; }

        [StringLength(100)]
        public string purchasePriceStr { get; set; }

        [StringLength(100)]
        public string refMobileNo { get; set; }

        [StringLength(100)]
        public string refNo { get; set; }

        [StringLength(100)]
        public string refRegNo { get; set; }

        [StringLength(100)]
        public string refType { get; set; }

        [StringLength(100)]
        public string refby { get; set; }

        [StringLength(100)]
        public string regNo { get; set; }

        [StringLength(100)]
        public string resCode { get; set; }

        [StringLength(100)]
        public string roadTaxStr { get; set; }

        [StringLength(100)]
        public string roundOffStr { get; set; }

        public int rowNumber { get; set; }

        [StringLength(100)]
        public string saleType { get; set; }

        [StringLength(100)]
        public string salutation { get; set; }

        [StringLength(100)]
        public string schemeOfGoiStr { get; set; }

        [StringLength(100)]
        public string stateDesc { get; set; }

        [StringLength(100)]
        public string status { get; set; }

        [StringLength(100)]
        public string stdCode { get; set; }

        [StringLength(100)]
        public string stdCodeComp { get; set; }

        [StringLength(100)]
        public string sumStr { get; set; }

        [StringLength(100)]
        public string teamLead { get; set; }

        [StringLength(100)]
        public string tehsilDesc { get; set; }

        [StringLength(100)]
        public string tradeIn { get; set; }

        [StringLength(100)]
        public string upload_id { get; set; }

        [StringLength(100)]
        public string varaintDesc { get; set; }

        [StringLength(100)]
        public string variantCode { get; set; }

        [StringLength(100)]
        public string vatStr { get; set; }

        [StringLength(100)]
        public string villageDesc { get; set; }

        [StringLength(100)]
        public string year { get; set; }
    }
}
