using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class PSF_CallLogging
    {
        public wyzuser wyzuser { get; set; }
        public List<workshop> workshopList { get; set; }
        public List<workshop> allworkshopList { get; set; }
        public customer cust { get; set; }
        public vehicle vehi { get; set; }
        public service lastService { get; set; }
        public psfassignedinteraction lastPSFAssignStatus { get; set; }
        public psfassignedinteraction lastPSFAssign { get; set; }
        public psfinteraction psfLastInteraction { get; set; }
        public insurance LatestInsurance { get; set; }
        public List<smstemplate> templates { get; set; }
        public customer custoAdd { get; set; }
        public List<psf_qt_history> psf_Qt_History { get; set; }
        public List<phone> phonesAdd { get; set; }
        public phone phoneAdd { get; set; }
    }
}