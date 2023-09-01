using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("driverdeliverynotes")]
    public class DriversDeliveryNotes
    {
        [Key]
        public long Id { get; set; }
        
        public long? CustomerId { get; set; }

        public long? VehicleId { get; set; }

        public long? DriverSchedulerId { get; set; }

        public DateTime? GeneratedOn { get; set; }

        [Column(TypeName = "bit")]
        public bool IsCompleted { get; set; }

        [Column(TypeName = "bit")]
        public bool IsMap { get; set; }


    }
}