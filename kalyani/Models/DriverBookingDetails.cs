using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("driverbookingdetails")]
    public class DriverBookingDetails
    {
        [Key]
        public long id { get; set; }
        
        [Column(TypeName ="date")]
        public DateTime? BookingDate { get; set; }

        [StringLength(25)]
        public string BookingTime { get; set; }

        public long PickUpDrop { get; set; }

        [StringLength(75)]
        public string BookingType { get; set; }

        [StringLength(450)]
        public string PickUpAddress { get; set; }

        [StringLength(450)]
        public string DropAddress { get; set; }

        public long? ScheduledBy { get; set; }

        public long? Vehicle_Id { get; set; }

        public long? Customer_Id { get; set; }

        public long? Driver_Id { get; set; }

        public long? LastAllocatedDriver { get; set; }

        public string UniqueKey { get; set; }

        public string TimeRage { get; set; }
        public long? workshop_id { get; set; }
        public long serviceBookedId { get; set; }
    }
}