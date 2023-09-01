namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("axisdetails")]
    public partial class axisdetail
    {
        [Key]
        public long axisId { get; set; }

        [StringLength(255)]
        public string aggreagation { get; set; }

        [StringLength(255)]
        public string chartName { get; set; }

        [StringLength(255)]
        public string chartType { get; set; }

        public long level { get; set; }

        [Column(TypeName = "bit")]
        public bool? sortAsc { get; set; }

        [StringLength(255)]
        public string xaxisFieldName { get; set; }

        [StringLength(255)]
        public string xaxisLabelName { get; set; }

        [StringLength(255)]
        public string yaxisLabelName { get; set; }

        [StringLength(255)]
        public string yaxisName { get; set; }

        public long dashboardId { get; set; }

        public virtual dynamicdashformat dynamicdashformat { get; set; }
    }
}
