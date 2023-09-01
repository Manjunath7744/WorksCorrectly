namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userworkshop")]
    public partial class userworkshop
    {
        public long id { get; set; }

        public long userWorkshop_id { get; set; }

        public long workshopList_id { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual workshop workshop { get; set; }
    }
}
