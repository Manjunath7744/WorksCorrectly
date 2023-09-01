using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class DriverFirebaseData
    {
        public string CustomerName { get; set; }//--
        public string CustomerPhoneNumber { get; set; }//--
        public string RegistrationNumber { get; set; }//--
        public string ChassisNumber { get; set; }//--
        public string Model { get; set; }//--
        public string SaleDate { get; set; }//--
        public string SaleLocation { get; set; }//--
        public string Mileage { get; set; }//--
        public string AptDateTime { get; set; }//--
        public string CRETeleCaller { get; set; }//--
        public string CREPhoneNumber { get; set; }//--
        public string ServiceBookedType { get; set; }//--
        public string WorkshopName { get; set; }//--
        public string ServiceAdvisor { get; set; }//--
        public string AptMode { get; set; }//--
        public string CRERemarks { get; set; }//--
        public long PickUpDropType { get; set; }//--

        public string Remarks { get; set; }
        public string PickUpAddress { get; set; }//--
        public string InteractionDate { get; set; }//--
        public string DriverName { get; set; }//--
        public string LastDisposition { get; set; }//--
        public long DriverId { get; set; }//--
        public long DriverScheduler_Id { get; set; }//--

        public string VehicleId { get; set; }//---
        public long Customer_Id { get; set; }//--
        public string UniqueKey { get; set; }

        public string DriverPhoneNumber { get; set; }//--

        public string Disposition { get; set; }//--
        public string Reasons { get; set; }//--
        public DateTime? RescheduledDateTime { get; set; }

        public string CREName { get; set; }//-

       // public int CurrentCount { get; set; }//--

      //  public int PendingCount { get; set; }//--

        //public int CompletedCount { get; set; }//--

        public string DeliveryNoteStatus { get; set; }

        public string Status { get; set; }
        public string DemandedRepair { get; set; }
        //Pickup Completed in 'PickUpDropRequired'
        public int PickUpCount { get; set; }
        public string PickupDispositon { get; set; }
        public string DropDisposition { get; set; }

    }
}