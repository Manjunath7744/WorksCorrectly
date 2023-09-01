using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("masterservicedetails")]
    public class masterservicedetails
    {
        [Key]
        public int id { get; set; }
        public long CustomerID { get; set; }
        public long VehicleID { get; set; }
        public string ChassisNo { get; set; }
        public string vehicleRegNo { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string phoneNumber3 { get; set; }
        public string NextServiceDate { get; set; }
        public string NextServiceType { get; set; }

    }
}