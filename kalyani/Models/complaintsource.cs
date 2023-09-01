namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("complaintsource")]
    public partial class complaintsource
    {
        public long id { get; set; }

        [Column("complaintSource")]
        [StringLength(100)]
        public string complaintSource1 { get; set; }
    }
}
