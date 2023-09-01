namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("irda_od_premium")]
    public partial class irda_od_premium
    {
        public long id { get; set; }

        [StringLength(255)]
        public string cubicCapacity { get; set; }

        public double odPercentage { get; set; }

        [StringLength(255)]
        public string vehicleAge { get; set; }

        [StringLength(255)]
        public string zone { get; set; }

        public double thirdPartyPremium { get; set; }

        public long paLL { get; set; }

        [StringLength(255)]
        public string vehicleType { get; set; }
    }
}
