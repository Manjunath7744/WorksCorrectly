using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("searchandassignaccess")]
    public class searchandassignaccess
    {
        public long id { get; set; }
        public string role { get; set; }
        public bool givenAccess { get; set; }
        public long? updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }
    }
}