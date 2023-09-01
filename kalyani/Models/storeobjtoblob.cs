namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("storeobjtoblob")]
    public partial class storeobjtoblob
    {
        public long id { get; set; }

        [Column(TypeName = "tinyblob")]
        public byte[] modelData { get; set; }

        [StringLength(255)]
        public string modelName { get; set; }
    }
}
