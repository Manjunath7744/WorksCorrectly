namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("renewaltypes")]
    public partial class renewaltype
    {
        public long id { get; set; }

        [StringLength(100)]
        public string renewalTypeName { get; set; }
    }
}
