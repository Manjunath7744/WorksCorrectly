using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class servicePushVM
    {
        public string service_category { get; set; }
        public string customer_name { get; set; }
        public string customer_address_present { get; set; }
        public string customer_address_office { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string chassis_no { get; set; }
        public string engine_no { get; set; }
        public string registration_no { get; set; }
        public string pickup_location { get; set; }
        public string pickup_date_time { get; set; }
        public string mobile_no1 { get; set; }
        public string mobile_no2 { get; set; }
        public string mobile_no3 { get; set; }
        public string workshop_name { get; set; }
        public string smr_name { get; set; }
        public string other_information { get; set; }
    }
}