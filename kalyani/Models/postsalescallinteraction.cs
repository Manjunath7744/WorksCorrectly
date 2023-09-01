using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("postsalescallinteraction")]

    public class postsalescallinteraction
    {
        public long id { get; set; }

        public string agentName { get; set; }

        public long callCount { get; set; }

        public string callDate { get; set; }

        public string callDuration { get; set; }

        public DateTime? callMadeDateAndTime { get; set; }

        public string callTime { get; set; }

        public string callType { get; set; }

        public int callTypePicId { get; set; }

        public bool dailedStatus { get; set; }

        public string dealerCode { get; set; }

        public long droppedCount { get; set; }

        public string filePath { get; set; }

        public string firebaseKey { get; set; }

        public string isCallinitaited { get; set; }

        public bool isComplaintCall { get; set; }

        public bool isDriverCall { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public string makeCallFrom { get; set; }


        public string serviceAdvisorfirebaseKey { get; set; }

        public string synchedToFirebase { get; set; }

        public int uniqueidForCallSync { get; set; }

        public long? postsalesassignedInteraction_id { get; set; }

        public long? customer_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        public string dailedNoIs { get; set; }

        public string ringTime { get; set; }

        public long? campaign_id { get; set; }

        public bool? chasserCall { get; set; }

        public string uniqueAndroidId { get; set; }

        public string fileSize { get; set; }

        public long dissatPSFintId { get; set; }
        public long workshopid { get; set; }

        public string uniqueIdGSM { get; set; }

    
        public bool isComplaint { get; set; }

        public bool callStatus { get; set; }

        public bool isResolved { get; set; }

    }
}