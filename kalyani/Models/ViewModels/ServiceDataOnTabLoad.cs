using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
public class ServiceDataOnTabLoad 
  {
    
    public long id {get; set;}
    public string jobCardDate {get; set;}
    public string jobCardNumber {get; set;}
    public string serviceAdvisor {get; set;}
    public string psfstatus {get; set;}
    public DateTime?  serviceDate {get; set;}    
    public string serviceType {get; set;}        
    public string lastServiceMeterReading {get; set;}
    public string serviceLocaton {get; set;}
    public string kilometer {get; set;}
    public string category {get; set;}
    public double partAmt {get; set;}
    public double labourAmt {get; set;}
    public double totalAmt {get; set;}
    public double mfgPartsTotal {get; set;}
	public double localPartsTotal {get; set;}
	public double maximileTotal {get; set;}
	public double oilConsumablesTotal {get; set;}
	public double maxiCareTotal {get; set;}
	public double mfgAccessoriesTotal {get; set;}
	public double localAccessoriesTotal {get; set;}
	public double inhouseLabourTotal {get; set;}
	public double outLabourTotal {get; set;}
	public string menuCodeDesc {get; set;}
	public double billAmt {get; set;}
	public string phonenumber {get; set;}
    public string workshop {get; set;}
    public string defectDetails {get; set;}
    public string laborDetails {get; set;}
    public string billDate { get; set; }
    public string custName { get; set; }
        public string jobcardlocation { get; set; }
        public DateTime uploadDateuploadDate { get; set; }
        public string technician { get; set; }
        public long? vehicle_vehicle_id { get; set; }
        public string kminhours { get; set; }


    }
}