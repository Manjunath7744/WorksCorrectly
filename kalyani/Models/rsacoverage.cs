using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("rsacoverage")]

    public class rsacoverage
    {
        public long id { get; set; }

        public string coverage { get; set; }
    }
}