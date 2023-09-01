namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("servicedoneschcall")]
    public partial class servicedoneschcall
    {
        public long id { get; set; }

        [StringLength(255)]
        public string agentId { get; set; }

        [StringLength(255)]
        public string agentName { get; set; }

        [StringLength(255)]
        public string customerName { get; set; }

        [StringLength(255)]
        public string customerPhone { get; set; }

        [StringLength(255)]
        public string dealerCode { get; set; }

        [StringLength(255)]
        public string model { get; set; }

        [Column(TypeName = "date")]
        public DateTime? serviceBookeduplodedCurrentDate { get; set; }

        [StringLength(255)]
        public string syncedToFireBaseServiceBooked { get; set; }

        [StringLength(255)]
        public string vehicalRegNo { get; set; }

        [StringLength(255)]
        public string vehicleNumber { get; set; }
    }
}
