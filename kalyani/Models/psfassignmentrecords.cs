using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("psfassignmentrecords")]
    public class psfassignmentrecords
    {
        public int id { get; set; }
        public string callstatus { get; set; }
        public string lastDisposition { get; set; }
        public DateTime? assigndate { get; set; }
        public int campaign_id { get; set; }
        public long customer_id { get; set; }
        public long calllDisposition_id { get; set; }
        public long service_id { get; set; }
        public long vehicle_id { get; set; }
        public long wyzUser_id { get; set; }
        public int upload_id { get; set; }
        public long workshop_id { get; set; }
        public bool iscei { get; set; }
        public bool isbodyshop { get; set; }
        public DateTime? updatedOnDate { get; set; }
        public DateTime? psf1 { get; set; }
        public DateTime? psf2 { get; set; }
        public bool isResolved { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string vehicleRegNo { get; set; }
        public string Model { get; set; }
        public DateTime? SaleDate { get; set; }
        public string Customername { get; set; }
        public string PhoneNumber { get; set; }
        public string jobCardNumber { get; set; }
        public DateTime? BillDate { get; set; }
        public string ServiceType { get; set; }
        public string SAName { get; set; }
        public string Technician { get; set; }
        public DateTime? CallDate { get; set; }
        public long psfassignedInteraction_id { get; set; }
        public string psfstatus { get; set; }
        public string workshopname { get; set; }
    }
}