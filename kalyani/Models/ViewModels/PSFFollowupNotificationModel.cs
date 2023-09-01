using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{

    public class PSFFollowupNotificationModel
    {

        public long customer_id { get; set; }
        public long campaign_id { get; set; }
        public long callinteraction_id { get; set; }
        public long psfAssignedInteraction_id { get; set; }
        public long vehicle_id { get; set; }

        public string customer_name { get; set; }
        public string Mobile_number { get; set; }
        public string chassisno { get; set; }
        public string vehicle_RegNo { get; set; }

        public string campaignName { get; set; }
        public string RONumber { get; set; }
        public string workshop { get; set; }
        public string BillDate { get; set; }
        public string psfFollowUpTime { get; set; }

        //Extra for Complaints
        public DateTime? issuedate { get; set; }
        public int ageing { get; set; }
        public DateTime? psffollowupdate { get; set; }
        public DateTime? aptdate { get; set; }
        public string apttime { get; set; }
        public string reworkMode { get; set; }
        public long attempts { get; set; }
        public long complaint_creid { get; set; }
        public long reworkStatus_id { get; set; }
        public string aptstatus { get; set; }
        public string Last_disposition { get; set; }

        public string psfLoginUser { get; set; } //For Live Notification

    }
}
