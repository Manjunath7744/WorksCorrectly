using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class liveReportViewModel
    {

    }
    public class reportFilter
    {
        public long reportId { get; set; }
        public string selected_CRE { get; set; }
        public string selected_Workshop { get; set; }
        public string selected_Campaign { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
    public class downloadReportFilter
    {
        public int reportId { get; set; }
        public string workshopLists { get; set; }
        public string smstypeLists { get; set; }
        public string smsstatusLists { get; set; }
        public string campaignLists { get; set; }
        public string creLists { get; set; }
        public string callfromDate { get; set; }
        public string smsdateFrom { get; set; }
        public string calltoDate { get; set; }
        public string smsdateTo { get; set; }
        public string reportName { get; set; }
        public string smstype { get; set; }
        public string billdateFrom { get; set; }
        public string billdateTo { get; set; }
        public string assigndateFrom { get; set; }
        public string assigndateTo { get; set; }
        public string connectionId { get; set; }
        public string ispending { get; set; }
        public string worktypelists { get; set; }
        public string roStatus { get; set; }
    }

    public class creTodaysLiveReport
    {
        public string CallTime { get; set; }
        public string VinNo { get; set; }
        public string VehRegno { get; set; }
        public string Dispostion { get; set; }
        public string comment { get; set; }
    }
    public class barchartVM
    {
        public string TimeInterval { get; set; }
        public string Calls { get; set; }
    }
}