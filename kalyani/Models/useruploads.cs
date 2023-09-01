using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("useruploads")]
    public class useruploads
    {
        [Key]

        public int id { get; set; }
        public string fileName { get; set; }
        public string rawfilePath { get; set; }
        public string uploadedPath { get; set; }
        public string discardedPath { get; set; }
        public string nomanagerpath { get; set; }
        public string duplicateCREPath { get; set; }
        public string duplicateExtensionCREPath { get; set; }
        public string duplicatephoneNumber { get; set; }
        public string duplicateUser { get; set; }
        public int totalUploaded { get; set; }
        public int totalDiscarded { get; set; }
        public int totalRecords { get; set; }
        public DateTime? uploadedDate { get; set; }
        public string uploadedTime { get; set; }
    }
}