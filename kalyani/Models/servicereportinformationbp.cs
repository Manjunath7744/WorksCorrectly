using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("servicereportinformationbp")]
    public class servicereportinformationbp
    {
        public long? id { get; set; }
        public string dlrcd { get; set; }
        public string brccd { get; set; }
        public string brcregion { get; set; }
        public string vinno { get; set; }
        public string regno { get; set; }
        public string series { get; set; }
        public string customerid { get; set; }
        public string customername { get; set; }
        public string jobtype { get; set; }
        public long? mileagein { get; set; }
        public long? mileageout { get; set; }
        public string jobcode { get; set; }
        public string jobdesc { get; set; }
        public string estno { get; set; }
        public string estamt { get; set; }
        public string finalinvamt { get; set; }
        public string joborderno { get; set; }
        public DateTime? jobopendate { get; set; }
        public DateTime? jobclosedate { get; set; }
        public DateTime? invoicedate { get; set; }
        public DateTime? promisedeliverydate { get; set; }
        public string promisedeliverytime { get; set; }
        public string brandtoyotagrey { get; set; }
        public string openserviceadvisorname { get; set; }
        public string editserviceadvisorname { get; set; }
        public string closeserviceadvisorname { get; set; }
        public string vehiclesolddealer { get; set; }
        public string jdpcustomer { get; set; }
        public string emflag { get; set; }
        public string bpreptype { get; set; }
        public string firstsmsflag { get; set; }
        public DateTime? firstsmsdttime { get; set; }
        public string secondfirstsmsflag { get; set; }
        public DateTime? secondfirstsmsdttime { get; set; }
        public string thirdsmsflag { get; set; }
        public DateTime? thirdsmsdttime { get; set; }
        public string fourthsmsflag { get; set; }
        public DateTime? fourthsmsdttime { get; set; }
        public string fifthsmsflag { get; set; }
        public DateTime? fifthsmssenddt { get; set; }
        public string sixsmsflag { get; set; }
        public DateTime? sixsmssenddt { get; set; }
        public string seventhsmsflag { get; set; }
        public DateTime? seventhsmssenddt { get; set; }
        public string eighthsmsflag { get; set; }
        public DateTime? eighthsmssenddt { get; set; }
        public string ninethsmsflag { get; set; }
        public DateTime? ninethsmssenddt { get; set; }
        public DateTime? estprintdttime { get; set; }
        public DateTime? roprintdttime { get; set; }
        public string appointmentwalkin { get; set; }
        public string paytype { get; set; }
        public string appointmentno { get; set; }
        public string couponcode { get; set; }
        public string taxinvoiceno { get; set; }
        public string waitflag { get; set; }
        public string cancelflag { get; set; }
        public string occupation { get; set; }
        public string suboccupation { get; set; }
        public string servicedat { get; set; }
        public string qcremarks { get; set; }
        public string mobilepayment { get; set; }
        public string nosmsdnd { get; set; }
        public string teleappregiser { get; set; }
        public DateTime ? carindate { get; set; }
        public string carintime { get; set; }
        public string insurancetype { get; set; }
        public string workshop_id { get; set; }
        public string location { get; set; }
        public string campaignFromDate { get; set; }
        public string campaignToDate { get; set; }
        public string campaignName { get; set; }
        public long? uploadid { get; set; }
        public string locType { get; set; }

    }
}