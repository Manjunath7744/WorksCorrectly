using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("sipregistrationid")]
    public class sipregistrationid
    {
        public long id { get; set; }
        [StringLength(45)]
        public string sipextension{ get; set; }
        [StringLength(45)]
        public string gsmregistrationid{ get; set; }
        public long tenant_id{ get; set; }
    }
}