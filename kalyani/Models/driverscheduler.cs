using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("driverscheduler")]
    public class driverscheduler
    {
        [Key]
        public int id { get; set; }

        [StringLength(45)]
        public string uniquekey { get; set; }

        public long? vehicle_id { get; set; }

        public long? customer_id { get; set; }

        public long? driver_id { get; set; }
        
        public long? scheduledBy { get; set; }

        public long? vehiclecount { get; set; }
        
        [StringLength(50)]
        public string firebasekey { get; set; }

        public long? lastdriver_id { get; set; }

        public bool IsCancelled { get; set; }

        public long driverBookingdetails_id { get; set; }

        public DateTime updatedOn { get; set; }

        public int PickUpDrop { get; set; } // only 1 and 2(1->PickUp and 2->Drop)

        public bool ispushed { get; set; }
        public bool iscanceldeleted { get; set; }
    }
}