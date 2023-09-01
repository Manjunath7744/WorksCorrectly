namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customer")]
    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            addresses = new HashSet<address>();
            appointmentbookeds = new HashSet<appointmentbooked>();
            assignedinteractions = new HashSet<assignedinteraction>();
            callinteractions = new HashSet<callinteraction>();
            complaints = new HashSet<complaint>();
            complaintinteractions = new HashSet<complaintinteraction>();
            segments = new HashSet<segment>();
            servicebookeds = new HashSet<servicebooked>();
            driverinteractions = new HashSet<driverinteraction>();
            insurances = new HashSet<insurance>();
            insuranceassignedinteractions = new HashSet<insuranceassignedinteraction>();
            serviceadvisorinteractions = new HashSet<serviceadvisorinteraction>();
            vehicles = new HashSet<vehicle>();
            psfassignedinteractions = new HashSet<psfassignedinteraction>();
            phones = new HashSet<phone>();
            emails = new HashSet<email>();
            smsinteractions = new HashSet<smsinteraction>();
        }

        public long id { get; set; }

        [StringLength(30)]
        public string anniversary_date { get; set; }

        [StringLength(30)]
        public string createdBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdDate { get; set; }

        [StringLength(30)]
        public string customerCategory { get; set; }

        [StringLength(30)]
        public string customerEmail { get; set; }

        [StringLength(30)]
        public string customerFName { get; set; }

        [StringLength(255)]
        public string customerId { get; set; }

        [StringLength(30)]
        public string customerLName { get; set; }

        [StringLength(255)]
        public string customerName { get; set; }

        [Column(TypeName = "bit")]
        public bool doNotDisturb { get; set; }

        [StringLength(30)]
        public string dob { get; set; }

        [StringLength(30)]
        public string mode_of_contact { get; set; }

        [StringLength(30)]
        public string preferred_day { get; set; }

        [StringLength(30)]
        public string preferred_time_end { get; set; }

        [StringLength(30)]
        public string preferred_time_start { get; set; }

        [StringLength(255)]
        public string salutation { get; set; }

        [StringLength(255)]
        public string sourceType { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [StringLength(1073741823)]
        public string comments { get; set; }

        [StringLength(100)]
        public string userDriver { get; set; }

        [StringLength(45)]
        public string dbvehregno { get; set; }

        [StringLength(45)]
        public string regnoSMR { get; set; }

        [StringLength(105)]
        public string preff_workshop { get; set; }

        [StringLength(100)]
        public string regno_SMR { get; set; }
        public string leadtag { get; set; }

        public string pancardno { get; set; }
        public string ckycno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<address> addresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignedinteraction> assignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaint> complaints { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintinteraction> complaintinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<segment> segments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverinteraction> driverinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insurance> insurances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insuranceassignedinteraction> insuranceassignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisorinteraction> serviceadvisorinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vehicle> vehicles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfassignedinteraction> psfassignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<phone> phones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<email> emails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<smsinteraction> smsinteractions { get; set; }
    }
}
