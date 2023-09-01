namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("logininformation")]
    public partial class logininformation
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        [StringLength(255)]
        public string ip_address { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime login_time { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime logout_time { get; set; }

        [StringLength(255)]
        public string user_role { get; set; }

        [StringLength(255)]
        public string username { get; set; }
    }
}
