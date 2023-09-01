namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workshop")]
    public partial class workshop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public workshop()
        {
            bays = new HashSet<bay>();
            campaigns = new HashSet<campaign>();
            complaintinteractions = new HashSet<complaintinteraction>();
            drivers = new HashSet<driver>();
            services = new HashSet<service>();
            serviceadvisors = new HashSet<serviceadvisor>();
            servicebookeds = new HashSet<servicebooked>();
            uploaddatas = new HashSet<uploaddata>();
            userworkshops = new HashSet<userworkshop>();
            workshopsummaries = new HashSet<workshopsummary>();
            wyzusers = new HashSet<wyzuser>();
        }

        public long id { get; set; }

        public int capacityPerDay { get; set; }

        [StringLength(30)]
        public string pincode { get; set; }

        [StringLength(50)]
        public string workshopAddress { get; set; }

        [StringLength(30)]
        public string workshopCode { get; set; }

        [StringLength(30)]
        public string workshopName { get; set; }
        public string workshopPhone { get; set; }

        public long? campaign_id { get; set; }

        public long? location_cityId { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? issales { get; set; }

        [Column(TypeName = "bit")]
        public bool? isinsurance { get; set; }

        //Added for Emails in Service by Nisarga
        public string escalationMails { get; set; }
        public string escalationPhone { get; set; }
        public string escalationCC { get; set; }
        //Added for Emails in Insurance
        public string insEscalationMails { get; set; }
        public string insEscalationCC { get; set; }
        public int districtid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bay> bays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<campaign> campaigns { get; set; }

        public virtual campaign campaign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintinteraction> complaintinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driver> drivers { get; set; }

        public virtual location location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<service> services { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisor> serviceadvisors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<uploaddata> uploaddatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userworkshop> userworkshops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<workshopsummary> workshopsummaries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wyzuser> wyzusers { get; set; }
    }
}
