namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("upselllead")]
    public partial class upselllead
    {
        public long id { get; set; }

        [StringLength(50)]
        public string taggedTo { get; set; }

        [StringLength(50)]
        public string upSellType { get; set; }

        [StringLength(1073741823)]
        public string upsellComments { get; set; }

        public long? srDisposition_id { get; set; }

        public long? taggingUsers_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? insuranceDisposition_id { get; set; }

        public long? psfInteraction_id { get; set; }

        public int? induspsfinteraction_Id { get; set; }

        public int? upsellId { get; set; }

        public virtual insurancedisposition insurancedisposition { get; set; }

        public virtual psfinteraction psfinteraction { get; set; }

        public virtual IndusPSFInteraction induspsfinteraction { get; set; }

        public virtual srdisposition srdisposition { get; set; }

        public virtual tagginguser tagginguser { get; set; }

        public virtual vehicle vehicle { get; set; }
    }
}
