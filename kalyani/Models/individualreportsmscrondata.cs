using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table ("individualreportsmscrondata")]
    public class individualreportsmscrondata
    {
        [Key]
        public int id { get; set; }
        //public string schema_name{ get; set; }
        public string smsid { get; set; }
    public string sms { get; set; }
        public string phonenumber { get; set; }
        //public long datetimeinput { get; set; }
        //public DateTime? date { get; set; }
        //public DateTime? from_time { get; set; }
        //public DateTime? to_time { get; set; }
        public string smsapi { get; set; }
        public bool sentstatus { get; set; }
        //public DateTime? sentdatetime { get; set; }
        public DateTime? updated_date { get; set; }
        //public DateTime? last_updated { get; set; }
        //public string responseFromGateway { get; set; }
        public long? customer_id { get; set; }
        public long? vehicle_id { get; set; }
        public long? wyzuser_id { get; set; }
        //public long? autoTemplateID { get; set; }
        //public string dealername { get; set; }
        public string reasons { get; set; }
        //public string policynumber { get; set; }
        //public string chassisno { get; set; }
        //public long insuranceid { get; set; }

    }
}