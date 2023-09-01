namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("agent_roaster")]
    public partial class agent_roaster
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        [StringLength(255)]
        public string agent_name { get; set; }

        [StringLength(255)]
        public string assistant_manager { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        [StringLength(255)]
        public string team_leader { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updated_date { get; set; }
    }
}
