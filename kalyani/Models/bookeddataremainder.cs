namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bookeddataremainder")]
    public partial class bookeddataremainder
    {
        public long id { get; set; }

        public long? chasserId { get; set; }

        public long? creId { get; set; }

        public long? locationId { get; set; }
    }
}
