namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("invalidworkshopdata")]
    public partial class invalidworkshopdata
    {
        public int id { get; set; }

        [StringLength(100)]
        public string chassisNo { get; set; }

        [StringLength(100)]
        public string customerName { get; set; }

        [StringLength(100)]
        public string uploaded_Date { get; set; }

        [StringLength(45)]
        public string uploadid { get; set; }

        [StringLength(45)]
        public string workshopCode { get; set; }

        [StringLength(45)]
        public string smrId { get; set; }

        [StringLength(45)]
        public string moduletypeid { get; set; }
    }
}
