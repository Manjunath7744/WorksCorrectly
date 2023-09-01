using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class ForecastVM
    {
        public List<campaign> campaigns { get; set; }
        public List<location> locations { get; set; }
        public List<forecastlogicservicetype> forecastlogicservicetypes { get; set; }
        public List<wyzuser> wyzusers { get; set; }

    }
}