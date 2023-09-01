using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("fetracking")]
    public class FETracking
    {
        [Key]
        public long Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public string LoginDate { get; set; }

        public string LoginTime { get; set; }

        public string Device { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public long WyzUserId { get; set; }
        public string FEName { get; set; }
        public string UniqueID { get; set; }
        public string Flag { get; set; }
        public string LogoutDate { get; set; }
        public string LogoutTime { get; set; }
        public string LogoutLocation { get; set; }
    }
}