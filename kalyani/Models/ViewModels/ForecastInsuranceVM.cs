using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class ForecastInsuranceVM
    {
        public List<location> locations { get; set; }
        public List<campaign> campaigns { get; set; }
        public List<wyzuser> wyzusers { get; set; }
        public List<renewaltype> renewaltypes { get; set; }
        public List<workshop> workshops { get; set; }
        public string FilterOn { get; set; }
    }

}