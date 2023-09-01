namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("driverinteraction")]
    public partial class driverinteraction
    {
        public long id { get; set; }

        public int customerCRN { get; set; }

        [StringLength(30)]
        public string firebaseKey { get; set; }

        [StringLength(30)]
        public string interactionDate { get; set; }

        public DateTime? interactionDateAndTime { get; set; }

        [StringLength(30)]
        public string interactionTime { get; set; }

        [Column(TypeName = "bit")]
        public bool isCall { get; set; }

        [StringLength(255)]
        public string mediaFile { get; set; }

        public byte[] mediaFileLob { get; set; }

        [StringLength(10)]
        public string pickupOrDrop { get; set; }

        public long? customer_id { get; set; }

        public long? status_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        public virtual customer customer { get; set; }

        public virtual driverinteractionstatu driverinteractionstatu { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual vehicle vehicle { get; set; }
    }
}
