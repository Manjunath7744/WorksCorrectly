using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("pmslabour")]

    public class pmslabour
    {
        public int id { get; set; }
        public long cityid { get; set; }
        public long modelid { get; set; }
        public string fuelType { get; set; }
        public long mileageid { get; set; }
        //public float labourAmount { get; set; }
        //public float partsAmount { get; set; }
        public float wheelAlignment { get; set; }
        public float wheelBalancing { get; set; }
        public float engineOil { get; set; }
        public float oilFillter { get; set; }
        public float brakeFluid { get; set; }
        public float coolant { get; set; }
        public float sparkPlug { get; set; }
        public float airFilter { get; set; }
        public float fuelFilter { get; set; }
        public float belt { get; set; }

        public float basic { get; set; }
        public float hygiene { get; set; }
        public float other1 { get; set; }
        public float other2 { get; set; }
        //public float total { get; set; }
        public float pmsLabourwithtax { get; set; }
        public float gasketDrainPlug { get; set; }
        public float oilFilterGasket { get; set; }
        public float clutchFluid { get; set; }
        public float transmissionOil { get; set; }
    }
}