using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
public class FollowUpNotificationModel 
  {
    
    public string followUpTime {get; set;}
    public string dealerCode {get; set;}
    public long callInteraction_id {get; set;}
    public string customerName {get; set;}
    public long customer_id {get; set;}
    public long vehicle_vehicle_id {get; set;}
  }
}