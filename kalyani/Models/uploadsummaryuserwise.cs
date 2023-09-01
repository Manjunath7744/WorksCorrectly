namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uploadsummaryuserwise")]
    public partial class uploadsummaryuserwise
    {
        public long id { get; set; }

        public long? wyzuser_id { get; set; }

        public long? totalAssignedCalls { get; set; }

        public long? campaign_id { get; set; }

        public long? location_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? isAutoAssigned { get; set; }

        public long? module { get; set; }

        public long? uploadid { get; set; }
    }
}
