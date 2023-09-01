using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("postsalescomplaintcre")]
    public class postsalescomplaintcre
    {
        [Key]
        public int id { get; set; }
        public long cre_id { get; set; }
        public long Complaintcre_id { get; set; }
        public long workshop_id { get; set; }
    }
}