namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dealer")]
    public partial class dealer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dealer()
        {
            wyzusers = new HashSet<wyzuser>();
            locations = new HashSet<location>();
            locations1 = new HashSet<location>();
            wyzusers1 = new HashSet<wyzuser>();
        }

        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool AndroidAuthentication { get; set; }

        [StringLength(255)]
        public string dealerAddress { get; set; }

        [StringLength(255)]
        public string dealerCode { get; set; }

        [StringLength(255)]
        public string dealerId { get; set; }

        [StringLength(255)]
        public string dealerName { get; set; }

        [StringLength(255)]
        public string noOfUsers { get; set; }

        [StringLength(255)]
        public string timeZoneOfDealer { get; set; }

        [Column(TypeName = "bit")]
        public bool webAndAndroidAuthen { get; set; }

        [Column(TypeName = "bit")]
        public bool webAthentication { get; set; }

        [Column(TypeName = "bit")]
        public bool webAuthWithCallInitiating { get; set; }

        [StringLength(255)]
        public string OEM { get; set; }

        [Column(TypeName = "bit")]
        public bool ipAuthorization { get; set; }

        [Column(TypeName = "bit")]
        public bool? userControl { get; set; }
        public bool? INSuserControl { get; set; }

        [Column(TypeName = "bit")]
        public bool? superControl { get; set; }
        public bool? INSsuperControl { get; set; }
        //public bool? SMRsuperControl { get; set; }

        [StringLength(45)]
        public string maindb { get; set; }

        public int followupdaylimit { get; set; }

        [StringLength(200)]
        public string opsguruUrl { get; set; }

        public int assignmentinterval { get; set; }
        public bool isfieldexecutive { get; set; }
        public bool ispolicydropexecutive { get; set; }

        //public string infoBip_baseurl { get; set; }

        //public string infoBip_key { get; set; }

        //public string infoBip_username { get; set; }

        //public string infoBip_password { get; set; }
        public string indusServiceUserName { get; set; }
        public string indusServicePassword { get; set; }
        public string indusBaseURL { get; set; }

        [Column(TypeName ="bit")]
        public bool PSFTriggerStatus { get; set; }
        public int bookingDayLimit { get; set; }
        public int insfollowupdaylimit { get; set; }

        public int insbookingDayLimit { get; set; }
        public bool insunassignedblock { get; set; }
        public bool smrunassignedblock { get; set; }
        public string authorizationkey { get; set; }
        public string logopath { get; set; }
        public bool isPsfDynamic { get; set; }
        public bool issmsenabled { get; set; }
        public bool isemailenabled { get; set; }
        


[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wyzuser> wyzusers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<location> locations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<location> locations1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wyzuser> wyzusers1 { get; set; }
    }
}
