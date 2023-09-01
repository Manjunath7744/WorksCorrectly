namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insautoassignmentsummary")]
    public partial class insautoassignmentsummary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(45)]
        public string filename { get; set; }

        public long? totalData { get; set; }

        public long? assignedData { get; set; }

        public long? alreadyAvailable { get; set; }

        public DateTime? uploadeddateTime { get; set; }
    }
}
