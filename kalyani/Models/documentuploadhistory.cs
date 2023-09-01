namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("documentuploadhistory")]
    public partial class documentuploadhistory
    {
        public long id { get; set; }

        [StringLength(100)]
        public string deptName { get; set; }

        [StringLength(100)]
        public string documentName { get; set; }

        [StringLength(500)]
        public string filePath { get; set; }

        public DateTime? uploadDateTime { get; set; }

        [StringLength(100)]
        public string user { get; set; }

        public long userId { get; set; }

        public long customerId { get; set; }

        [StringLength(255)]
        public string uploadFileName { get; set; }

        [NotMapped]
        public HttpPostedFileBase[]  fileList { get; set; }

        public long? insCallInterId { get; set; }
    }
}
