using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("salesreport")]
    public class salesreport
    {
        public long? id { get; set; }
        public string BranchName { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Tehsil { get; set; }
        public string District { get; set; }
        public string PinCode { get; set; }
        public string PhoneNo { get; set; }
        public string AltPhoneNoOne { get; set; }
        public string AltPhoneNoTwo { get; set; }
        public string EmailID { get; set; }
        public string RegistrationNo { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public string Color { get; set; }
        public DateTime? DateofBirth { get; set; }
        public DateTime? Anniversary { get; set; }
        public string Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string CustomerType { get; set; }
        public string CorporateName { get; set; }
        public DateTime? EWStartDate { get; set; }
        public DateTime? EWEndDate { get; set; }
        public string EWProductCode { get; set; }
        public string LoyaltyStartDate { get; set; }
        public string LoyaltyEndDate { get; set; }
        public string LoyaltyProductCode { get; set; }
        public string RSAStartSate { get; set; }
        public DateTime ? RSAEndDate { get; set; }
        public DateTime? InsuranceDate { get; set; }
        public string InsuranceCoverNote { get; set; }
        public string InsuranceCompany { get; set; }
        public double InsuranceAmount { get; set; }
        public string SalesConsultantName { get; set; }
        public string workshop_id { get; set; }
        public string location { get; set; }
        public string campaignFromDate { get; set; }
        public string campaignToDate { get; set; }
        public long? uploadid { get; set; }
        public string locType { get; set; }

    }
}