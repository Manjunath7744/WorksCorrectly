using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("userlogs")]
    public class userlogs
    {
        [Key]
        public long id { get; set; }

        public long wyzuserId { get; set; }

        public string username { get; set; }

        public string managername { get; set; }

        public DateTime loginDateTime { get; set; }

        public DateTime? logoutDateTime { get; set; }

        public string hostaddress { get; set; }

        public string sessionid { get; set; }

        public string dealerCode { get; set; }
    }
}