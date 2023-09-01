namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("processdefinition")]
    public partial class processdefinition
    {
        public long id { get; set; }

        [StringLength(255)]
        public string chart_name { get; set; }

        [StringLength(255)]
        public string chart_type { get; set; }

        [StringLength(255)]
        public string dashboard_name { get; set; }

        [StringLength(255)]
        public string process_name { get; set; }

        [StringLength(255)]
        public string table_name { get; set; }

        [StringLength(255)]
        public string x_axis { get; set; }

        [StringLength(255)]
        public string x_axis_fields { get; set; }

        [StringLength(255)]
        public string y_axis { get; set; }

        [StringLength(255)]
        public string y_axis_fields { get; set; }
    }
}
