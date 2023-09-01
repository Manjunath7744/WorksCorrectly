using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class CallRecordingDataTable
    {

        public long callinteraction_id { get; set; }
        public long customer_id { get; set; }
        public long vehicle_vehicle_id { get; set; }
        public DateTime? callDate { get; set; }
        public string callTime { get; set; }
        public string callType { get; set; }
        public string isCallinitaited { get; set; }
        public string userName { get; set; }
        public string campaignName { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public string vehicleRegNo { get; set; }
        public string chassisNo { get; set; }
        public string Disposition { get; set; }
        public string media { get; set; }
        public string noServiceReason { get; set; }
        public long? filesize { get; set; }
        public string callstatus { get; set; }
        public string call_mode { get; set; }
    }
}