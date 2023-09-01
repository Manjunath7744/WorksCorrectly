namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workshopsummary")]
    public partial class workshopsummary
    {
        public long id { get; set; }

        public int noOfServicesBooked { get; set; }

        [Column(TypeName = "date")]
        public DateTime? serviceBookedDate { get; set; }

        public long? workshop_id { get; set; }

        public virtual workshop workshop { get; set; }
    }
}
