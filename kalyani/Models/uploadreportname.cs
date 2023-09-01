namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uploadreportnames")]
    public partial class uploadreportname
    {
        public int id { get; set; }

        [StringLength(45)]
        public string ReportName { get; set; }

        [StringLength(45)]
        public string ReportFilterName { get; set; }

        [StringLength(45)]
        public string OEM { get; set; }

        [StringLength(45)]
        public string UploadType { get; set; }

        public int? ReportId { get; set; }
    }
}
