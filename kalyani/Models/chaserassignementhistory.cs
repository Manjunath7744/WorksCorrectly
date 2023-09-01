namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chaserassignementhistory")]
    public partial class chaserassignementhistory
    {
        public long id { get; set; }

        public DateTime? assignedDate { get; set; }

        public long assignedId { get; set; }

        public long chasserId { get; set; }

        public long oldCREId { get; set; }
    }
}
