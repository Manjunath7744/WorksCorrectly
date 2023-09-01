using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class InsurenceViewModel
    {
        public List<CallLogAjaxLoadInsurance> callLogsInsurence { get; set; }
    }

    public class InsurenceFilter
    {
        public string Campaign { get; set; }
        public DateTime? ExFromDate { get; set; }
        public DateTime? ExToDate { get; set; }
        public string Flag { get; set; }
        public string LocationId { get; set; }

        public int getDataFor { get; set; }

        public string InsuCmp { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public DateTime? fromsaleDate { get; set; }
        public DateTime? tosaleDate { get; set; }

        public string attempts { get; set; }
        public string AppointmentType { get; set; }
        public string reasons { get; set; }
        public string BookingStatus { get; set; }
        public string pattern { get; set; }
        public bool isFiltered { get; set; }
        public string model { get; set; }
        public string modelcategory { get; set; }
        public string renewalTypelist { get; set; }
        public string Followupfromdate { get; set; }
        public string Followuptodate { get; set; }
        public string FollowupReason { get; set; }
        public string DueDateEdited { get; set; }
        public string LastserviceFromDate { get; set; }
        public string LastserviceToDate { get; set; }
         public string LastDispostion { get; set; }
        public string Tagging { get; set; }

    }

    public class CallLogAjaxLoadInsurance
    {
        public string Flag { get; set; }
        public string model { get; set; }
        public long customer_id { get; set; }
        public long vehicle_vehicle_id { get; set; }
        public long wyzUser_id { get; set; }
        public long assigned_intercation_id { get; set; }
        public long insuranceassigned_id { get; set; }
        public string campaign { get; set; }
        public string customerName { get; set; }
        public string Phone { get; set; }
        public string RegNo { get; set; }
        public string chassisNo { get; set; }
        public string policyDueMonth { get; set; }
        public DateTime? policyDueDate { get; set; }
        public string NextRenewalType { get; set; }
        public string lastInsuranceCompany { get; set; }
        public string brand { get; set; }
        public string campaignName { get; set; }
        public string customerId { get; set; }
        public string customer_name { get; set; }
        public string phoneNumber { get; set; }
        public string vehicle_RegNo { get; set; }
        public string renewalType { get; set; }
        public string policy_due_month { get; set; }
        public DateTime? duedate { get; set; }
        public string last_insuirancecompanyname { get; set; }
        public string location { get; set; }
        public string assigned { get; set; }
        public string assignedDate { get; set; }
        public string creName { get; set; }
        public string lastDipso { get; set; }
        public DateTime? saledate { get; set; }

        public string appointmentTime { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string appointmentType { get; set; }
        public string appointmentStatus { get; set; }
        public string workshopname { get; set; }

        //Nisarga added
        public long? locationID { get; set; }
        public long? campaignID { get; set; }
        public string nextRenewalTypeID { get; set; }
        public string modelCategory { get; set; }
        public long workshopId { get; set; }
    }
    //Counts
    public class insurenceFilterCount
        {

        public long id { get; set; }
        public long counts { get; set; }

        }

    public class CallLogDispositionLoadInsurance
    {

        public long callinteraction_id { get; set; }
        public string customer_name { get; set; }
        public string vehicle_RegNo { get; set; }
        public DateTime? duedate { get; set; }
        public string dispoDate { get; set; }
        public string renewalType { get; set; }
        public string dispoTime { get; set; }
        public string appointmentToTime { get; set; }
        public string reason { get; set; }
        public long vehicle_id { get; set; }
        public long customer_id { get; set; }
        public long no_of_attempts { get; set; }
        public string campaignName { get; set; }
        public string vehicleRegNo { get; set; }
        public DateTime? call_date { get; set; }
        public string Mobile_number { get; set; }
        public string fieldwalkingloc { get; set; }

        public string phonenumber { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string appointmentTime { get; set; }
        public string typeOfPickup { get; set; }
        public string appointmentFromTime { get; set; }
        public string insurance_agent { get; set; }
        public string Last_disposition { get; set; }
        public string followUpDate { get; set; }
        public string followUpTime { get; set; }
        public DateTime? policyDueDate { get; set; }
       // public DateTime? policyExpiryDate { get; set; }
        public string policyExpiryDate { get; set; }
        public string appointmentstatus { get; set; }
        public string appointment_status { get; set; }
        public string insuranceAgent { get; set; }
        public long droppedCount { get; set; }
        public string chassisNo { get; set; }
        public string chassiNo { get; set; }
        public string flag { get; set; }
        public string appointmentType { get; set; }
        public string insuranceCompany { get; set; }

        public string fse { get; set; }
        public string model { get; set; }
        public string reasons { get; set; }
        public string brand { get; set; }
        public long count { get; set; }
        public string Location { get; set; }
        public string ra_fresh { get; set; }
        public string ra_assigned { get; set; }
        public string rl_fresh { get; set; }
        public string rl_assigned { get; set; }
        public string ra_od_fresh { get; set; }
        public string ra_od_assigned { get; set; }
        public string rl_od_fresh { get; set; }
        public string rl_od_assigned { get; set; }
        public string sales_fresh { get; set; }
        public string sales_assigned { get; set; }
        public string service_fresh { get; set; }
        public string service_assigned { get; set; }
        public string total { get; set; }
        public string ra_total { get; set; }
        public string rl_total { get; set; }
        public string ra_od_total { get; set; }
        public string rl_od_total { get; set; }
        public string sales_total { get; set; }
        public string service_total { get; set; }
        public string vehLoc { get; set; }
    }
    public class insuranceBuketData
    {
        public string Flag { get; set; }
        public string model { get; set; }
        public long customer_id { get; set; }
        public long vehicle_vehicle_id { get; set; }
        public long wyzUser_id { get; set; }
        public long assigned_intercation_id { get; set; }
        public long insuranceassigned_id { get; set; }
        public string campaign { get; set; }
        public string customerName { get; set; }
        public string Phone { get; set; }
        public string RegNo { get; set; }
        public string chassisNo { get; set; }
        public string policyDueMonth { get; set; }
        public DateTime? policyDueDate { get; set; }
        public string NextRenewalType { get; set; }
        public string lastInsuranceCompany { get; set; }
        public string brand { get; set; }
        public string campaignName { get; set; }
        public string customerId { get; set; }
        public string customer_name { get; set; }
        public string vehicle_RegNo { get; set; }
        public string renewalType { get; set; }
        public string policy_due_month { get; set; }
        public DateTime? duedate { get; set; }
        public string last_insuirancecompanyname { get; set; }
        public string location { get; set; }
        public string assigned { get; set; }
        public string assignedDate { get; set; }
        public string creName { get; set; }
        public string lastDipso { get; set; }
        public DateTime? saledate { get; set; }
        public string appointmentTime { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string appointmentType { get; set; }
        public string workshopname { get; set; }
        public string Mobile_number { get; set; }
        public DateTime? call_date { get; set; }
        public long no_of_attempts { get; set; }
        public string followUpDate { get; set; }
        public string followUpTime { get; set; }
        public long callinteraction_id { get; set; }
        public string dispoDate { get; set; }
        public string dispoTime { get; set; }
        public string appointmentToTime { get; set; }
        public string reason { get; set; }
        public long vehicle_id { get; set; }
        public string vehicleRegNo { get; set; }
        public string fieldwalkingloc { get; set; }

        public string phonenumber { get; set; }
        public string typeOfPickup { get; set; }
        public string appointmentFromTime { get; set; }
        public string insurance_agent { get; set; }
        public string Last_disposition { get; set; }
        public string policyExpiryDate { get; set; }
        public string appointmentstatus { get; set; }
        public string appointment_status { get; set; }
        public string insuranceAgent { get; set; }
        public long droppedCount { get; set; }
        public string chassiNo { get; set; }
        public string insuranceCompany { get; set; }
        public string fse { get; set; }
        public string reasons { get; set; }
        public long count { get; set; }
        public string ra_fresh { get; set; }
        public string ra_assigned { get; set; }
        public string rl_fresh { get; set; }
        public string rl_assigned { get; set; }
        public string ra_od_fresh { get; set; }
        public string ra_od_assigned { get; set; }
        public string rl_od_fresh { get; set; }
        public string rl_od_assigned { get; set; }
        public string sales_fresh { get; set; }
        public string sales_assigned { get; set; }
        public string service_fresh { get; set; }
        public string service_assigned { get; set; }
        public string total { get; set; }
        public string ra_total { get; set; }
        public string rl_total { get; set; }
        public string ra_od_total { get; set; }
        public string rl_od_total { get; set; }
        public string sales_total { get; set; }
        public string service_total { get; set; }
        public string vehLoc { get; set; }
        public string tagging { get; set; }
    }

    public class coupon_viewdetails
    {
        public string coupon_uniqueid { get; set; }
        public string coupon_name { get; set; }
        public string wyzuser_id { get; set; }
        public string couponleft { get; set; }
        public string coupondetails { get; set; }

        //public string couponName { get { return string.Concat(coupon_name + "(" + couponleft + ")"); } }
    }
}