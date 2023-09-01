namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uniqueidforcall")]
    public partial class uniqueidforcall
    {
        public long id { get; set; }

        public int callinitiating_id { get; set; }
    }
}
