using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class ChangeAssignment
    {
        public string CreName { get; set; }
        public string finalDisposition_id { get; set; }
        public string customer_name { get; set; }
        public long id { get; set; }
        public string Mobile_number { get; set; }
        public string vehicle_RegNo { get; set; }
        public long vehical_Id { get; set; }
        public long customer_id { get; set; }
        public long wyzUser_id { get; set; }
        public DateTime? uplodedCurrentDate { get; set; }
        public long campaign_id { get; set; }
        public string module { get; set; }
        public string campaignName { get; set; }
        public string serviceType { get; set; }
        public string renewalType { get; set; }
        public string locationName { get; set; }
        public string assignPlan { get; set; }
        public string category { get; set; }
        public long count { get; set; }
        public long totalUploaded { get; set; }
        public long totalAssigned { get; set; }
        public long totalUnassigned { get; set; }
        public long alreadyInBucket { get; set; }
        public long totalRejected { get; set; }
        public long excelError { get; set; }
        public long rejectedInvalidWorkshopId { get; set; }
        public string disposition { get; set; }

        public long servicebookedId { get; set; }
        //by nisarga
        public string workshopname { get; set; }
        public string nextServiceTypeId { get; set; }
    }
}