using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class KatariaEmailBodyTemplates
    {
        public senderBody sender { get; set; }
        public List<senderBody> to { get; set; }
        //public List<senderBody> cc { get; set; }
        public string subject { get; set; }
        public string htmlContent { get; set; }
    }
    public class senderBody{
        public string name { get; set; }
        public string email { get; set; }
   
    }
}