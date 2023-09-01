namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insurancecompanies")]
    public partial class insurancecompany
    {
        public long id { get; set; }

        [StringLength(255)]
        public string companyName { get; set; }
    }
}
