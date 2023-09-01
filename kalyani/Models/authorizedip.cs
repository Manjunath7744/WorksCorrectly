namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("authorizedip")]
    public partial class authorizedip
    {
        public long id { get; set; }

        [StringLength(255)]
        public string ip_address { get; set; }

        public long? dealerId { get; set; }
    }
}
