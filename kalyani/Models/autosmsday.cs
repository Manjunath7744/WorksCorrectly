namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("autosmsday")]
    public partial class autosmsday
    {
        [Column(TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public decimal? numberOfDays { get; set; }
    }
}
