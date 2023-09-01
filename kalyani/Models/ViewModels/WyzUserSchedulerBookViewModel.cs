using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
namespace AutoSherpa_project.Models.ViewModels
{
    public class WyzUserSchedulerBookViewModel
    {
        public List<wyzuser> wyzusers { get; set; }
        public List<fieldwalkinlocation> fieldwalkinlocations { get; set; }
        public List<PickupDropDataOnTabLoad> pickupDropDataOnTabs { get; set; }
        public List<bookingdatetime> bookingdatetimes { get; set; }
        public List<pickupdrop> pickupdrops { get; set; }


    }
}