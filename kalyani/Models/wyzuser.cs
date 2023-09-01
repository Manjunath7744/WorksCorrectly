using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AutoSherpa_project.Models
{
    [Table("wyzuser")]
    public partial class wyzuser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public wyzuser()
        {
            appointmentbookeds = new HashSet<appointmentbooked>();
            assignedinteractions = new HashSet<assignedinteraction>();
            availabilities = new HashSet<availability>();
            callinteractions = new HashSet<callinteraction>();
            caselists = new HashSet<caselist>();
            complaints = new HashSet<complaint>();
            complaints1 = new HashSet<complaint>();
            complaintassignedinteractions = new HashSet<complaintassignedinteraction>();
            complaintinteractions = new HashSet<complaintinteraction>();
            drivers = new HashSet<driver>();
            driverinteractions = new HashSet<driverinteraction>();
            insuranceagents = new HashSet<insuranceagent>();
            insuranceassignedinteractions = new HashSet<insuranceassignedinteraction>();
            psfassignedinteractions = new HashSet<psfassignedinteraction>();
            serviceadvisors = new HashSet<serviceadvisor>();
            serviceadvisorinteractions = new HashSet<serviceadvisorinteraction>();
            servicebookeds = new HashSet<servicebooked>();
            servicebookeds1 = new HashSet<servicebooked>();
            smsinteractions = new HashSet<smsinteraction>();
            taggingusers = new HashSet<tagginguser>();
            taggingusers1 = new HashSet<tagginguser>();
            unavailabilities = new HashSet<unavailability>();
            uploads = new HashSet<upload>();
            uploaddatas = new HashSet<uploaddata>();
            userlocations = new HashSet<userlocation>();
            userroles = new HashSet<userrole>();
            userworkshops = new HashSet<userworkshop>();
            fieldwalkinlocations = new HashSet<fieldwalkinlocation>();
        }

        public long id { get; set; }

        //[StringLength(30)]
        public string creManager { get; set; }

        //[StringLength(30)]
        public string dealerCode { get; set; }

        //[StringLength(30)]
        public string dealerId { get; set; }

        //[StringLength(30)]
        public string dealerName { get; set; }

        //[StringLength(30)]
        public string emailId { get; set; }

        //[StringLength(30)]
        public string firstName { get; set; }

        [Column(TypeName = "bit")]
        public bool insuranceRole { get; set; }

        //[StringLength(30)]
        public string lastName { get; set; }

        //[StringLength(30)]
        public string password { get; set; }

        //[StringLength(30)]
        public string phoneIMEINo { get; set; }

        //[StringLength(30)]
        public string phoneNumber { get; set; }

        //[StringLength(500)]
        public string registrationId { get; set; }

        //[StringLength(30)]
        public string role { get; set; }

        //[StringLength(30)]
        public string role1 { get; set; }

        //[StringLength(30)]
        public string userName { get; set; }

        [Column(TypeName = "bit")]
        public bool unAvailable { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updatedDate { get; set; }

        public long allocatedCount { get; set; }

        public long? location_cityId { get; set; }

        public long? workshop_id { get; set; }

        public long? tenant_id { get; set; }

        public long? dealer_id { get; set; }

        //[StringLength(30)]
        public string extensionId { get; set; }

        //[StringLength(45)]
        public string appVersion { get; set; }

        //[StringLength(100)]
        public string osversion { get; set; }

        //[StringLength(100)]
        public string phoneModel { get; set; }

        public string gsmRegistrationId { get; set; }
        public string gsmPhonenumber { get; set; }
        public long? Rm { get; set; }
        public long? Wm { get; set; }
        public string ipAddress { get; set; }
        public bool searchAssignAccess { get; set; }
        public long? updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }
        public string knowlarityCaller_id { get; set; }
        [NotMapped]
        public string workshopCode { get; set; }
        [NotMapped]
        public string moduletype { get; set; }
        public bool uniquesupcre { get; set; }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignedinteraction> assignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<availability> availabilities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caselist> caselists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaint> complaints { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaint> complaints1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintassignedinteraction> complaintassignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintinteraction> complaintinteractions { get; set; }

        public virtual dealer dealer { get; set; }

        public virtual dealer dealer1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driver> drivers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverinteraction> driverinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insuranceagent> insuranceagents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insuranceassignedinteraction> insuranceassignedinteractions { get; set; }

        public virtual location location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfassignedinteraction> psfassignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisor> serviceadvisors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisorinteraction> serviceadvisorinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<smsinteraction> smsinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tagginguser> taggingusers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tagginguser> taggingusers1 { get; set; }

        public virtual tenant tenant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<unavailability> unavailabilities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upload> uploads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<uploaddata> uploaddatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userlocation> userlocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userrole> userroles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userworkshop> userworkshops { get; set; }

        public virtual workshop workshop { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fieldwalkinlocation> fieldwalkinlocations { get; set; }
        public DateTime LatestPasswordUpdatedDate { get; set; }
    }
}
