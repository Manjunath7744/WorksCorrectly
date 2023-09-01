using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class CallLoggingCustomerAddressVM
    {
        public long addressId { get; set; }
        public string address { get; set; }
        public long pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public DateTime? updateOn { get; set; }
        public string updatedBy { get; set; }
        public bool isPrimary { get; set; }
    }
}