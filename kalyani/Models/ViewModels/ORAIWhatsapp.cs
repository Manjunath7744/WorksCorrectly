using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class ORAIWhatsapp
    {
        public string From { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
    }
}