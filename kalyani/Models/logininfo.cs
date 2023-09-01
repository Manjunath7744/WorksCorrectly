namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("logininfo")]
    public partial class logininfo
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        [StringLength(255)]
        public string ip_address { get; set; }

        public DateTime? login_time { get; set; }

        public DateTime? logout_time { get; set; }

        [StringLength(255)]
        public string oem { get; set; }

        [StringLength(255)]
        public string tenantCode { get; set; }

        [StringLength(255)]
        public string tenantName { get; set; }

        public long userId { get; set; }

        [StringLength(255)]
        public string user_role { get; set; }

        [StringLength(255)]
        public string username { get; set; }
    }
}
