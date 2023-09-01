namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("forecastservicetypes")]
    public partial class forecastservicetype
    {
        public long id { get; set; }

        [StringLength(255)]
        public string mainType { get; set; }

        [StringLength(255)]
        public string serviceType { get; set; }
    }
}
