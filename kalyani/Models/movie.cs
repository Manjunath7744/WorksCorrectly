namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("movie")]
    public partial class movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(255)]
        public string CustomerMobile { get; set; }

        [StringLength(255)]
        public string adrress { get; set; }

        [StringLength(255)]
        public string customerName { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string number { get; set; }

        public long phoneNumber { get; set; }

        [StringLength(255)]
        public string teliphone { get; set; }
    }
}
