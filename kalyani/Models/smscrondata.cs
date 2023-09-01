using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("smscrondata")]
    public class smscrondata
    {
        public int id { get; set; }

        public string sms { get; set; }

        public string phonenumber { get; set; }

        public DateTime? date { get; set; }

        public string smsapi { get; set; }

        public int? sentstatus { get; set; }

        public DateTime? sentdatetime { get; set; }

        public DateTime? updated_date { get; set; }

        public DateTime? last_updated { get; set; }

        public string responseFromGateway { get; set; }

        public long? customer_id { get; set; }

        public long? vehicle_id { get; set; }

        public long? wyzuser_id { get; set; }

        public long? autoTemplateID { get; set; }

        public string dealername { get; set; }

        public string reasons { get; set; }
    }
}