using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class serviceReminderViewModel
    {
        public List<campaign> campaignList { get; set; }

    }
    public class serviceFilter
    {
        public string city { get; set; }
        public string workshopLoc { get; set; }
        public string CRES { get; set; }
        public string campaign { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public DateTime? fromSLDate { get; set; }
        public DateTime? toSLDate { get; set; }
        public string serviceType { get; set; }
        public string serviceBookedType { get; set; }
        public bool isFiltered { get; set; }
        public int getDataFor { get; set; }
        public string lastDipo { get; set; }
        public string droppedCount { get; set; }
        public string typeOfPickup { get; set; }
        //by nisarga
        public string bookingstatus { get; set; }
        public string reasons { get; set; }
        // public string instatus { get; set; }

    }

    public class insuranceFilter
    {
        public string campaign { get; set; }
        public DateTime? expiryfromDate { get; set; }
        public DateTime? callfromDate { get; set; }
        public DateTime? expirytoDate { get; set; }
        public DateTime? calltoDate { get; set; }
        public string flag { get; set; }
        public string model { get; set; }
        public string CRES { get; set; }
        public string appointmentType { get; set; }
        public DateTime? appointmentFromDate { get; set; }
        public DateTime? appointmentToDate { get; set; }
        public string reasons { get; set; }
        public string attempts { get; set; }
        public bool isFiltered { get; set; }
        public int getDataFor { get; set; }
        public string LocationId { get; set; }
        public string renewalTypelist { get; set; }




    }


    public class CallLogAjaxLoadMR
    {

        public string userName { get; set; }
        public string customer_name { get; set; }
        public string vehicle_RegNo { get; set; }
        public string veh_model { get; set; }
        public string Loyalty { get; set; }
        public string duedate { get; set; }
        public string due_type { get; set; }
        public string forecast_type { get; set; }
        public string last_psfstatus { get; set; }
        public string DND_status { get; set; }
        public string campaignName { get; set; }
        public string complaints_count { get; set; }
        public string vehical_Id { get; set; }
        //public string  vehicle_Id{get; set;}
        public string customer_id { get; set; }
        public string assigned_intercation_id { get; set; }

        public string CREName { get; set; }
        public string Mobile_number { get; set; }
        public string model { get; set; }
        public string RONumber { get; set; }
        private DateTime ROdate { get; set; }
        private DateTime BillDate { get; set; }
        public string vehicle_id { get; set; }
        public string Category { get; set; }

        public string creName { get; set; }
        public string campaign { get; set; }
        public string customerName { get; set; }
        public string phone { get; set; }
        public string regNo { get; set; }
        public string chassisNo { get; set; }
        public string policyDueMonth { get; set; }
        private DateTime policyDueDate { get; set; }
        public string nextRenewalType { get; set; }
        public string lastInsuranceCompany { get; set; }
        private long insuranceassigned_id { get; set; }
        private long vehicle_vehicle_id { get; set; }
        public string Flag { get; set; }
        public string brand { get; set; }
        public string last_serviceDate { get; set; }
    }
    public class otherFilter
    {

        public string city { get; set; }
        public string workshopLoc { get; set; }
        public string CRES { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

        public bool isFiltered { get; set; }
        public int getDataFor { get; set; }
    }
    public class CallLogDispositionLoadMR
    {
        public string Loyalty { get; set; }
        public string due_type { get; set; }

        public string isCallinitaited { get; set; }
        public string category { get; set; }
        public string forecast_type { get; set; }
        public string last_psfstatus { get; set; }
        public string complaints_count { get; set; }

        //public string creName { get; set; }
        public long droppedCount { get; set; }
        public string campaignName { get; set; }
        public DateTime? call_date { get; set; }
        public string customerName { get; set; }
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
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public string DND_status { get; set; }

        public string scheduled_date { get; set; }
        public string scheduled_time { get; set; }
        public string duedate { get; set; }
        public string userName { get; set; }
        public string mediaFileLob { get; set; }
        public string Media_file { get; set; }
        public string dialedNoIs { get; set; }
        public DateTime? psfAppointmentDate { get; set; }
        public string psfAppointmentTime { get; set; }
        public string vehical_Id { get; set; }
        public string CREName { get; set; }
        public string callinteraction_id { get; set; }
        public string customer_name { get; set; }
        public string Mobile_number { get; set; }
        public string RONumber { get; set; }
        public DateTime? ROdate { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? Servey_date { get; set; }
        public string model { get; set; }
        public string chassisNo { get; set; }
        public string flag { get; set; }
        public long no_of_attempts { get; set; }
        public string brand { get; set; }
        public string fse { get; set; }
        public string reasons { get; set; }
        public string appointmentType { get; set; }
        public string insuranceCo { get; set; }

        public DateTime? saledate { get; set; }
        public DateTime? nextServicedate { get; set; }
        public string nextServiceType { get; set; }
        public string forecastLogic { get; set; }
        public string NoShowPeriod { get; set; }
        public string locationName { get; set; }
        public string lostStatus { get; set; }
        public DateTime? lastServiceDate { get; set; }
        public string lastServiceType { get; set; }
        public string location_id { get; set; }
        public string uploaddate { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        //  public string chassisnumber { get; set; }
        public string vehicleRegNum { get; set; }
        public DateTime? nextServiceDateTenure { get; set; }
        public string averageRunning { get; set; }
        public DateTime? nextServiceDateMilage { get; set; }
        public DateTime? nextServiceDate { get; set; }
        public DateTime? lastVisitDate { get; set; }
        public string lastVisitType { get; set; }
        public string milege { get; set; }
        public string visitSuccessRate { get; set; }
        public string serviceNoShowPeriod { get; set; }
        public string workshopName { get; set; }
        public string EmailID { get; set; }
        public string serviceType { get; set; }
        public string lastMileage { get; set; }
        public string lastServiceMileage { get; set; }
        public string averageMileage { get; set; }
        public DateTime? maxPSDate { get; set; }

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
        public string dnd { get; set; }
        public string duetype { get; set; }
        public string last_serviceDate { get; set; }

        //10-03-2020
        public string chassisnumber { get; set; }
    }


    public class CallLogDispositionLoadReminderMR
    {

        //Bucket1
        public string customer_name { get; set; }
        public string vehicle_RegNo { get; set; }
        public string veh_model { get; set; }
        public string category { get; set; }
        public string Loyalty { get; set; }
        public string duedate { get; set; }
        public string due_type { get; set; }
        public string forecast_type { get; set; }
        public string last_psfstatus { get; set; }
        public string DND_status { get; set; }
        public string campaignName { get; set; }
        public string complaints_count { get; set; }
        public string customer_id { get; set; }
        public string vehical_Id { get; set; }
        public string wyzUser_id { get; set; }
        public string assigned_intercation_id { get; set; }
        public string username { get; set; }
        //public string lastServiceDate { get; set; }

        //Bucket 2//
        public string CREName { get; set; }

        public string scheduled_date { get; set; }
        public string scheduled_time { get; set; }

        public string workshop { get; set; }

        public DateTime? lastServiceDate { get; set; }



        public string isCallinitaited { get; set; }


        //bucket 78


        //Insurance reminder
        public string lastInsuranceCompany { get; set; }
        public string insuranceCompany { get; set; }
        public string nextRenewalType { get; set; }

        public DateTime? policyDueDate { get; set; }
        public string regNo { get; set; }
        public string phone { get; set; }
        public string campaign { get; set; }
        public string vehicleRegNo { get; set; }
        public DateTime? policyExpiryDate { get; set; }
        public string appointmentFromTime { get; set; }
        public string appointment_status { get; set; }
        public string insuranceAgent { get; set; }
        public string fieldwalkingloc { get; set; }


        public string calldate { get; set; }
        public string chassiNo { get; set; }
        public string serviceBookedType { get; set; }
        public string serviceadvisor { get; set; }

        public string typeOfPickup { get; set; }
        public string booking_status { get; set; }
        public string attempts { get; set; }

        public long droppedCount { get; set; }
        public DateTime? call_date { get; set; }
        public string customerName { get; set; }
        public string mobileNumber { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string appointmentTime { get; set; }
        public string renewalType { get; set; }
        public string appointmentstatus { get; set; }
        public string insurance_agent { get; set; }
        public string Last_disposition { get; set; }
        public string reason { get; set; }
        public string followUpDate { get; set; }
        public string followUpTime { get; set; }
        public string vehicle_id { get; set; }

        public string mediaFileLob { get; set; }
        public string Media_file { get; set; }
        public string dialedNoIs { get; set; }
        public DateTime? psfAppointmentDate { get; set; }
        public string psfAppointmentTime { get; set; }
        public string callinteraction_id { get; set; }
        public string Mobile_number { get; set; }
        public string RONumber { get; set; }
        public DateTime? ROdate { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? Servey_date { get; set; }
        public string model { get; set; }
        public string chassisNo { get; set; }
        public string flag { get; set; }
        public long no_of_attempts { get; set; }
        public string brand { get; set; }
        public string fse { get; set; }
        public string reasons { get; set; }
        public string appointmentType { get; set; }
        public string insuranceCo { get; set; }

        public DateTime? saledate { get; set; }
        public DateTime? nextServicedate { get; set; }
        public string nextServiceType { get; set; }
        public string forecastLogic { get; set; }
        public string NoShowPeriod { get; set; }
        public string locationName { get; set; }
        public string lostStatus { get; set; }
        public string lastServiceType { get; set; }
        public string location_id { get; set; }
        public string uploaddate { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        //  public string chassisnumber { get; set; }
        public string vehicleRegNum { get; set; }
        public DateTime? nextServiceDateTenure { get; set; }
        public string averageRunning { get; set; }
        public DateTime? nextServiceDateMilage { get; set; }
        public DateTime? nextServiceDate { get; set; }
        public DateTime? lastVisitDate { get; set; }
        public string lastVisitType { get; set; }
        public string milege { get; set; }
        public string visitSuccessRate { get; set; }
        public string serviceNoShowPeriod { get; set; }
        public string workshopName { get; set; }
        public string EmailID { get; set; }
        public string serviceType { get; set; }
        public string lastMileage { get; set; }
        public string lastServiceMileage { get; set; }
        public string averageMileage { get; set; }
        public DateTime? maxPSDate { get; set; }

        //10-03-2020
        public string chassisnumber { get; set; }


        //other reminder

        public string dailedNoIs { get; set; }
        public string vehLoc { get; set; }
    }

    public class psfFilter
    {
        public string campaign { get; set; }
        public string city { get; set; }
        public string workshopLoc { get; set; }
        public string CRES { get; set; }
        public DateTime? fromassigndate { get; set; }
        public DateTime? toassigndate { get; set; }
        public bool isFiltered { get; set; }
        public int getDataFor { get; set; }
        public int ServiceTypeName { get; set; }
        public int serviceBookedType { get; set; }
        public int lastDispo { get; set; }
        public int droppedCount { get; set; }

    }

    public class psfreminder
    {
        public string campaignName { get; set; }
        public string CREName { get; set; }
        public string customer_name { get; set; }
        public string Mobile_number { get; set; }
        public string vehicle_RegNo { get; set; }
        public string model { get; set; }
        public string RONumber { get; set; }
        public DateTime? ROdate { get; set; }
        public DateTime? BillDate { get; set; }
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public string category { get; set; }
        public string assigned_intercation_id { get; set; }

        //public string creName { get; set; }
        public string callinteraction_id { get; set; }
        public DateTime? followUpDate { get; set; }
        //public string followUpDate { get; set; }/*--*/
        public string followUpTime { get; set; }
        public DateTime? call_date { get; set; }
        public DateTime? Servey_date { get; set; }
        public string psfAppointmentDate { get; set; }
        //public DateTime? psfAppointmentDate { get; set; } /*--*/
        public string psfAppointmentTime { get; set; }

        public string Last_disposition { get; set; }

    }

    //Manoj added 08-08-2020
    public class psfCompMgrCallLog
    {
        public long DissatStatus_id { get; set; }
        public string campaignName { get; set; }
        public string customer_name { get; set; }
        public string Mobile_number { get; set; }
        public string chassisno { get; set; }
        public string vehicle_RegNo { get; set; }
        public string workshop { get; set; }
        public string RONumber { get; set; }
        public DateTime? issuedate { get; set; }
        public int ageing { get; set; }
        public DateTime? psffollowupdate { get; set; }
        public string psffollowuptime { get; set; }
        public long calldispositiondata_id { get; set; }
        public DateTime? aptdate { get; set; }
        public string apttime { get; set; }
        public string reworkMode { get; set; }
        public long attempts { get; set; }
        public DateTime? BillDate { get; set; }
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public long complaint_creid { get; set; }
        public long reworkStatus_id { get; set; }
        public long callinteraction_id { get; set; }
        public string psfassignedInteraction_id { get; set; }
        public string aptstatus { get; set; }
        public string Last_disposition { get; set; }

    }

    public class psfCompMgrFilter
    {
        public int getDataFor { get; set; }
        public bool isFiltered { get; set; }
        public string compMgrWyzuserid { get; set; }
        public string workshopid { get; set; }
        public int aging { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public int attempts { get; set; }
        public string reworkmode { get; set; }
    }
}