using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    public class firebaseData
    {
        public long ageOfVehicle { get; set; }

        public string agentName { get; set; }

        public string callDate { get; set; }

        public string callDuration { get; set; }

        public string callTime { get; set; }

        public string callType { get; set; }

        public string callTypePicId { get; set; }

        public string customerName { get; set; }

        public string customerPhone { get; set; }

        public long daysBetweenVisit { get; set; }

        public string filePath { get; set; }

        public string fileSize { get; set; }

        public string interactionDate { get; set; }

        public string interactionTime { get; set; }

        public string latitude { get; set; }

        public string logitude { get; set; }

        public string ringTime { get; set; }

        public long ringingTime { get; set; }

        public long uniqueidForCallSync { get; set; }

        //Think extra
        public byte[] mediaFileLob { get; set; }

        public string dealerCode { get; set; }

    }
}