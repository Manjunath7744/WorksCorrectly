using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Models.ViewModels
{
    public class SACalllogingVM
    {
        public wyzuser wyzuser { get; set; }
        public customer cust { get; set; }
        public string submittedBtnName { get; set; }

        public customer custoAdd { get; set; }
        public vehicle vehi { get; set; }
        public List<emailtemplate> emailtemplates { get; set; }
        public Email email { get; set; }
        public email emailAdd { get; set; }
        public phone phoneAdd { get; set; }
        public insurance LatestInsurance { get; set; }
        public rosaassignment rosaassignment { get; set; }
        public sainteraction Sainteraction { get; set; }
        public sareferrals Sareferrals { get; set; }
        public address addressesAdd { get; set; }
        public List<phone> phonesAdd { get; set; }
        public List<workshop> allworkshopList { get; set; }
        public List<SelectListItem> citystatesList { get; set; }
        public List<SelectListItem> stateList { get; set; }
        public List<location> locationList { get; set; }

        public List<insurancecompany> companiesList { get; set; }
        public List<smstemplate> smstemplates { get; set; }

        public List<phone> custPhoneList { get; set; }
        public long CustomerId { get; set; }

        public long VehicleId { get; set; }
        public long rosaassignedId { get; set; }

        public long UserId { get; set; }
        public string insurance { get; set; }
        public string newcar { get; set; }
        public string rsa { get; set; }
        public string ew { get; set; }
        //public string nextRoStatus { get; set; }
        public string CustCategory { get; set; }
        public string ceicustCat { get; set; }
        public documentuploadhistory docHistory { get; set; }
        public long prefferedEmail { get; set; }
        public long prefferedPhone { get; set; }
        public List<roreasons> roreasons { get; set; }
        public List<modelslist> roModels { get; set; }
        public List<rsacoverage> rsacoverageLists { get; set; }
        public List<ewcoverage> ewcoverageLists { get; set; }
        public bool enableDispositionForm { get; set; }

    }

    public class repairOrderVM
    {
        public string vehiclereg_no { get; set; }
        public DateTime? rodate { get; set; }
        public string servicetype { get; set; }
        public string pdt { get; set; }
        public string chassisno { get; set; }
        public string customername { get; set; }
        public string rostatus { get; set; }
        public DateTime? saledate { get; set; }
        public long customer_id { get; set; }
        public long vehicle_id { get; set; }
        public long id { get; set; }
    }
    public class repairorderFilter
    {
        public string roStatus { get; set; }
        public string roDate { get; set; }
        public string PDT { get; set; }
        public string PRU { get; set; }
        public int roworkType { get; set; }
        public bool isFiltered { get; set; }
        public int getDataFor { get; set; }
    }

}