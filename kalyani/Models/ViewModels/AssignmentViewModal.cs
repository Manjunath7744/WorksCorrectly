using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoSherpa_project.Models;

namespace AutoSherpa_project.Models.ViewModels
{
    public class AssignmentViewModal
    {
        public List<ChangeAssignment> changeAssignDetails { get; set; }

        public long  uploadId { get; set; }
    }

    public class SMRAssignmentFilter
    {
        public string AllCampaign { get; set; }
        public string PSFCampaign { get; set; }
        public string CampaignName { get; set; }
        public string location { get; set; }
        public string workshop { get; set; }
        public DateTime? from_date { get; set; }
        public DateTime? to_date { get; set; }
        public string vehicle_model { get; set; }
    }
}