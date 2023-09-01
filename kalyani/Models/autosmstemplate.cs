namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("autosmstemplates")]
    public partial class autosmstemplate
    {
        public int id { get; set; }

        [StringLength(355)]
        public string smsAPI { get; set; }

        [StringLength(1000)]
        public string smsTemplate { get; set; }

        [StringLength(100)]
        public string smsType { get; set; }

        [Column(TypeName = "bit")]
        public bool? active { get; set; }

        public long? moduletypeId { get; set; }

        public long? day { get; set; }

        public TimeSpan? smsstartfrom { get; set; }

        public TimeSpan? sendsmsupto { get; set; }
    }
}
