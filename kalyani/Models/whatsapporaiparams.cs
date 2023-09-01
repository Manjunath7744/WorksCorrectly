using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("whatsapporaiparams")]
    public class whatsapporaiparams
    {
       public int id { get; set; }
       public int parameterid { get; set; }
       public string parametername { get; set; }
       public string originalparam { get; set; }
         
    }
}