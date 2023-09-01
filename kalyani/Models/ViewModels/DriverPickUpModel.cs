using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class DriverPickUpModel
    {
        [StringLength(255)]
        public string BookingType { get; set; }
        [StringLength(255)]
        public string driverName { get; set; }
        [StringLength(255)]
        public string ScheduledBy { get; set; }
        [StringLength(255)]
        public string Role { get; set; }
        [StringLength(255)]
        public string BookingDate { get; set; }
        [StringLength(255)]
        public string BookingTime { get; set; }
        [StringLength(255)]
        public string Workshopname { get; set; }
        [StringLength(255)]
        public string PickUpAddress { get; set; }
        [StringLength(255)]
        public string DropAddress { get; set; }
        [StringLength(255)]
        public string customerName { get; set; }
        [StringLength(255)]
        public string ChassisNo { get; set; }
        [StringLength(255)]
        public string Chassis { get; set; }
        [StringLength(255)]
        public string Engineno { get; set; }
        [StringLength(255)]
        public string VehicleRegNo { get; set; }
        [StringLength(255)]
        public string Model { get; set; }
        [StringLength(255)]

        public string Color { get; set; }
        [StringLength(255)]
        public string Attemptstatus { get; set; }
        [StringLength(255)]
        public string Dispostion { get; set; }
        [StringLength(255)]
        public string PickUp_dropComplete { get; set; }
        [StringLength(255)]
        public string Inventories { get; set; }
        [StringLength(255)]

        public string Invoice_Amt { get; set; }
        [StringLength(255)]
        public string PickupDateTime { get; set; }
        [StringLength(255)]
        public string Workshopdeliverytime { get; set; }
        [StringLength(255)]
        public string DropDateTime { get; set; }
        [StringLength(255)]
        public string startlocation { get; set; }
        [StringLength(255)]

        public string stoplocation { get; set; }
        [StringLength(255)]
        public string kmtravelled { get; set; }
        [StringLength(255)]
        public string PaymentCollected { get; set; }
        [StringLength(255)]
        public string PaymentReason { get; set; }
        [StringLength(255)]
        public string PaymentMode { get; set; }
        [StringLength(255)]

        public string Amount { get; set; }
        [StringLength(255)]
        public string ReferenceNo { get; set; }
        [StringLength(255)]
        public string PaymentRemarks { get; set; }
        public string saledate { get; set; }
        [StringLength(255)]
        public string nextServicetype { get; set; }
        [StringLength(255)]
        public string phonenumber { get; set; }
        [StringLength(255)]
        public string wmphonenumber { get; set; }
        [StringLength(255)]
        public string BranchAddress { get; set; }

    }
}