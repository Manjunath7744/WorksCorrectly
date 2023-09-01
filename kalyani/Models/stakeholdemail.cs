namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stakeholdemail")]
    public partial class stakeholdemail
    {
        public long id { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [Column(TypeName = "bit")]
        public bool? active_status { get; set; }

        [StringLength(100)]
        public string attachmentName { get; set; }

        [StringLength(100)]
        public string ToEmail_id { get; set; }

        [StringLength(500)]
        public string Cc { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        [StringLength(100)]
        public string EmailTime { get; set; }

        public long? Cremanager_id { get; set; }

        public long? module_id { get; set; }
    }
}
