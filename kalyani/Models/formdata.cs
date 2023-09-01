namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("formdata")]
    public partial class formdata
    {
        public long id { get; set; }

        [StringLength(70)]
        public string name { get; set; }

        [StringLength(100)]
        public string value { get; set; }

        public long? upload_id { get; set; }

        public virtual upload upload { get; set; }
    }
}
