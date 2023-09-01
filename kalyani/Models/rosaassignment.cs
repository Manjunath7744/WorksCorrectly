using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("rosaassignment")]

    public class rosaassignment
    {
   public long     id{get;set;}
    public DateTime? assigneddatetime { get; set; }
    public long repairorderdetails_id { get; set; }
    public long upload_id { get; set; }
    public long wyzuser_id { get; set; }
    public long customer_id { get; set; }
    public long vehicle_id { get; set; }
    public string vehiclereg_no { get; set; }
    public DateTime? rodate { get; set; }
    public string servicetype { get; set; }
    public DateTime? promisedeliverytime { get; set; }
    public string customername { get; set; }
    public string chassisno { get; set; }
    public string rostatus { get; set; }
    public DateTime? saledate { get; set; }
    public string ronumber { get; set; }
    public string technician { get; set; }
    public string visittypes { get; set; }
    public long? mileage { get; set; }
    public string serviceadvisor_name { get; set; }
    public long? workshop_id { get; set; }
    public string jobcardlocation { get; set; }
    public DateTime? rostatusupdatedate { get; set; }
    //public bool ismanuallyCreated { get; set; }
    }
}