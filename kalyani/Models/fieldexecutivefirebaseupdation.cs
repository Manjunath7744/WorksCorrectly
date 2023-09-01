namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fieldexecutivefirebaseupdation")]
    public partial class fieldexecutivefirebaseupdation
    {
        public long id { get; set; }

        public long? appointmentbookedid { get; set; }

        public long? insassignedid { get; set; }

        [StringLength(500)]
        public string firebasekey { get; set; }

        public long? tobepushed { get; set; }

        public long? upload_id { get; set; }

        public DateTime? updatedatetime { get; set; }

        public long? vehicle_id { get; set; }

        public long agentId { get; set; }

        public string lastfirebase_key { get; set; }

        public long inspolicydrop_id { get; set; }

        public bool isCancelled { get; set; }
    }
}
