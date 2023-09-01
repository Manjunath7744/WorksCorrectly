using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class TGSRequest
    {
        public string template_id { get; set; }
        public string sender { get; set; }
        public List<IDictionary<string, string>>  recipients { get; set; }
    }
}