using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class SRDispositionDataOnTabLoad
    {
        public string callDisposition { get; set; }
        public string comments { get; set; }
        public string feedback { get; set; }
        public string typeOfCall { get; set; }
        public string assignId { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string FollowUpTime { get; set; }
        public string NoServiceReason { get; set; }
        public long kms { get; set; }
    }
}
