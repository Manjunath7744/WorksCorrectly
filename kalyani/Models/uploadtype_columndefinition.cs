namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uploadtype_columndefinition")]
    public partial class uploadtype_columndefinition
    {
        public long id { get; set; }

        public long UploadType_id { get; set; }

        public long definitions_id { get; set; }

        public virtual columndefinition columndefinition { get; set; }
    }
}
