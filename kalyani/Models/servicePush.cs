using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("servicePush")]
    public class servicePush
    {
        [Key]
        public long id { get; set; }
        public long? callHistoryCubeId { get; set; }
        public DateTime? updateDateAndTime { get; set; }
    }
}