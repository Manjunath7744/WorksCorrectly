using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("whatsappParams")]
    public class whatsappParams
    {
        public int id { get; set; }
        public int smsId { get; set; }
        public int paramCount { get; set; }
        [StringLength(45)]
        public string param1 { get; set; }
        [StringLength(45)]
        public string param2 { get; set; }
        [StringLength(45)]
        public string param3 { get; set; }
        [StringLength(45)]
        public string param4 { get; set; }
        [StringLength(45)]
        public string param5 { get; set; }
        [StringLength(45)]
        public string param6 { get; set; }
        [StringLength(45)]
        public string param7 { get; set; }
        [StringLength(45)]
        public string param8 { get; set; }
        [StringLength(45)]
        public string param9 { get; set; }
        [StringLength(45)]
        public string param10 { get; set; }
        [StringLength(45)]
        public string param11 { get; set; }
        [StringLength(45)]
        public string param12 { get; set; }
        [StringLength(45)]
        public string param13 { get; set; }
    }
}
