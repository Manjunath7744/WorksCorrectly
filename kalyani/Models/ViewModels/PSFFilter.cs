using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class PSFFilter
    {
        public DateTime? ExFromBillDate { get; set; }
        public DateTime? ExToBillDate { get; set; }
        public DateTime? ExFromCallDate { get; set; }
        public DateTime? ExToCallDate { get; set; }
        public int getDataFor { get; set; }
        public int ageing { get; set; }
        public string workshop { get; set; }
        public string serviceType { get; set; }
        public string customerCategory { get; set; }
        public bool isFiltered { get; set; }
        public string ddllastdisposition { get; set; }
    }
}