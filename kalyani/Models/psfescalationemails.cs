using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("psfescalationemails")]
    public class psfescalationemails
    {
        public long id { get; set; }
        public long workshopid_fk { get; set; }
        public string toemail { get; set; }
        public string ccemail { get; set; }

    }
}