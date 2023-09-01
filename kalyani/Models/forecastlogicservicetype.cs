namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("forecastlogicservicetype")]
    public partial class forecastlogicservicetype
    {
        public long id { get; set; }

        [Required]
        [StringLength(150)]
        public string servicetype { get; set; }

        [Column(TypeName = "bit")]
        public bool inactive { get; set; }
    }
}
