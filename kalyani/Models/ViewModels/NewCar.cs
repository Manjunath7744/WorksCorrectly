using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class NewCar
    {
        public string vehiRegNum { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string dealership { get; set; }
        public DateTime purchaseDate { get; set; }


    }
}