using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("driverappfiledetails")]
    public class DriverAppFileDetails
    {
        [Key]
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string PictureType { get; set; }
        public long? Vehicle_Id { get; set; }
        public long? DriverScheduler_Id { get; set; }
        public int? PckUpDropType { get; set; }
    }
}