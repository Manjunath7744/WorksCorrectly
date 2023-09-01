namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("modelcache")]
    public partial class modelcache
    {
        public long id { get; set; }

        [StringLength(1073741823)]
        public string updateContent { get; set; }
    }
}
