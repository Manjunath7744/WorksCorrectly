namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("servicebooked")]
    public partial class servicebooked
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public servicebooked()
        {
            callinteractions = new HashSet<callinteraction>();
            serviceadvisorinteractions = new HashSet<serviceadvisorinteraction>();
        }

        public long serviceBookedId { get; set; }

        [Column(TypeName = "bit")]
        public bool isPickupRequired { get; set; }

        public DateTime? scheduledDateTime { get; set; }

        //[StringLength(255)]
        public string serviceBookedType { get; set; }

        //[StringLength(255)]
        public string statusOfSB { get; set; }

        //[StringLength(255)]
        public string typeOfPickup { get; set; }

        public long? callInteraction_id { get; set; }

        public long? customer_id { get; set; }

        public long? driver_id { get; set; }

        public long? pickupDrop_id { get; set; }

        public long? serviceAdvisor_advisorId { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? workshop_id { get; set; }

        public long? specialOfferMaster_id { get; set; }

        //[StringLength(200)]
        [AllowHtml]
        public string serviceBookingAddress { get; set; }

        public long? serviceBookType_id { get; set; }

        public long? serviceBookStatus_id { get; set; }

        public long? wyzUser_id { get; set; }

        public long? chaser_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updatedDate { get; set; }

        public long service_id { get; set; }

        //[StringLength(45)]
        public string pickUpAmount { get; set; }

       // [StringLength(50)]
        public string bookingsource { get; set; }
        public long? dropdriver_id { get; set; }

        //for driver scheduling and app
        public long? lastallocateddriver_id { get; set; }
        public long? lastallocateddropdriver_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? servicepickupdate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? servicedropdate { get; set; }
        [AllowHtml]
        public string serviceBookingDropAddress { get; set; }
        public long pickupdroptype { get; set; }

        public virtual calldispositiondata calldispositiondata { get; set; }

        public virtual calldispositiondata calldispositiondata1 { get; set; }

        public virtual callinteraction callinteraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        public virtual customer customer { get; set; }

        public virtual driver driver { get; set; }

        public virtual pickupdrop pickupdrop { get; set; }

        public virtual serviceadvisor serviceadvisor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisorinteraction> serviceadvisorinteractions { get; set; }

        public virtual specialoffermaster specialoffermaster { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual workshop workshop { get; set; }

        public virtual servicetype servicetype { get; set; }

        public virtual servicetype servicetype1 { get; set; }

        public virtual vehicle vehicle { get; set; }

        public virtual wyzuser wyzuser1 { get; set; }
    }
}
