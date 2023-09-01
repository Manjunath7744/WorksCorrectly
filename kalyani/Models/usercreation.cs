namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usercreation")]
    public partial class usercreation
    {
        [Key]
        public long uid { get; set; }

        [StringLength(255)]
        public string udealerName { get; set; }

        [StringLength(255)]
        public string uemail { get; set; }

        [StringLength(255)]
        public string ufirstName { get; set; }

        [StringLength(255)]
        public string ulastName { get; set; }

        [StringLength(255)]
        public string upassword { get; set; }

        public long uphNo { get; set; }

        public long uphoneEMIno { get; set; }

        [StringLength(255)]
        public string urole { get; set; }
    }
}
