using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("psfservicetypes")]
    public class psfservicetypes
    {
        public long id { get; set; }

        public int serviceId { get; set; }

        [StringLength(30)]
        public string serviceTypeName { get; set; }

        public bool isActive { get; set; }
    }
}