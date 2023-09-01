using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AutoSherpa_project.Models
{
    [Table("navigationaccess")]
    public class navigationaccess
    {
        [Key]
        public long id { get; set; }
        public long  wyzuser_id { get; set; }

        [StringLength(30)]
        public string role { get; set; }
        
        [StringLength(300)]
        public string form_id { get; set; }
        
        [Column(TypeName = "bit")] 
        public bool isactive { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

    }
}