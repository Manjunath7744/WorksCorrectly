namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("servicebookedsynced")]
    public partial class servicebookedsynced
    {
        public long id { get; set; }

        [StringLength(255)]
        public string customerPhone { get; set; }

        [StringLength(255)]
        public string smsType { get; set; }
    }
}
