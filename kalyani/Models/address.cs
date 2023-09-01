namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("address")]
    public partial class address
    {
        public long id { get; set; }

        [StringLength(1073741823)]
        public string addressLine1 { get; set; }

        [StringLength(1073741823)]
        public string addressLine2 { get; set; }

        [StringLength(1073741823)]
        public string addressLine3 { get; set; }

        public int addressType { get; set; }

        [StringLength(30)]
        public string city { get; set; }

        [AllowHtml]
        public string concatenatedAdress { get; set; }

        [StringLength(30)]
        public string country { get; set; }

        [Column(TypeName = "bit")]
        public bool isPreferred { get; set; }

        public long pincode { get; set; }

        [StringLength(30)]
        public string state { get; set; }

        public long? customer_Id { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [StringLength(100)]
        public string district { get; set; }

        [StringLength(100)]
        public string tehsil { get; set; }

        public string wyzUserName { get; set; }

        public DateTime? updatedDateTime { get; set; }

        public virtual customer customer { get; set; }
        
        [NotMapped]
        public string ddlId { get; set; }
    }
}
