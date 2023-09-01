using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class TaggingView
    {
        public string name { get; set; }
        public long upsellLeadId { get; set; }
        public string upsellType { get; set; }
        public long wyzUserId { get; set; }
    }
}