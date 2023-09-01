namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userlocation")]
    public partial class userlocation
    {
        public long id { get; set; }

        public long userLocation_id { get; set; }

        public long locationList_cityId { get; set; }

        public virtual location location { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
