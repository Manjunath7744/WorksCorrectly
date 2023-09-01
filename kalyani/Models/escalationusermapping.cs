namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kalyanimotors.escalationusermapping")]
    public partial class escalationusermapping
    {
        public long id { get; set; }

        [StringLength(255)]
        public string creIds { get; set; }

        public long escalationUserId { get; set; }

        public long locationId { get; set; }

        [StringLength(255)]
        public string moduleId { get; set; }

        public long workshopId { get; set; }
    }
}
