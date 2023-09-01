using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class driverImageVM
    {
        public List<DriverAppFileDetails> driverAppFileDetails{get;set;}
        public string paths { get; set; }
        public string vehicleRegNo { get; set; }
        public string PickupTime { get; set; }
        public string DeliveryTime { get; set; }
        public string pickupstartedlocation { get; set; }
        public string dropstartedlocation { get; set; }
        public string pickupstopedlocation { get; set; }
        public string dropstopedlocation { get; set; }
        public string dropkmtravelled { get; set; }
        public string pickupkmtravelled { get; set; }
    }
}