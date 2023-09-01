namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usersession")]
    public partial class usersession
    {
        public long id { get; set; }

        [StringLength(200)]
        public string ipAddress { get; set; }

        public DateTime? lastSeen { get; set; }

        public DateTime? loggedIn { get; set; }

        public DateTime? sessionDateandTime { get; set; }

        [StringLength(30)]
        public string userLogined { get; set; }
    }
}
