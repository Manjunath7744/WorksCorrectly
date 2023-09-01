namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dealerpanel")]
    public partial class dealerpanel
    {
        public long id { get; set; }

        [StringLength(255)]
        public string region { get; set; }

        [StringLength(255)]
        public string workshopCode { get; set; }

        [StringLength(255)]
        public string dealerName { get; set; }

        [StringLength(255)]
        public string workshopType { get; set; }

        [StringLength(255)]
        public string jDP_Non_JDP { get; set; }

        [StringLength(105)]
        public string workshopGroup { get; set; }

        [StringLength(255)]
        public string location { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string city { get; set; }

        [StringLength(255)]
        public string state { get; set; }

        [StringLength(255)]
        public string pincode { get; set; }
    }
}
