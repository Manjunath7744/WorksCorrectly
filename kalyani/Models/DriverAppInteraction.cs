using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("driverappinteractions")]
    public class DriverAppInteraction
    {
        [Key]
        public long Id { get; set; }
        public string DemandedRepair { get; set; }
        public string Mileage { get; set; }
        public bool IsSameDayDelivery { get; set; }
        public string PaymentCollected { get; set; }
        public string WorkshopGateIntime { get; set; }
        public string MileageAfterGateIn { get; set; }
        public string PaymentReason { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string ReferenceNo { get; set; }
        public string PaymentRemarks { get; set; }
        public long DriverScheduler_Id { get; set; }
        public long Vehicle_Id { get; set; }
        public long Customer_Id { get; set; }
        public string UniqueKey { get; set; }
        public bool IsPickUp { get; set; }
        public bool IsDrop { get; set; }
        public string Inventories { get; set; }
        public string Disposition { get; set; }
        public string Reasons { get; set; }
        public DateTime? RescheduledDateTime { get; set; }
        public string InteractionType { get; set; }
        public string DriverAppFiles_Ids { get; set; }
        public string Invoice_Amt { get; set; }//delivercheckdrop
        public string PickupDateTime { get; set; }//pickupsstraTEDSAVEREACHEDPICKUP
        public string DeliveryDateTime { get; set; }//SAVEREACHEDLOCATION
        public string kmtravelled { get; set; }
        public string startlocation { get; set; }
        public string stoplocation { get; set; }

        [NotMapped]
        public int PickUpDropType { get; set; }

        [NotMapped]
        public string VehicleId { get; set; }

       // [NotMapped]
        public string DriverName { get; set; }

       // [NotMapped]
        public long? DriverId { get; set; }

        public DateTime LastUpdatedOn { get; set; }
        
        [NotMapped]
        public List<Dictionary<string,string>> DictFilesList { get; set; }
        
        [NotMapped]
        public string CREName { get; set; }

        [NotMapped]
        public string Details { get; set; }

    }
}