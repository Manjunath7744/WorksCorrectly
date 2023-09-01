namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("imagestorage")]
    public partial class imagestorage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public imagestorage()
        {
            casesummaries = new HashSet<casesummary>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string image { get; set; }

        public byte[] image1_lob { get; set; }

        public byte[] image2_lob { get; set; }

        public byte[] image3_lob { get; set; }

        public byte[] image4_lob { get; set; }

        public byte[] image5_lob { get; set; }

        [StringLength(255)]
        public string imageId { get; set; }

        [StringLength(255)]
        public string latitude { get; set; }

        [StringLength(255)]
        public string latitude1 { get; set; }

        [StringLength(255)]
        public string latitude2 { get; set; }

        [StringLength(255)]
        public string latitude3 { get; set; }

        [StringLength(255)]
        public string latitude4 { get; set; }

        [StringLength(255)]
        public string latitude5 { get; set; }

        [StringLength(255)]
        public string longitude { get; set; }

        [StringLength(255)]
        public string longitude1 { get; set; }

        [StringLength(255)]
        public string longitude2 { get; set; }

        [StringLength(255)]
        public string longitude3 { get; set; }

        [StringLength(255)]
        public string longitude4 { get; set; }

        [StringLength(255)]
        public string longitude5 { get; set; }

        [StringLength(255)]
        public string mode { get; set; }

        public long serialNoOfPendingCase { get; set; }

        [StringLength(255)]
        public string signature { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<casesummary> casesummaries { get; set; }
    }
}
