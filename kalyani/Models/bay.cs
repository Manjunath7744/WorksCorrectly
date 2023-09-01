namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bay")]
    public partial class bay
    {
        public long bayId { get; set; }

        public long bayCapacityperday { get; set; }

        [StringLength(255)]
        public string workshopName { get; set; }

        public long? workshop_id { get; set; }

        public virtual workshop workshop { get; set; }
    }
}
