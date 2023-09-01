namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("agentdatablob")]
    public partial class agentdatablob
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(255)]
        public string agentId { get; set; }

        [StringLength(255)]
        public string columnName { get; set; }

        [StringLength(255)]
        public string createdBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdOn { get; set; }

        [Column(TypeName = "tinyblob")]
        public byte[] data { get; set; }

        [Column(TypeName = "bit")]
        public bool modificationStatus { get; set; }

        [StringLength(255)]
        public string modifiedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modifiedOn { get; set; }
    }
}
