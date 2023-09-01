using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class CallLogDispositionLoadMRForcast
    {
        public int CallLogDispositionLoadMRForcastID { get; set; }
        public string isCallinitaited { get; set; }
        public string creName { get; set; }//
        public long droppedCount { get; set; }
        public string campaignName { get; set; }
        public DateTime? call_date { get; set; }
        public string customerName { get; set; }//
        public string mobileNumber { get; set; }
        public string vehicle_RegNo { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string appointmentTime { get; set; }
        public string renewalType { get; set; }
        public string appointmentstatus { get; set; }
        public string insurance_agent { get; set; }
        public string Last_disposition { get; set; }
        public string reason { get; set; }
        public string followUpDate { get; set; }
        public string followUpTime { get; set; }
        public DateTime? policyDueDate { get; set; }
        public string vehicle_id { get; set; }  // 
        public string customer_id { get; set; }//

        public string scheduled_date { get; set; }
        public string scheduled_time { get; set; }
        public string duedate { get; set; }
        public string userName { get; set; }
        public string mediaFileLob { get; set; }
        public string Media_file { get; set; }
        public string dialedNoIs { get; set; }
        public DateTime? psfAppointmentDate { get; set; }
        public string psfAppointmentTime { get; set; }
        public string callinteraction_id { get; set; }
        public string customer_name { get; set; }
        public string Mobile_number { get; set; }
        public string RONumber { get; set; }
        public DateTime? ROdate { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? Servey_date { get; set; }
        public string model { get; set; }//
        public string chassisNo { get; set; }
        public string flag { get; set; }
        public long no_of_attempts { get; set; }
        public string brand { get; set; }
        public string fse { get; set; }
        public string reasons { get; set; }
        public string appointmentType { get; set; }
        public string insuranceCo { get; set; }

        public DateTime? saledate { get; set; }//
        public DateTime? nextServicedate { get; set; }//
        public string nextServiceType { get; set; }//
        public string forecastLogic { get; set; }//
        public string NoShowPeriod { get; set; }//
        public string locationName { get; set; }//
        public string lostStatus { get; set; }//
        public DateTime? lastServiceDate { get; set; }//
        public string lastServiceType { get; set; }//
        public string location_id { get; set; }//
        public string uploaddate { get; set; }//
        public string phoneNumber { get; set; }//
        public string address { get; set; }
        public string chassisnumber { get; set; }//
        public string vehicleRegNum { get; set; }//
        public DateTime? nextServiceDateTenure { get; set; }//
        public string averageRunning { get; set; }//
        public DateTime? nextServiceDateMilage { get; set; }//
        public DateTime? lastVisitDate { get; set; }
        public string lastVisitType { get; set; }
        public string milege { get; set; }
        public string visitSuccessRate { get; set; }
        public string serviceNoShowPeriod { get; set; }
        public string workshopName { get; set; }
        public string EmailID { get; set; }
        public string serviceType { get; set; }
        public string lastMileage { get; set; }//
        public string lastServiceMileage { get; set; }
        public string averageMileage { get; set; }
        public DateTime? maxPSDate { get; set; }//

        public string maxpsyear { get; set; }
        public long JAN { get; set; }
        public long FEB { get; set; }
        public long MAR { get; set; }
        public long APR { get; set; }
        public long MAY { get; set; }
        public long JUN { get; set; }
        public long JUL { get; set; }
        public long AUG { get; set; }
        public long SEP { get; set; }
        public long OCT { get; set; }
        public long NOV { get; set; }
        public long DEC { get; set; }
        public long Other { get; set; }
        public string dnd { get; set; }//
        public string duetype { get; set; }
        public string last_serviceDate { get; set; }
        public string saledealercode { get; set; }
        public string ServiceTypeId { get; set; }
        public string CategoryId { get; set; }
        public string RoId { get; set; }
    }
    public class smrforecastDownloads
    {
        public DateTime? nextServiceDateTenure { get; set; }
        public string averageRunning { get; set; }
        public string lastMileage { get; set; }
        public string nextServiceDateMilage { get; set; }
        public DateTime? maxPSDate { get; set; }
        public string creName { get; set; }
        public string uploaddate { get; set; }
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public string location_id { get; set; }
        public string chassisnumber { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public string dnd { get; set; }
        public string vehicleRegNum { get; set; }
        public string model { get; set; }
        public DateTime? saledate { get; set; }
        public DateTime? nextServicedate { get; set; }
        public string nextServiceType { get; set; }
        public string forecastLogic { get; set; }
        public string NoShowPeriod { get; set; }
        public string locationName { get; set; }
        public string lostStatus { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public string lastServiceType { get; set; }
    }

    public class DataWorkshopWiseCounts
    {
     
        public string workshopname { get; set; }
        public string FR1 { get; set; }
        public string FR2 { get; set; }
        public string FR3 { get; set; }
        public string FR4 { get; set; }
        public string PMS { get; set; }
        public string PMS20 { get; set; }
        public string PMS30 { get; set; }
        public string PMS40 { get; set; }
        public string PMS50 { get; set; }
        public string PMS60 { get; set; }
        public string PMS70 { get; set; }
        public string PMS80_and_above { get; set; }
        public string Total { get; set; }
     
    }
}