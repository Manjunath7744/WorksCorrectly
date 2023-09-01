namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("postupdate")]
    public partial class postupdate
    {
        public long id { get; set; }

        [StringLength(255)]
        public string contentType { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        [StringLength(255)]
        public string testFormName { get; set; }

        [StringLength(1073741823)]
        public string updateContent { get; set; }

        [StringLength(255)]
        public string updateFilePath { get; set; }

        [StringLength(255)]
        public string updateSubType { get; set; }

        [StringLength(255)]
        public string updateTitle { get; set; }

        [StringLength(255)]
        public string updateType { get; set; }

        [StringLength(255)]
        public string viewUpdateBy { get; set; }
    }
}
