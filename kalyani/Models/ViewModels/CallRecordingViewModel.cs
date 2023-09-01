using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoSherpa_project.Models;

namespace AutoSherpa_project.Models.ViewModels
{
    public class CallRecordingViewModel
    {
       public List<calldispositiondata> dispoList { get; set; }
       public List<campaign> campaignList { get; set; }
       public List<calldispositiondata> reasonsList { get; set; }
       public List<wyzuser> creList { get; set; }
    }

    public class RecordingFilters
    {
        public int FilterFor { get; set; }

        public DateTime? Call_Date_from { get; set; }
        public DateTime? Call_Date_to { get; set; }
        public string calltype { get; set; }
        public string   campaign { get; set; }
        public string CRMCallType { get; set; }
        public string disposition { get; set; }
        public string cresName { get; set; }
        public string SecondReason { get; set; }
        public bool isFiltered { get; set; }
        public string callStatus { get; set; }
    }
}