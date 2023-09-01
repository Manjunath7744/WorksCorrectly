namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("postupdatemapping")]
    public partial class postupdatemapping
    {
        public long id { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [Column(TypeName = "bit")]
        public bool isAvailable_download { get; set; }

        [Column(TypeName = "bit")]
        public bool isAvailable_test { get; set; }

        [Column(TypeName = "bit")]
        public bool isViewed_test { get; set; }

        [Column(TypeName = "bit")]
        public bool isViewed_update { get; set; }

        [StringLength(255)]
        public string postUpdateId { get; set; }

        [StringLength(255)]
        public string updateCreated_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updateCreated_on { get; set; }

        [StringLength(255)]
        public string user_id { get; set; }
    }
}
