using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("uio_report")]

    public class uio_report
    {
        public long? id { get; set; }
        public string VINNumber { get; set; }
        public string ModelSeries { get; set; }
        public string ModelName { get; set; }
        public DateTime? SalesDate { get; set; }
        public string NDealerCode { get; set; }
        public long? NMileage { get; set; }
        public DateTime? NReportDate { get; set; }
        public string N1DealerCode { get; set; }
        public long? N1Mileage { get; set; }
        public DateTime? N1ReportDate { get; set; }
        public string N2DealerCode { get; set; }
        public long? N2Mileage { get; set; }
        public DateTime? N2ReportDate { get; set; }
        public string AllocatedBranch { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerDistrict { get; set; }
        public string CustomerIncome { get; set; }
        public string CustomerOccupation { get; set; }
        public string CustomerState { get; set; }
        public string Address { get; set; }
        public string Address3 { get; set; }
        public string Email { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string CustomerRoad { get; set; }
        public string IC { get; set; }
        public string workshop_id { get; set; }
        public string location { get; set; }
        public string campaignFromDate { get; set; }
        public string campaignToDate { get; set; }
        public string campaignName { get; set; }
        public long? uploadid { get; set; }
        public string locType { get; set; }

    }
}