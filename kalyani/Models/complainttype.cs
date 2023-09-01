namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("complainttypes")]
    public partial class complainttype
    {
        public long id { get; set; }

        [StringLength(50)]
        public string departmentName { get; set; }

        [StringLength(50)]
        public string taggedUserName { get; set; }

        [StringLength(50)]
        public string taggedUserNumber { get; set; }

        public long moduleTypeId { get; set; }
    }
}
