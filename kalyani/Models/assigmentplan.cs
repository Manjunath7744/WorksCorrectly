namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assigmentplan")]
    public partial class assigmentplan
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool commercialVehicle { get; set; }

        [Column(TypeName = "bit")]
        public bool? insurance { get; set; }

        public long? locationId { get; set; }

        [Column(TypeName = "bit")]
        public bool? psf { get; set; }

        public long serviceTypeId { get; set; }

        [Column(TypeName = "bit")]
        public bool? smr { get; set; }

        public long? wyzuserId { get; set; }

        public long? campaign_id { get; set; }

        public DateTime? created_date { get; set; }

        public long renewalTypeId { get; set; }

        [Column(TypeName = "bit")]
        public bool dissat { get; set; }

        [Column(TypeName = "bit")]
        public bool iscei { get; set; }

        [Column(TypeName = "bit")]
        public bool isbodyshop { get; set; }
    }
}
