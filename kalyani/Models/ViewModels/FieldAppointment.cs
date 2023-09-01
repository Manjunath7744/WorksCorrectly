using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class FieldAppointment
    {
        [StringLength(255)]
        public string SrNo { get; set; }
        [StringLength(255)]
        public string chassisNo { get; set; }
        [StringLength(255)]
        public string variant { get; set; }
        [StringLength(255)]
        public string saleDate { get; set; }
        [StringLength(255)]
        public string customerName { get; set; }
        [StringLength(255)]
        public string model { get; set; }
        [StringLength(255)]
        public string CustomerPincode { get; set; }
        [StringLength(255)]
        public string vehicleRegNo { get; set; }
        [StringLength(255)]
        public string CustomerAddress { get; set; }
        [StringLength(255)]
        public string CustomerPhone { get; set; }
        [StringLength(255)]
        public string AppointmentDetails { get; set; }
        [StringLength(255)]
        public string appointmentFromTime { get; set; }
        [StringLength(255)]
        public string creName { get; set; }
        [StringLength(255)]
        public string creRemarks { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        [StringLength(255)]
        public string LapsingDate { get; set; }
        [StringLength(255)]
        public string Company { get; set; }
        [StringLength(255)]
        public string IDVPreviousPloicy { get; set; }
        [StringLength(255)]
        public string NCBPreviousPloicy { get; set; }
        [StringLength(255)]
        public string PremiumwithNCB { get; set; }
        [StringLength(255)]
        public string DSE { get; set; }
        [StringLength(255)]
        public string Date { get; set; }
        [StringLength(255)]
        public string PremiumwithoutNCB { get; set; }
        [StringLength(255)]
        public string IDVCurrentPolicy { get; set; }
        [StringLength(255)]
        public string Discount { get; set; }
        [StringLength(255)]
        public string DSERemarks { get; set; }
        [StringLength(255)]
        public string policyNo { get; set; }
        [StringLength(255)]
        public string grossPremium { get; set; }
        [StringLength(255)]
        public string EngineNo	 { get; set; }
    [StringLength(255)]
    public string VisitType	 { get; set; }
        [StringLength(255)]
        public string fename { get; set; }
        [StringLength(255)]
        public string Premium { get; set; }


    }
}