namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("phonenodeletion")]
    public partial class phonenodeletion
    {
        public long id { get; set; }

        public long customerId { get; set; }

        public DateTime? deletedDate { get; set; }

        [StringLength(255)]
        public string phoneNumber { get; set; }

        public long userId { get; set; }

        [StringLength(500)]
        public string remarks { get; set; }
    }
}
