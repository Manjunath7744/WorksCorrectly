using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
public class VehicleDataOnTabLoad 
  {
    
    public string vehicleRegNo {get; set;}
    public string model {get; set;}
    public string variant {get; set;}
    public string color {get; set;}
    
    public DateTime? nextServicedate {get; set;}
    public string nextServicetype {get; set;}
    public string odometerReading {get; set;}
    public string forecastLogic {get; set;}
    public string category {get; set;}
   }
}