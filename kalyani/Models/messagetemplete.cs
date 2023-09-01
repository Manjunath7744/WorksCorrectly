namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("messagetemplete")]
    public partial class messagetemplete
    {
        public long id { get; set; }

        [StringLength(255)]
        public string dealerCode { get; set; }

        [StringLength(255)]
        public string messageBody { get; set; }

        [StringLength(255)]
        public string messageHeader { get; set; }

        [StringLength(255)]
        public string messageType { get; set; }
    }
}
