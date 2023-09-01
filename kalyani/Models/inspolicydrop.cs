using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("inspolicydrop")]
    public class inspolicydrop
    {
        [Key]
        public long id { get; set; }
        
        public string appointment_time { get; set; }
        
        [Column(TypeName ="date")]
        public DateTime appointment_date { get; set; }

        public long? vehicle_id { get; set; }

        public long? customer_id { get; set; }

        public long? agent_id { get; set; }

        public long? wyzuser_id { get; set; }
        
        public string address { get; set; }

        public string custRemarks { get; set; }

        public string creRemarks { get; set; }

        public long? lastagent_id { get; set; }

        public DateTime updated_datetime { get; set; }

        public string wyzuser_name { get; set; }

        public bool iscancelled { get; set; }

        public long? location_id { get; set; }

        public int? pincode { get; set; }

        [NotMapped]
        public string LocationName { get; set; }

        [NotMapped]
        public string AgentName { get; set; }
    }
}