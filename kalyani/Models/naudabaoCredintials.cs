using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("naudabaocredintials")]
    public class naudabaoCredintials
    {
       [Key]

       public int id { get; set; } 
       public string naudabaouip { get; set; } 
       public string naudabaouid { get; set; } 
       public string naudabaopassword { get; set; } 
       public string naudabaoport { get; set; } 
       public string naudabaodb { get; set; } 
       public string synctype { get; set; } 
       public bool isactive { get; set; } 
    }
}