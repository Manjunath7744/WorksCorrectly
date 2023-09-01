using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("formmapping")]
    public class formmapping
    {
        [Key]
        public int id { get; set; }

        [StringLength(300)]
        public string form_name { get; set; }

        [StringLength(400)]
        public string href_link { get; set; }

        [StringLength(50)]
        public string font_icon { get; set; }

        public int mainform_id { get; set; }

        [StringLength(50)]
        public string user_role { get; set; }

        [StringLength(30)]
        public string module_type { get; set; }

        public int displaying_order { get; set; }

        [Column(TypeName = "bit")]
        public bool isactive { get; set; }

        public long? updatedby { get; set; }

        public DateTime? updatedon { get; set; }
    }
}