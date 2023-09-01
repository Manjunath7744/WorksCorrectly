using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("businesswhatsappdetails")]
    public class businesswhatsappdetails
    {
        [Key]
       public int id { get; set; }
       public string baseurl { get; set; }
       public string username { get; set; }
       public string password { get; set; }
       public string bearertoken { get; set; }
       public DateTime? expiry { get; set; }
       public bool isactive { get; set; }
       public string fromnumber { get; set; }
        

    }
}