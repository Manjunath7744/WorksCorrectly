namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fmanualuser")]
    public partial class fmanualuser
    {
        public long id { get; set; }

        public long upload_id { get; set; }

        public long wyzuser_id { get; set; }
    }
}
