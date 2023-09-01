using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("postsalescallhistory_stage1")]
    public class postsalescallhistory_stage1
    {
        public int id { get; set; }
        public string assignedCRE { get; set; }
        public DateTime? assignedDate { get; set; }
        public long? vehicle_vehicle_id { get; set; }
        public long? customer_id { get; set; }
        public long? postAssignedInteraction_id { get; set; }
        public long? cicallinteraction_id { get; set; }
        public string creName { get; set; }
        public DateTime callDate { get; set; }
        public string callTime { get; set; }
        public string isCallinitaited { get; set; }
        public string callType { get; set; }
        public string callDuration { get; set; }
        public string ringTime { get; set; }
        public string psfCallingDayType { get; set; }
        public string chassisNo { get; set; }
        public string model { get; set; }
        public string variant { get; set; }
        public DateTime saleDate { get; set; }
        public string vehicleRegNo { get; set; }
        public string customerName { get; set; }
        public string preffered_address { get; set; }
        public string prefferedPhoneNumber { get; set; }
        public string Workshop { get; set; }
        public string PrimaryDisposition { get; set; }
        public string SecondaryDisposition { get; set; }
        public string Comments { get; set; }
        public string isContacted { get; set; }
        public DateTime? psfFollowUpDate { get; set; }
        public string psfFollowUpTime { get; set; }
        public string IsSatisfied { get; set; }
        public string Remarks { get; set; }
        public long? calldispositiondata_id { get; set; }
        public string filepath { get; set; }

        public DateTime? updatedDate { get; set; }
        public string cremanager { get; set; }
        public string wyzUser_id { get; set; }
        public string makeCallFrom { get; set; }
        public long? fileSize { get; set; }
        public string dailedNumber { get; set; }
        public long? campaign_id { get; set; }
        public long? workshop_id { get; set; }
        public long? Compliant_Category_id { get; set; }
        public long? complaint_creid { get; set; }
        public string complaint_crename { get; set; }

        public DateTime? issuedate { get; set; }
        public long? reworkStatus_id { get; set; }
        public long? DissatStatus_id { get; set; }
        public string uniqueIdGSM { get; set; }
        public string qos { get; set; }
        public string isResolved { get; set; }
        public string attendedBy { get; set; }
        public bool isComplaint { get; set; }
        public string gsm_android { get; set; }
        public bool callStatus { get; set; }
        public string q1 { get; set; }
        public string q2 { get; set; }
        public string q3 { get; set; }
        public string q4 { get; set; }
        public string q5 { get; set; }
        public string q6 { get; set; }
        public string q7 { get; set; }
        public string q8 { get; set; }
        public string q9 { get; set; }
        public string q10 { get; set; }
        public string q11 { get; set; }
        public string q12 { get; set; }
        public string q13 { get; set; }
        public string q14 { get; set; }
        public string q15 { get; set; }
        public string q16 { get; set; }
        public string q17 { get; set; }
        public string q18 { get; set; }
        public string q19 { get; set; }
        public string q20 { get; set; }
        public string q21 { get; set; }
        public string q22 { get; set; }
        public string q23 { get; set; }
        public string q24 { get; set; }
        public string q25 { get; set; }
        public string q26 { get; set; }
        public string q27 { get; set; }
        public string q28 { get; set; }
        public string q29 { get; set; }
        public string q30 { get; set; }
        public string q31 { get; set; }
        public string q32 { get; set; }
        public string q33 { get; set; }
        public string q34 { get; set; }
        public string q35 { get; set; }
        public string q36 { get; set; }
        public string q37 { get; set; }
        public string q38 { get; set; }
        public string q39 { get; set; }
        public string q40 { get; set; }
        public int salesworkshop_id { get; set; }
        public string modelcat { get; set; }
        [NotMapped]
        public string salesoutlet { get; set; }
        [NotMapped]
        public string DSE { get; set; }
    }
}