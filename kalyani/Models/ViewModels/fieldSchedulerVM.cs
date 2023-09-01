using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class fieldSchedulerVM
    {
       public string regNo { get; set; }
       public string aptDateTime { get; set; }
       public string assigned { get; set; }
       public string pin { get; set; }
       public string addressOfVisit { get; set; }
       public string type { get; set; }
       public string fieldExec { get; set; }
        public string scheduleTime { get; set; }
        public string custName { get; set; }
        public string phone { get; set; }
        public string chassisNum { get; set; }
        public string cre { get; set; }
        public string apptId { get; set; }
        public string pickUpDate { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string wyzUserID { get; set; }
        public string vehicleID { get; set; }
        public string customerID { get; set; }
    }
    public class fieldSchedulerFilter
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string fieldLocation { get; set; }
        public string appointmentType { get; set; }
        public string assignedStatus { get; set; }

    }
}