using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace AutoSherpa_project.Models.ViewModels
{
    public class ServiceLogViewModel
    {
        public List<CallLogAjaxLoad> callLogAjaxLoads { get; set; }// For First Load Only
        public List<CallLogDispositionLoad> dispositionLoads { get; set; }

        public List<calldispositiondata> reasonsList { get; set; }
        public List<workshop> workshopsList { get; set; }
       
        
        //public List<>
        public ServiceLogBucketFilter serviceLogFilter { get; set; }
        
    }

    public class ServiceLogBucketFilter
    {
        public string Campaign { get; set; }
        public DateTime? From_date { get; set; }
        public DateTime? followUP_From_date { get; set; }
        public DateTime? To_Date { get; set; }
        public DateTime? followUP_To_Date { get; set; }
        public DateTime? From_Saledate { get; set; }
        public DateTime? To_SaleDate { get; set; }
        public DateTime? lastcall_From_date { get; set; }
        public DateTime? lastcall_To_Date { get; set; }
        public DateTime? Booked_From_date { get; set; }
        public DateTime? Booked_To_Date { get; set; }
        public string Service_type { get; set; }
        public string visit_Type { get; set; }
        public string ServiceBooked_type { get; set; }
        public DateTime? LSD_From_Date { get; set; }
        public DateTime? LSD_To_Date { get; set; }
        public string workshop_Id { get; set; }
        public string Tagging { get; set; }
        public string BookingStatus { get; set; }
        public string LastDispostion { get; set; }
        public int getDataFor { get; set; }
        public string reasons { get; set; }
        public string droppedCount { get; set; }
        public string modelId { get; set; }
        public bool isFiltered { get; set; }
        //Only for advaithhyundai.....
        public string IncomingCallType { get; set; }
    }

    //For Scheduler
    public class CallLogAjaxLoad
    {

        public string customer_name { get; set; }
        public string isrobocall { get; set; }
        public string vehicle_RegNo { get; set; }
        public string veh_model { get; set; }
        public string category { get; set; }
        //public string Loyalty_Conflict { get; set; }
        public string Loyalty { get; set; }
        public string duedate { get; set; }
        public string due_type { get; set; }
        public string forecast_type { get; set; }
        public string last_psfstatus { get; set; }
        //public string DND_status_Conflict { get; set; }
        public string campaignName { get; set; }
        public string complaints_count { get; set; }
        public string vehical_Id { get; set; }
        public string wyzUser_id { get; set; }
        public string customer_id { get; set; }
        public string assigned_intercation_id { get; set; }
        public string workshop { get; set; }
        public DateTime? BillDate { get; set; }
        //public string Mobile_number_Conflict { get; set; }
        public string model { get; set; }
        public string vehicle_id { get; set; }
        public string callinteraction_id { get; set; }
        public string saname { get; set; }
        public DateTime? lastServiceDate { get; set; }
        public string tagging { get; set; }
        public string Mobile_number { set; get; }
        public string RONumber { get; set; }

        public string DND_status { get; set; }
        public string servicetype { get; set; }
        public string customercategory { get; set; }
    }

    //for FollowUp
    public class CallLogDispositionLoad
    {
        public string customer_name { get; set; }
        public string vehicle_RegNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string Mobile_number_Conflict { get; set; }
        public string callinteraction_id { get; set; }
        public string followupdate { get; set; }
        //public string followUpTime { get; set; }
        public string Last_disposition_Conflict { get; set; }
        public string scheduled_date { get; set; }
        public string scheduled_time { get; set; }
        public string reason { get; set; }
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public string duedate { get; set; }
        public string campaignName { get; set; }
        public string campaign { get; set; }
        public DateTime? calldate { get; set; }

        public DateTime? call_date { get; set; }
        public string RONumber { get; set; }
        public DateTime? ROdate { get; set; }
        public DateTime? BillDate_Conflict { get; set; }
        public string model { get; set; }
        public string category { get; set; }
        //public string followUpDate { get; set; }
        public string psfAppointmentDate { get; set; }
        public string psfAppointmentTime { get; set; }
        public string veh_model { get; set; }
        public string attempts { get; set; }
        public string workshop { get; set; }
        public DateTime? lastServiceDate { get; set; }
        public string lastServiceType { get; set; }
        public string saname { get; set; }
        public string crename { get; set; }
        public string followuptime { get; set; }
        public string duetype { get; set; }
        public string psfassignedinteraction_id { get; set; }
        public string chassisno { get; set; }
        public string feedbackrating { get; set; }
        public string resolvedBy { get; set; }
        public DateTime? resolvedDate { get; set; }
        public string Compliant_Category_Conflict { get; set; }
        public string Compliant_Category { get; set; }
        public DateTime? issuedate { get; set; }
        public int ageing { get; set; }
        public string resolutionMode { get; set; }
        public DateTime? psffollowupdate { get; set; }
        public string psffollowuptime { get; set; }
        public string calldispositiondata_id { get; set; }
        public DateTime? aptdate { get; set; }
        public string reworkMode { get; set; }
        public string apttime { get; set; }
        public string complaint_creid { get; set; }
        public string reworkStatus_id { get; set; }
        public string aptstatus { get; set; }
        public string DissatStatus_id_Conflict { get; set; }
        public string last_serviceDate { get; set; }
        public string tagging { get; set; }
        public DateTime? Servey_date { get; set; }
        public string Last_disposition { get; set; }
        public string Mobile_number { get; set; }
        public string chassiNo { get; set; }
        public string serviceBookedType { get; set; }
        public string serviceadvisor { get; set; }
        public string typeOfPickup { get; set; }
        public string booking_status { get; set; }
        //public string lastServiceType { get; set; }
        //public string lastServiceDate { get; set; }
        public string servicetype { get; set; }
        public string customercategory { get; set; }
        public string SecondaryDisposition { get; set; }
    }

    public class serviceBuckets
    {
        public string customer_name { get; set; }
        public string vehicle_RegNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string Mobile_number_Conflict { get; set; }
        public string phoneNumber { get; set; }
        public string callinteraction_id { get; set; }
        public string followupdate { get; set; }
        public string Last_disposition_Conflict { get; set; }
        public string scheduled_date { get; set; }
        public string scheduled_time { get; set; }
        public string reason { get; set; }
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public string duedate { get; set; }
        public string campaignName { get; set; }
        public string campaign { get; set; }
        public DateTime? calldate { get; set; }
        public DateTime? call_date { get; set; }
        public string RONumber { get; set; }
        public DateTime? ROdate { get; set; }
        public DateTime? BillDate_Conflict { get; set; }
        public string model { get; set; }
        public string category { get; set; }
        public string psfAppointmentDate { get; set; }
        public string psfAppointmentTime { get; set; }
        public string veh_model { get; set; }
        public string attempts { get; set; }
        public string workshop { get; set; }
        public DateTime? lastServiceDate { get; set; }
        public string lastServiceType { get; set; }
        public string saname { get; set; }
        public string crename { get; set; }
        public string followuptime { get; set; }
        public string duetype { get; set; }
        public string psfassignedinteraction_id { get; set; }
        public string chassisno { get; set; }
        public string feedbackrating { get; set; }
        public string resolvedBy { get; set; }
        public DateTime? resolvedDate { get; set; }
        public string Compliant_Category_Conflict { get; set; }
        public string Compliant_Category { get; set; }
        public DateTime? issuedate { get; set; }
        public int ageing { get; set; }
        public string resolutionMode { get; set; }
        public DateTime? psffollowupdate { get; set; }
        public string psffollowuptime { get; set; }
        public string calldispositiondata_id { get; set; }
        public DateTime? aptdate { get; set; }
        public string reworkMode { get; set; }
        public string apttime { get; set; }
        public string complaint_creid { get; set; }
        public string reworkStatus_id { get; set; }
        public string aptstatus { get; set; }
        public string DissatStatus_id_Conflict { get; set; }
        public string last_serviceDate { get; set; }
        public string tagging { get; set; }
        public DateTime? Servey_date { get; set; }
        public string Last_disposition { get; set; }
        public string Mobile_number { get; set; }
        public string chassiNo { get; set; }
        public string serviceBookedType { get; set; }
        public string serviceadvisor { get; set; }
        public string typeOfPickup { get; set; }
        public string booking_status { get; set; }
        public string Loyalty { get; set; }
        public string due_type { get; set; }
        public string forecast_type { get; set; }
        public string last_psfstatus { get; set; }
        public string complaints_count { get; set; }
        public string vehical_Id { get; set; }
        public string wyzUser_id { get; set; }
        public string assigned_intercation_id { get; set; }
        public string DND_status { get; set; }
    }
    //Counts
    public class serviceFilterCount
    {

        public long id { get; set; }
        public long counts { get; set; }

    }
}