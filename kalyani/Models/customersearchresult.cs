namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customersearchresult")]
    public partial class customersearchresult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customersearchresult()
        {
            savedsearchresults = new HashSet<savedsearchresult>();
        }

        public long id { get; set; }

        [StringLength(30)]
        public string cid { get; set; }

        [StringLength(50)]
        public string customerCategory { get; set; }

        [StringLength(200)]
        public string customerName { get; set; }

        [StringLength(50)]
        public string lastdisposition { get; set; }

        [StringLength(50)]
        public string model { get; set; }

        [StringLength(50)]
        public string nextServicedate { get; set; }

        [StringLength(50)]
        public string phoneNumber { get; set; }

        [StringLength(50)]
        public string variant { get; set; }

        [StringLength(50)]
        public string vehicleRegNo { get; set; }

        [StringLength(30)]
        public string vehicle_id { get; set; }

        [StringLength(45)]
        public string upload_id { get; set; }

        [StringLength(45)]
        public string chassisno { get; set; }

        public long ins_assignId { get; set; }

        [StringLength(255)]
        public string ins_assignedcre { get; set; }

        public long insu_finaldispo { get; set; }

        [StringLength(255)]
        public string policyDueDate { get; set; }

        public long serviceId { get; set; }

        public long smr_assignId { get; set; }

        [StringLength(255)]
        public string smr_assignedcre { get; set; }

        public long smr_finaldispo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<savedsearchresult> savedsearchresults { get; set; }
    }
}
