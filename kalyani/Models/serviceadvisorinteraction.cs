namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("serviceadvisorinteraction")]
    public partial class serviceadvisorinteraction
    {
        public long id { get; set; }

        [StringLength(170)]
        public string filePath { get; set; }

        [StringLength(30)]
        public string firebaseKey { get; set; }

        [StringLength(30)]
        public string interactionDate { get; set; }

        public DateTime? interactionDateAndTime { get; set; }

        [StringLength(30)]
        public string interactionTime { get; set; }

        [StringLength(30)]
        public string latitude { get; set; }

        [StringLength(30)]
        public string longitude { get; set; }

        [StringLength(255)]
        public string mediaFile { get; set; }

        public byte[] mediaFileLob { get; set; }

        [StringLength(30)]
        public string typeOfInteraction { get; set; }

        public long? customer_id { get; set; }

        public long? psfAssignedInteraction_id { get; set; }

        public long? serviceBooked_serviceBookedId { get; set; }

        public long? status_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        public virtual customer customer { get; set; }

        public virtual psfassignedinteraction psfassignedinteraction { get; set; }

        public virtual serviceadvisorinteractionstatu serviceadvisorinteractionstatu { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual servicebooked servicebooked { get; set; }

        public virtual vehicle vehicle { get; set; }
    }
}
