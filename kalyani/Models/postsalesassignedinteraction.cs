using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("postsalesassignedinteraction")]

    public class postsalesassignedinteraction
    {

        public long id { get; set; }

        public string callMade { get; set; }

        public bool displayFlag { get; set; }

        public string interactionType { get; set; }

        public string lastDisposition { get; set; }

        public DateTime? uplodedCurrentDate { get; set; }

        public long? campaign_id { get; set; }

        public long? customer_id { get; set; }

        public long? finalDisposition_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        public long? workshop_id { get; set; }

        public long? upload_id { get; set; }

        public bool isResolved { get; set; }
        public long salesworkshop_id { get; set; }
        public string psf1 { get; set; }
        public string psf2 { get; set; }
        public string psf3 { get; set; }
       
    }
}