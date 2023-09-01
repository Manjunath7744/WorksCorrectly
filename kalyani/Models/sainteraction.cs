using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("sainteraction")]
    public class sainteraction
    {
        public int id { get; set; }
        public long roassignid { get; set; }
        public long wyzuser_id { get; set; }
        public DateTime calldate { get; set; }
        public string calltime { get; set; }
        public DateTime? callmadeon { get; set; }
        public string saname { get; set; }
        public long vehicle_id { get; set; }
        public long customer_id { get; set; }
        public string reasonforpendingro { get; set; }
        public string partname { get; set; }
        public string partnumber { get; set; }
        public string pendingreson_remarks { get; set; }
        public DateTime? requestdate { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? etd { get; set; }
        public string partname5 { get; set; }
        public string partnumber5 { get; set; }
        public DateTime? requestdate5 { get; set; }
        public DateTime? orderdate5 { get; set; }
        public DateTime? etd5 { get; set; }
        public string partname2 { get; set; }
        public string partnumber2 { get; set; }
        public DateTime? requestdate2 { get; set; }
        public DateTime? orderdate2 { get; set; }
        public DateTime? etd2 { get; set; }
        public string partname3 { get; set; }
        public string partnumber3 { get; set; }
        public DateTime? requestdate3 { get; set; }
        public DateTime? orderdate3 { get; set; }
        public DateTime? etd3 { get; set; }
        public string partname4 { get; set; }
        public string partnumber4 { get; set; }
        public DateTime? requestdate4 { get; set; }
        public DateTime? orderdate4 { get; set; }
        public DateTime? etd4 { get; set; }
        public long? totalParts { get; set; }
    }
}