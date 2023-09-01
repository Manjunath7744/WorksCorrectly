namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userroles")]
    public partial class userrole
    {
        public long id { get; set; }

        public long users_id { get; set; }

        public long roles_id { get; set; }

        public virtual role role { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
