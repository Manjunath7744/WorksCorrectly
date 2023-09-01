using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
   public class ComplaintViewModel 
 {
    
    public string model {get; set;}
    public string vehicleRegNo {get; set;}
        public long vehicleId { get; set; }
    public string variant {get; set;}
    public DateTime? saleDate {get; set;}
    public string chassisNo {get; set;}
    public string customerName {get; set;}
    public string customerEmail {get; set;}
    public string preferredAdress {get; set;}
    public string customerMobileNo {get; set;}
    public string concatenatedAdress {get; set;}
    public string complaintNumber {get; set;}
    public string complaintType {get; set;}
    public string functionName {get; set;}
    public string sourceName {get; set;}
    public string subcomplaintType {get; set;}
    public string assignedUser {get; set;}
    public string resolutionBy {get; set;}
    public long location {get; set;}
    public string priority {get; set;}
    public string complaintStatus {get; set;}
    public DateTime? issueDate {get; set;}
    public long ageOfComplaint {get; set;}
    public long wyzUser {get; set;}
    public string customerstatus {get; set;}
    public string reasonFor {get; set;}
    public string actionTaken {get; set;}
    public DateTime? lastServiceDate {get; set;}
    public string raisedDate {get; set;}
    public string serviceadvisor {get; set;}  
    public string workshop {get; set;}
    public string endDate {get; set;}
    public long customerId { get; set; }
    public bool isComplaintExist { get; set; }
        public List<role> Roles { get; set; }
        public string funRole { get; set; }

        public customer Customers { get; set; }
        public vehicle vehicles { get; set; }
        public phone phones { get; set; }
        public email emails { get; set; }
        public address addresss { get; set; }

        public string actionTackens { get; set; }
        public string customerStatuss { get; set; }
        public string ownerships { get; set; }
  }
}