namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("taggedassignment")]
    public partial class taggedassignment
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool active { get; set; }

        public long assignedinteraction_id { get; set; }

        public long assignedtag { get; set; }

        public long module_id { get; set; }

        public long tagging_id { get; set; }

        public DateTime? taggingdate { get; set; }

        public long vehicle_id { get; set; }
    }
}
