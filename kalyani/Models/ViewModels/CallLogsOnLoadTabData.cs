using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoSherpa_project.Models;

namespace AutoSherpa_project.Models.ViewModels
{
public class CallLogsOnLoadTabData
 {
    public List<WorkShopSummaryDataOnTabLoad> workshopSummaryLoadList {get; set;}
    public List<CustomerDataOnTabLoad> customerLoadList {get; set;}
    public List<VehicleDataOnTabLoad> vehicleLoadList {get; set;}
    public List<WyzUserDataOnTabLoad> wyzUserLoadList {get; set;}
    public List<AssignedInteractionDataOnTabLoad> assignedInteractionLoadList {get; set;}
    public List<ServiceDataOnTabLoad> serviceLoadList {get; set;}
    public List<InsuranceDataOnTabLoad> insuranceLoadList {get; set;}
    public List<SRDispositionDataOnTabLoad> srdispositionLoadList {get; set;}
    public List<CallInteractionDataOnTabLoad> callInteractionLoadList {get; set;}  
    public List<ServiceAdvisorDataOnTabLoad> serviceAdvisorLoadList {get; set;}
    public List<PickupDropDataOnTabLoad> pickupDropLoadList {get; set;}
 
    public List<SchedularDataOnTabLoad> driverLoadList {get; set;}
    public List<ServicebookedDataOnTabLoad> servicebookedLoadList {get; set;}
    public List<customer>  customerList {get; set;}
    public List<vehicle>   vehicleList {get; set;}
    public List<wyzuser> wyzUserList {get; set;}
    public List<assignedinteraction> assignedInteractionsList {get; set;}
    public List<service> serviceList {get; set;}
    public List<insurance> insuranceList {get; set;}
    public List<srdisposition> srdispositionList {get; set;}
    public List<callinteraction> interactionCallList {get; set;}  
    public List<serviceadvisor> serviceAdvisorList {get; set;}
    public List<pickupdrop> pickupDropList {get; set;}
    public List<driver> driverList {get; set;}
  }
}
