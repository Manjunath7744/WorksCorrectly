namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("addoncovers")]
    public partial class addoncover
    {
        public long id { get; set; }

        [StringLength(255)]
        public string coverName { get; set; }
    }
}
