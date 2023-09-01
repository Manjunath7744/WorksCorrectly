namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smsparameters")]
    public partial class smsparameter
    {
        public long id { get; set; }

        [StringLength(255)]
        public string message { get; set; }

        [StringLength(255)]
        public string phone { get; set; }

        [StringLength(255)]
        public string senderid { get; set; }

        [StringLength(20)]
        public string sucessStatus { get; set; }
    }
}
