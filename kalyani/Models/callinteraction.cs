namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("callinteraction")]
    public partial class callinteraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public callinteraction()
        {
            psfinteractions = new HashSet<psfinteraction>();
            complaints = new HashSet<complaint>();
            srdispositions = new HashSet<srdisposition>();
            insurancedispositions = new HashSet<insurancedisposition>();
            induspsfinteractions = new HashSet<IndusPSFInteraction>();
            servicebookeds = new HashSet<servicebooked>();
        }

        public long id { get; set; }

        [StringLength(30)]
        public string agentName { get; set; }

        public long callCount { get; set; }

        [StringLength(30)]
        public string callDate { get; set; }

        [StringLength(10)]
        public string callDuration { get; set; }

        public DateTime? callMadeDateAndTime { get; set; }

        [StringLength(30)]
        public string callTime { get; set; }

        [StringLength(20)]
        public string callType { get; set; }

        public int callTypePicId { get; set; }

        [Column(TypeName = "bit")]
        public bool dailedStatus { get; set; }

        [StringLength(30)]
        public string dealerCode { get; set; }

        public long droppedCount { get; set; }

        [StringLength(500)]
        public string filePath { get; set; }

        [StringLength(100)]
        public string firebaseKey { get; set; }

        [StringLength(50)]
        public string isCallinitaited { get; set; }

        [Column(TypeName = "bit")]
        public bool isComplaintCall { get; set; }

        [Column(TypeName = "bit")]
        public bool isDriverCall { get; set; }

        [StringLength(30)]
        public string latitude { get; set; }

        [StringLength(30)]
        public string longitude { get; set; }

        [StringLength(30)]
        public string makeCallFrom { get; set; }

        [StringLength(255)]
        public string mediaFile { get; set; }

        public byte[] mediaFileLob { get; set; }

        [StringLength(50)]
        public string serviceAdvisorfirebaseKey { get; set; }

        [StringLength(10)]
        public string synchedToFirebase { get; set; }

        public int uniqueidForCallSync { get; set; }

        public long? assignedInteraction_id { get; set; }

        public long? customer_id { get; set; }

        public long? psfAssignedInteraction_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        public long? insuranceAssignedInteraction_id { get; set; }

        [StringLength(100)]
        public string dailedNoIs { get; set; }

        [StringLength(10)]
        public string ringTime { get; set; }

        public long? campaign_id { get; set; }

        public long? serviceBooked_serviceBookedId { get; set; }

        public long? appointmentBooked_appointmentId { get; set; }

        [Column(TypeName = "bit")]
        public bool? chasserCall { get; set; }

        [Column(TypeName = "bit")]
        public bool? convertion { get; set; }

        [StringLength(100)]
        public string ronumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? rodate { get; set; }

        [StringLength(45)]
        public string uniqueAndroidId { get; set; }

        [StringLength(250)]
        public string fileSize { get; set; }

        public long dissatPSFintId { get; set; }

        [StringLength(50)]
        public string uniqueIdGSM { get; set; }

        public long tagging_id { get; set; }

        [StringLength(255)]
        public string tagging_name { get; set; }

        public long idmap { get; set; }

        //For Indus
        public long service_id { get; set; }

        //For FE app
        [Column(TypeName ="bit")]
        public bool insDocuUploaded { get; set; }

        public string kmTravelled { get; set; }

        public string startLocation { get; set; }

        public string stopLocation { get; set; }

        [Column(TypeName = "bit")]
        public bool isComplaint { get; set; }

        [Column(TypeName = "bit")]
        public bool callStatus { get; set; }

        public bool isResolved { get; set; }

        public virtual appointmentbooked appointmentbooked { get; set; }

        public virtual assignedinteraction assignedinteraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfinteraction> psfinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndusPSFInteraction> induspsfinteractions { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaint> complaints { get; set; }

        public virtual insuranceassignedinteraction insuranceassignedinteraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<srdisposition> srdispositions { get; set; }

        public virtual campaign campaign { get; set; }

        public virtual customer customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insurancedisposition> insurancedispositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        public virtual psfassignedinteraction psfassignedinteraction { get; set; }

        public virtual servicebooked servicebooked { get; set; }

        public virtual vehicle vehicle { get; set; }
    }
}
