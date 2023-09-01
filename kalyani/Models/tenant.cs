namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tenant")]
    public partial class tenant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tenant()
        {
            wyzusers = new HashSet<wyzuser>();
        }

        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool isDeactivated { get; set; }

        [StringLength(40)]
        public string tenantCode { get; set; }

        [StringLength(30)]
        public string tenantPhone { get; set; }

        [StringLength(150)]
        public string tenentAddress { get; set; }

        public int totalUsers { get; set; }

        [StringLength(150)]
        public string apkURLPath { get; set; }

        [StringLength(150)]
        public string appVersion { get; set; }

        public string feAppVersion { get; set; }
        public string driverAppVersion { get; set; }

        [Column(TypeName = "bit")]
        public bool? insAutoCallAllocation { get; set; }

        [Column(TypeName = "bit")]
        public bool? smrAutoCallAllocation { get; set; }

        public int? NumberOFDaysSMR { get; set; }

        public int? NumberOFDaysINS { get; set; }

        public int? DataRemovalDays { get; set; }

        public int? DroppedCount { get; set; }

        [Column(TypeName = "bit")]
        public bool? smrAutoSMS { get; set; }

        [Column(TypeName = "bit")]
        public bool? insAutoSMS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastsaledate { get; set; }

        [StringLength(45)]
        public string gsmip { get; set; }
        //by nisarga
        [StringLength(100)]
        public string gsmRegistrationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wyzuser> wyzusers { get; set; }
       



    }
}
