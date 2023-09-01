using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("fefirebasehistorydata")]
    public class fefirebasehistorydata
    {
        public long id { get; set; }

        public string actionDate { get; set; }

        public int ageOfVehicle { get; set; }

        public string appointmentType { get; set; }

        public int callTypePicId { get; set; }

        public string chassisNo { get; set; }

        public string comments { get; set; }

        public string creName { get; set; }

        public string currentAddress { get; set; }

        public long? custId { get; set; }

        public string customerName { get; set; }

        public string customerPhone { get; set; }

        public int daysBetweenVisit { get; set; }

        public string dealerCode { get; set; }

        public string discountValue { get; set; }

        public string engineNo { get; set; }

        public string insuranceCompany { get; set; }

        public string lastDisposition { get; set; }

        public string lastPolicyDate { get; set; }

        public string lastPremium { get; set; }

        public string makeCallFrom { get; set; }

        public string model { get; set; }

        public string nextRenewalDate { get; set; }

        public string paymentReference { get; set; }

        public string paymentType { get; set; }

        public string pincode { get; set; }

        public string policyNo { get; set; }

        public string renewalMode { get; set; }

        public string renewalType { get; set; }

        public int ringingTime { get; set; }

        public string saleDate { get; set; }

        public long appointmentbookedId { get; set; }

        public long insassign_id { get; set; }

        public string aptScheduledDate { get; set; }

        public string appointmentTime { get; set; }
        public long disposition { get; set; }

        public long? userId { get; set; }

        public string vehicleRegNo { get; set; }

        public long? vehicleId { get; set; }

        //Cols Not in- Used for Pushing data ******  -From-> Appointment
        public string vehicalRegNo { get; set; }

        public string customerAddress { get; set; }

        public string interactionDate { get; set; }

        public string lastPolicyNo { get; set; }

        public string lastIDV { get; set; }

        public long premiumWithValue { get; set; }

        public string coupon { get; set; }

        public string lastCoverage { get; set; }

        public string premium { get; set; }

        public string uploadedDate { get; set; }

        public string status { get; set; }

        public string documentUploaded { get; set; }
        
        //new columns for report
        public string kmTravelled { get; set; }

        public string startLocation { get; set; }

        public string stopLocation { get; set; }

        public string reason { get; set; }

        public string crePhoneNumber { get; set; }

        public string firebasekey { get; set; }

        public long? inspolicydrop_id { get; set; }

        public DateTime? updateddatetime { get; set; }

        [NotMapped]
        public string Disposition { get; set; }

        [NotMapped]
        public string InteractionId { get; set; }

        [NotMapped]
        public string Details { get; set; }

        [NotMapped]
        public string AgentName { get; set; }
    }
}