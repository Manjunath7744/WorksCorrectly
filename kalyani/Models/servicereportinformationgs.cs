using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("servicereportinformationgs")]
    public class servicereportinformationgs
    {
        public long? id { get; set; }
        public string vinno { get; set; }
        public string regno { get; set; }
        public string series { get; set; }
        public string customerid { get; set; }
        public string customername { get; set; }
        public string jobtype { get; set; }
        public string mileagein { get; set; }
        public string jobcode { get; set; }
        public string jobdesc { get; set; }
        public string joborderno { get; set; }
        public DateTime ? jobopendate { get; set; }
        public string jobopentime { get; set; }
        public DateTime ? jobclosedate { get; set; }
        public string jobclosetime { get; set; }
        public long? mileageout { get; set; }
        public DateTime ? invoicedate { get; set; }
        public string invoicetime { get; set; }
        public DateTime ? promisedeliverydate { get; set; }
        public string promisedeliverytime { get; set; }
        public string brandtoyotagrey { get; set; }
        public string OpenServiceAdvisoName { get; set; }
        public string EditServiceAdvisorName { get; set; }
        public string CloseServiceAdvisorName { get; set; }
        public string VehicleSoldDealer { get; set; }
        public string JDPCustomer { get; set; }
        public string EMFlag { get; set; }
        public string BPRepType { get; set; }
        public string FIRSTSMSFLAG { get; set; }
        public DateTime ? FIRSTSMSDTTIME { get; set; }
        public string SECONDSMSFLAG { get; set; }
        public string THIRDSMSFLAG { get; set; }
        public string FOURTHSMSFLAG { get; set; }
        public DateTime ? ROESTPrintDTTIME { get; set; }
        public DateTime ? SECONDSMSDTTIME { get; set; }
        public DateTime ? THIRDSMSDTTIME { get; set; }
        public DateTime ? FOURTHSMSDTTIME { get; set; }
        public string APPOINTMENTWALKIN { get; set; }
        public string PayType { get; set; }
        public string AppointmentNo { get; set; }
        public string CouponCode { get; set; }
        public string TaxInvoiceNo { get; set; }
        public string WaitFlag { get; set; }
        public string CancelFlag { get; set; }
        public string Occupation { get; set; }
        public string SubOccupation { get; set; }
        public string ServicedAt { get; set; }
        public string QCRemarks { get; set; }
        public string MOBILEPAYMENT { get; set; }
        public string workshop_id { get; set; }
        public string location { get; set; }
        public string campaignFromDate { get; set; }
        public string campaignToDate { get; set; }
        public string campaignName { get; set; }
        public long? uploadid { get; set; }
        public string locType { get; set; }

    }
}