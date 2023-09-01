using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("driverbookingdatetime")]
    public class driverbookingdatetime
    {
        public long id { get; set; }

        [StringLength(255)]
        public string timeRange { get; set; }

        public TimeSpan? startTime { get; set; }

        public TimeSpan? endTime { get; set; }
    }
}