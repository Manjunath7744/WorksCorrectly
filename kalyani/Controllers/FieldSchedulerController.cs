using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using Newtonsoft.Json;
using AutoSherpa_project.Models.ViewModels;
using System.Data.Entity.Core.Objects;
using MySql.Data.MySqlClient;
using Firebase.Database;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Net;
using System.IO;
using System.Data.Entity.Migrations;

namespace AutoSherpa_project.Controllers
{
    public class FieldSchedulerController : Controller
    {
        #region Kalyani 1 to 1 changeAssignment
        // GET: FieldScheduler
        public ActionResult FieldSchedulerView()
        {
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    ViewBag.FieldLocation = dBContext.fieldwalkinlocations.Where(x => x.typeOfLocation == "1").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [Obsolete]
        public ActionResult getDataforFieldScheduer(string values)
        {
            List<fieldSchedulerVM> fieldSchedulerVMs = new List<fieldSchedulerVM>();
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    var getData = JsonConvert.DeserializeObject<fieldSchedulerFilter>(values);
                    string from = getData.fromDate.Value.ToString("yyyy-MM-dd");
                    string to = getData.toDate.Value.ToString("yyyy-MM-dd");
                    DateTime fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", null).Date;
                    DateTime toDate = DateTime.ParseExact(to, "yyyy-MM-dd", null).Date;
                    var fieldLoc = long.Parse(getData.fieldLocation);

                    if (getData.appointmentType == "Payment Collection" || getData.appointmentType == "Direct visit")
                    {
                        List<appointmentbooked> appBooked = dBContext.appointmentbookeds.Where(x => x.typeOfPickup == "Field" && x.purpose == getData.appointmentType && x.fieldWalkinLocation == fieldLoc
                                                                      && (x.appointmentDate >= fromDate && x.appointmentDate <= toDate)).ToList();
                        if (getData.assignedStatus == "Assigned")
                        {
                            appBooked = appBooked.Where(x => x.insuranceAgent_insuranceAgentId != null && x.insuranceBookStatus_id == 25).ToList();
                        }
                        else if (getData.assignedStatus == "Unassigned")
                        {
                            appBooked = appBooked.Where(x => x.insuranceAgent_insuranceAgentId == null && x.insuranceBookStatus_id == 25).ToList();
                        }
                        else if (getData.assignedStatus == "All")
                        {
                            appBooked = appBooked.Where(x => x.insuranceBookStatus_id == 25).ToList();
                        }
                        if (appBooked.Count != 0)
                        {

                            foreach (appointmentbooked item in appBooked)
                            {
                                vehicle vehicle = dBContext.vehicles.Single(x => x.vehicle_id == item.vehicle_id);
                                insuranceagent insuranceagent = dBContext.insuranceagents.FirstOrDefault(x => x.insuranceAgentId == item.insuranceAgent_insuranceAgentId);
                                pickupdrop pickupdrop = dBContext.pickupdrops.FirstOrDefault(x => x.id == item.pickupDrop_id);
                                customer customer = dBContext.customers.FirstOrDefault(x => x.id == item.customer_id);
                                phone phone = dBContext.phones.FirstOrDefault(x => x.customer_id == item.customer_id && x.isPreferredPhone == true);
                                wyzuser wyzuser = dBContext.wyzusers.FirstOrDefault(x => x.id == item.wyzuser_id);
                                fieldSchedulerVM fieldSchedulerVM = new fieldSchedulerVM();

                                fieldSchedulerVM.regNo = vehicle.vehicleRegNo;
                                fieldSchedulerVM.aptDateTime = item.appointmentDate + " " + item.appointmentFromTime;
                                if (getData.assignedStatus == "Assigned")
                                {
                                    fieldSchedulerVM.assigned = "Yes";
                                }
                                else if (getData.assignedStatus == "Unassigned")
                                {
                                    fieldSchedulerVM.assigned = "No";
                                }
                                else if (getData.assignedStatus == "All")
                                {
                                    if (item.insuranceAgent_insuranceAgentId != null)
                                    {
                                        fieldSchedulerVM.assigned = "Yes";
                                    }
                                    else
                                    {
                                        fieldSchedulerVM.assigned = "No";
                                    }
                                }
                                fieldSchedulerVM.pin = item.pincode;
                                fieldSchedulerVM.addressOfVisit = item.addressOfVisit;
                                fieldSchedulerVM.type = item.purpose;
                                fieldSchedulerVM.fieldExec = insuranceagent.insuranceAgentName;
                                fieldSchedulerVM.scheduleTime = pickupdrop.pickupDate + " " + pickupdrop.timeFrom + " " + pickupdrop.timeTo;
                                fieldSchedulerVM.custName = customer.customerName;
                                fieldSchedulerVM.phone = phone.phoneNumber;//ispreferred
                                fieldSchedulerVM.chassisNum = vehicle.chassisNo;
                                fieldSchedulerVM.cre = wyzuser.userName;//username
                                fieldSchedulerVM.apptId = item.appointmentId.ToString();
                                fieldSchedulerVM.pickUpDate = "";
                                fieldSchedulerVM.startTime = "";
                                fieldSchedulerVM.endTime = "";
                                fieldSchedulerVM.wyzUserID = item.wyzuser_id.ToString();
                                fieldSchedulerVM.vehicleID = item.vehicle_id.ToString();
                                fieldSchedulerVM.customerID = item.customer_id.ToString();
                                fieldSchedulerVMs.Add(fieldSchedulerVM);
                            }
                        }
                    }
                    else if (getData.appointmentType == "Policy Drop")
                    {
                        List<insuranceassignedinteraction> insuranceassignedinteractions = dBContext.insuranceassignedinteractions.Where(x => x.finalDisposition_id == 94 && x.pd_locationId == fieldLoc
                                                                                           && (x.appointmentDate >= fromDate && x.appointmentDate <= toDate)).ToList();
                        if (getData.assignedStatus == "Assigned")
                        {
                            insuranceassignedinteractions = insuranceassignedinteractions.Where(x => x.FEID != 0).ToList();
                        }
                        else if (getData.assignedStatus == "Unassigned")
                        {
                            insuranceassignedinteractions = insuranceassignedinteractions.Where(x => x.FEID == 0).ToList();
                        }
                        if (insuranceassignedinteractions.Count != 0)
                        {
                            foreach (insuranceassignedinteraction item in insuranceassignedinteractions)
                            {
                                vehicle vehicle = dBContext.vehicles.FirstOrDefault(x => x.vehicle_id == item.vehicle_vehicle_id);
                                address address = dBContext.addresses.FirstOrDefault(x => x.customer_Id == item.customer_id);
                                string calldispositiondata = dBContext.calldispositiondatas.FirstOrDefault(x => x.id == item.finalDisposition_id).disposition;
                                string insuranceagentName = "";
                                if (item.FEID != 0)
                                {
                                    insuranceagentName = dBContext.insuranceagents.FirstOrDefault(x => x.insuranceAgentId == item.FEID).insuranceAgentName;
                                }
                                pickupdrop pickupdrop = dBContext.pickupdrops.FirstOrDefault(x => x.id == item.pickUPID);
                                customer customer = dBContext.customers.FirstOrDefault(x => x.id == item.customer_id);
                                phone phone = dBContext.phones.FirstOrDefault(x => x.customer_id == item.customer_id && x.isPreferredPhone == true);
                                wyzuser wyzuser = dBContext.wyzusers.FirstOrDefault(x => x.id == item.wyzUser_id);
                                fieldSchedulerVM fieldSchedulerVM = new fieldSchedulerVM();

                                fieldSchedulerVM.regNo = vehicle.vehicleRegNo;
                                fieldSchedulerVM.aptDateTime = item.appointmentDate.ToString();//app from time
                                if (getData.assignedStatus == "Assigned")
                                {
                                    fieldSchedulerVM.assigned = "Yes";
                                }
                                else if (getData.assignedStatus == "Unassigned")
                                {
                                    fieldSchedulerVM.assigned = "No";
                                }
                                else if (getData.assignedStatus == "All")
                                {
                                    if (item.FEID != 0)
                                    {
                                        fieldSchedulerVM.assigned = "Yes";
                                    }
                                    else
                                    {
                                        fieldSchedulerVM.assigned = "No";
                                    }
                                }
                                fieldSchedulerVM.pin = item.policyPincode;
                                fieldSchedulerVM.addressOfVisit = address.concatenatedAdress;
                                fieldSchedulerVM.type = calldispositiondata;
                                fieldSchedulerVM.fieldExec = insuranceagentName;
                                if (pickupdrop != null)
                                {
                                    fieldSchedulerVM.scheduleTime = pickupdrop.pickupDate + " " + pickupdrop.timeFrom + " " + pickupdrop.timeTo;
                                }
                                else
                                {
                                    fieldSchedulerVM.scheduleTime = " ";
                                }
                                fieldSchedulerVM.custName = customer.customerName;
                                fieldSchedulerVM.phone = phone.phoneNumber;
                                fieldSchedulerVM.chassisNum = vehicle.chassisNo;
                                fieldSchedulerVM.cre = wyzuser.userName;
                                fieldSchedulerVM.apptId = item.id.ToString();
                                fieldSchedulerVM.pickUpDate = "";
                                fieldSchedulerVM.startTime = "";
                                fieldSchedulerVM.endTime = "";
                                fieldSchedulerVM.wyzUserID = item.wyzUser_id.ToString();
                                fieldSchedulerVM.vehicleID = item.vehicle_vehicle_id.ToString();
                                fieldSchedulerVM.customerID = item.customer_id.ToString();
                                fieldSchedulerVMs.Add(fieldSchedulerVM);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.ToString().Substring(0, 20);
            }
            return Json(new { data = fieldSchedulerVMs, draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getSMSTemplateMessage(int smsId, int vehiId, int UserId)
        {

            Session["smsId"] = smsId;
            //int UserId = Convert.ToInt32(Session["UserId"].ToString());
            //int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                //smstemplate template = new smstemplate();
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                       new MySqlParameter("@inwyzuser_id",UserId.ToString()),
                       new MySqlParameter("@invehicle_id",vehiId.ToString()),
                       new MySqlParameter("@inlocid",0),
                       new MySqlParameter("@insmsid",smsId.ToString()),
                       new MySqlParameter("@ininsid","0"),
                    };

                    string template = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault().ToString();

                    return Json(new { success = true, sms = template });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }
        }
        public ActionResult sendSMS(string phNum, string smstemplate,int CId, int VId, int UId)
        {
            int smsId = Convert.ToInt32(Session["smsId"].ToString());
            // int locId = Convert.ToInt32(Session["locId"].ToString());
            int custId = CId;
            int vehiId = VId;
            int UserId = UId;

            try
            {
                smstemplate template = new smstemplate();
                smsparameter parameter = new smsparameter();
                using (var db = new AutoSherDBContext())
                {
                    template = db.smstemplates.FirstOrDefault(m => m.smsId == smsId);
                    parameter = db.smsparameters.FirstOrDefault();

                    string APIURL = string.Empty;
                    string uri = template.smsAPI;
                    string message = smstemplate;

                    APIURL = uri + parameter.phone + "=" + phNum + "&" + parameter.message + "=" + message + "&" + parameter.senderid + "=" + template.dealerName;

                    WebRequest request = WebRequest.Create(APIURL);
                    HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                    httpWebRequest.Method = "GET";
                    httpWebRequest.Accept = "application/json";
                    //request.Method = "GET";
                    //request.ContentType = "application/json";

                    HttpWebResponse response = null;
                    response = (HttpWebResponse)httpWebRequest.GetResponse();

                    string response_string = string.Empty;
                    using (Stream strem = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(strem);
                        response_string = sr.ReadToEnd();

                        sr.Close();
                    }

                    smsinteraction smsinteraction = new smsinteraction();

                    smsinteraction.interactionDate = DateTime.Now.ToShortDateString();
                    smsinteraction.interactionDateAndTime = DateTime.Now;
                    smsinteraction.interactionTime = DateTime.Now.ToString("hh:MM:ss");
                    smsinteraction.interactionType = "Text Msg";
                    smsinteraction.responseFromGateway = response_string;
                    smsinteraction.customer_id = custId;
                    smsinteraction.vehicle_vehicle_id = vehiId;
                    smsinteraction.wyzUser_id = UserId;
                    smsinteraction.mobileNumber = phNum;
                    smsinteraction.smsType = template.smsId.ToString();
                    smsinteraction.smsMessage = message;
                    smsinteraction.isAutoSMS = false;


                    if (response_string.Contains(parameter.sucessStatus))
                    {
                        smsinteraction.smsStatus = true;
                        smsinteraction.reason = "Send Successfully";
                    }
                    else
                    {
                        smsstatu status = new smsstatu();

                        //response_string = "200";

                        status = db.smsstatus.FirstOrDefault(m => response_string.Contains(m.code));
                        if (status == null)
                        {
                            smsinteraction.smsStatus = false;
                            smsinteraction.reason = "Sending Failed";
                        }
                        else if (status != null)
                        {
                            smsinteraction.smsStatus = false;
                            smsinteraction.reason = status.description;
                        }
                    }
                    db.smsinteractions.Add(smsinteraction);
                    db.SaveChanges();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false, error = "" });

        }
        //    public async Task<ActionResult> FirebaseUpdation(string dealer)
        //    {
        //        var firebaseBaseURL = new FirebaseClient("https://wyzcrm-2feff.firebaseio.com/");
        //        using (AutoSherDBContext dBContext = new AutoSherDBContext())
        //        {
        //            fieldexecutivefirebaseupdation fieldexecutivefirebaseupdation = new fieldexecutivefirebaseupdation();
        //            List<fieldexecutivefirebaseupdation> appointmentIDs = dBContext.Fieldexecutivefirebaseupdations.Where(x => x.appointmentbookedid != 0 && x.tobepushed == 0).ToList();
        //            List<fieldexecutivefirebaseupdation> insuranceIDs = dBContext.Fieldexecutivefirebaseupdations.Where(x => x.insassignedid != 0 && x.tobepushed == 0).ToList();

        //            if (appointmentIDs.Count > 0)
        //            {
        //                foreach (fieldexecutivefirebaseupdation item in appointmentIDs)
        //                {
        //                    string key = item.firebasekey;
        //                    appointmentbooked appointmentBookedData = new appointmentbooked();
        //                    if(dBContext.appointmentbookeds.Any(x => x.appointmentId == item.appointmentbookedid))
        //                    {
        //                        appointmentBookedData = dBContext.appointmentbookeds.FirstOrDefault(x => x.appointmentId == item.appointmentbookedid);
        //                        insurance insurance = getLatestInsuranceOfVehicle(appointmentBookedData.vehicle_id);
        //                        if (key != null)
        //                        {
        //                            long? lastInsAgentID = appointmentBookedData.lastinsuranceagentid;
        //                            insuranceagent insAgent = dBContext.insuranceagents.FirstOrDefault(x => x.insuranceAgentId == lastInsAgentID);
        //                            wyzuser user = insAgent.wyzuser;
        //                            string dealerCode = user.dealerCode;
        //                            string userName = user.userName;

        //                            string URLPath = dealerCode + "/InsuranceAgent/" + userName  +"/ScheduledCalls/"  + "CallInfo/" + key;
        //                            var firebaseURL = await firebaseBaseURL.Child(URLPath).OnceSingleAsync<ServiceAdvisorHistoryInfo>();//delete
        //                            item.firebasekey = null;
        //                            dBContext.SaveChanges();
        //                        }
        //                        insuranceagent insuranceAgentKNN = appointmentBookedData.insuranceagent;
        //                        wyzuser wyzuserKNN = insuranceAgentKNN.wyzuser;

        //                        long? vehID = appointmentBookedData.vehicle_id;
        //                        long appointmentID = appointmentBookedData.appointmentId;
        //                        long? custID = appointmentBookedData.customer_id;
        //                        ServiceAdvisorHistoryInfo serviceAdvisorHistoryInfo = new ServiceAdvisorHistoryInfo();
        //                        vehicle vehicle = dBContext.vehicles.FirstOrDefault(x => x.vehicle_id == vehID);
        //                        List<insurancecallhistorycube> insurancecallhistorycubes = dBContext.insurancecallhistorycubes.Where(x => x.vehicle_vehicle_id == vehID).ToList();
        //                        serviceAdvisorHistoryInfo.vehicleId = appointmentBookedData.vehicle_id.ToString();//can directly take vehID
        //                        if (insurancecallhistorycubes.Count > 0)
        //                        {
        //                            serviceAdvisorHistoryInfo.lastDisposition = insurancecallhistorycubes.Last().SecondaryDisposition;
        //                            serviceAdvisorHistoryInfo.comments = insurancecallhistorycubes.Last().comments;
        //                        }

        //                        var address = dBContext.addresses.FirstOrDefault(x => x.customer_Id == custID && x.isPreferred == true);
        //                        if (appointmentBookedData.pincodeValue != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.pincode = appointmentBookedData.pincodeValue;
        //                        }
        //                        else if (address != null)//prefered address
        //                        {
        //                            serviceAdvisorHistoryInfo.pincode = address.pincode.ToString();
        //                        }
        //                        else
        //                        {
        //                            serviceAdvisorHistoryInfo.pincode = "0";
        //                        }

        //                        if (appointmentBookedData.customer != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.custId = appointmentBookedData.customer_id.ToString();
        //                        }

        //                        if (appointmentBookedData.insuranceagent.wyzuser != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.userId = appointmentBookedData.insuranceagent.wyzUser_id.ToString();
        //                        }

        //                        if (appointmentBookedData.customer != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.customerName = appointmentBookedData.customer.customerName;
        //                        }

        //                        var phone = dBContext.phones.FirstOrDefault(x => x.customer_id == appointmentBookedData.customer_id && x.isPreferredPhone == true);
        //                        if (phone != null)///////////////////////////////////////
        //                        {
        //                            serviceAdvisorHistoryInfo.customerPhone = phone.phoneNumber.ToString();
        //                        }

        //                        if (appointmentBookedData.wyzuser.dealerCode != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.dealerCode = appointmentBookedData.wyzuser.dealerCode;
        //                        }

        //                        if (vehicle.engineNo != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.engineNo = vehicle.engineNo;
        //                        }

        //                        if (vehicle.vehicleRegNo != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.vehicalRegNo = vehicle.vehicleRegNo;
        //                        }

        //                        if (vehicle.model != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.model = vehicle.model;
        //                        }

        //                        if (vehicle.saleDate != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.saleDate = vehicle.saleDate.Value.ToString("dd/MM/yyyy");
        //                        }

        //                        if (vehicle.chassisNo != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.chassisNo = vehicle.chassisNo;
        //                        }

        //                        if (appointmentBookedData.appointmentDate != null)
        //                        {
        //                            string scheduledDateTime = appointmentBookedData.appointmentDate.Value.ToString("dd/MM/yyyy");
        //                            serviceAdvisorHistoryInfo.serviceScheduledDate = scheduledDateTime;
        //                        }

        //                        if (appointmentBookedData.appointmentFromTime != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.appointmentTime = appointmentBookedData.appointmentFromTime;
        //                        }
        //                        //serviceAdvisorHistoryInfo.appointmentTime= appointmentBookedData.appointmentFromTime != null? appointmentBookedData.appointmentFromTime:null;

        //                        if (appointmentBookedData.addressOfVisit != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.customerAddress = appointmentBookedData.addressOfVisit;
        //                        }

        //                        if (appointmentBookedData.renewalType != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.serviceType = appointmentBookedData.renewalType;
        //                        }

        //                        if (appointmentBookedData.wyzuser != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.creName = appointmentBookedData.wyzuser.userName;
        //                        }

        //                        long callCount = dBContext.callinteractions.Where(x => x.appointmentbooked.appointmentId == appointmentID).Count();
        //                        if (callCount > 0)///////////////////////////////////////////////////////
        //                        {
        //                            callinteraction call = dBContext.callinteractions.Last(x => x.appointmentBooked_appointmentId == appointmentBookedData.appointmentId);
        //                            serviceAdvisorHistoryInfo.interactionDate = call.callDate + " " + call.callTime;
        //                        }

        //                        serviceAdvisorHistoryInfo.appointmentType = appointmentBookedData.purpose;
        //                        serviceAdvisorHistoryInfo.currentAddress = appointmentBookedData.addressOfVisit;

        //                        if (insurance.insuranceCompanyName != null)
        //                        {

        //                            serviceAdvisorHistoryInfo.insuranceCompany = insurance.insuranceCompanyName;

        //                        }

        //                        if (insurance.policyNo != null)
        //                        {

        //                            serviceAdvisorHistoryInfo.lastPolicyNo = insurance.policyNo;

        //                        }

        //                        if (insurance.idv != null)
        //                        {

        //                            serviceAdvisorHistoryInfo.lastIDV = insurance.idv.ToString();

        //                        }

        //                        if (insurance.premiumAmountBeforeTax != null)
        //                        {

        //                            serviceAdvisorHistoryInfo.lastPremium= String.Format("{0:0.00}", insurance.premiumAmountBeforeTax);

        //                        }

        //                        if (insurance.policyIssueDate != null)
        //                        {

        //                            serviceAdvisorHistoryInfo.lastPolicyDate = insurance.policyIssueDate.Value.ToString("dd/MM/yyyy");

        //                        }

        //                        if (insurance.policyDueDate != null)
        //                        {

        //                            serviceAdvisorHistoryInfo.nextRenewalDate = insurance.policyDueDate.Value.ToString("dd/MM/yyyy");

        //                        }

        //                        if (appointmentBookedData.renewalMode != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.renewalMode = appointmentBookedData.renewalMode;
        //                        }

        //                        if (appointmentBookedData.discountValue != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.discountValue = appointmentBookedData.discountValue;
        //                        }

        //                        if (appointmentBookedData.renewalType != null)
        //                        {
        //                            serviceAdvisorHistoryInfo.renewalType = appointmentBookedData.renewalType;
        //                        }

        //                        if (appointmentBookedData.premiumwithTax != 0)/////////******************
        //                        {
        //                            serviceAdvisorHistoryInfo.premium = appointmentBookedData.premiumwithTax.ToString();
        //                        }

        //                        serviceAdvisorHistoryInfo.serviceBookedId = appointmentBookedData.appointmentId.ToString();
        //                        string currentDateIs = DateTime.Now.ToString("dd/MM/yyyy");
        //                        serviceAdvisorHistoryInfo.makeCallFrom = "Insurance Agent";
        //                        serviceAdvisorHistoryInfo.uploadedDate = currentDateIs;
        //                        serviceAdvisorHistoryInfo.status = "Not yet Called";

        //                        string dealerCodeKNN = wyzuserKNN.dealerCode;
        //                        string userNameKNN = wyzuserKNN.userName;
        //                        List<documentuploadhistory> documentList = dBContext.documentuploadhistories.Where(x => x.customerId == custID).ToList();
        //                        List<Dictionary<string, string>> docLink = new List<Dictionary<string, string>>(); 
        //                        foreach (documentuploadhistory doc in documentList)
        //                        {
        //                            Dictionary<string, string> docLinkSingle = new Dictionary<string, string>();
        //                            List<string> filePath = new List<string>();
        //                            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;

        //                            filePath = doc.filePath.Split(';').ToList();
        //                            foreach(var file in filePath)
        //                            {
        //                                string uploadedFileName = (file.Substring(file.IndexOf(doc.user))).Split('\\')[1];
        //                                uploadedFileName = siteRoot + "/UploadedFiles/" + dealerCodeKNN + "/" + doc.user + "/" + uploadedFileName;
        //                                docLinkSingle.Add(doc.documentName, uploadedFileName);
        //                            }
        //                            docLink.Add(docLinkSingle);
        //                        }
        //                        serviceAdvisorHistoryInfo.documentsLink.AddRange(docLink);
        //                        var firebaseURLKNN = await firebaseBaseURL.Child(dealerCodeKNN + "/InsuranceAgent" + userNameKNN + "/" +
        //                                 "ScheduledCalls" + "/" + "CallInfo").PostAsync(serviceAdvisorHistoryInfo);
        //                        var refKey = firebaseURLKNN.Key;
        //                        updateFireBaseOfAppointment(appointmentBookedData, refKey, dealer);
        //                    }


        //                }

        //            }
        //            else if (insuranceIDs.Count > 0)
        //            {
        //                foreach (fieldexecutivefirebaseupdation item in insuranceIDs)
        //                {
        //                    string key = item.firebasekey;
        //                    if(dBContext.insuranceassignedinteractions.Any(x => x.id == item.insassignedid))
        //                    {
        //                        insuranceassignedinteraction ins = dBContext.insuranceassignedinteractions.FirstOrDefault(x => x.id == item.insassignedid);
        //                        if (key != null)
        //                        {
        //                            long? lastAgentID = ins.lastFEID;
        //                            insuranceagent insAgent = dBContext.insuranceagents.FirstOrDefault(x => x.insuranceAgentId == lastAgentID);
        //                            wyzuser user = insAgent.wyzuser;

        //                            string dealerCode = user.dealerCode;
        //                            string userName = user.userName;

        //                            string URLPath = dealerCode + "/InsuranceAgent/" + userName + "/ScheduledCalls/" + "CallInfo/" + key;
        //                            var firebaseURL = await firebaseBaseURL.Child(URLPath).OnceSingleAsync<ServiceAdvisorHistoryInfo>();//delete
        //                            item.firebasekey = null;
        //                            dBContext.SaveChanges();
        //                        }
        //                        long? vehID = ins.vehicle_vehicle_id;
        //                        long insuranceID = ins.id;
        //                        long? custID = ins.customer_id;

        //                        vehicle vehicle = dBContext.vehicles.FirstOrDefault(x => x.vehicle_id == vehID);
        //                        customer customer = dBContext.customers.FirstOrDefault(x => x.id == custID);
        //                        insurance insurance = getLatestInsuranceOfVehicle(vehID);
        //                        List<insurancecallhistorycube> insCall = dBContext.insurancecallhistorycubes.Where(x => x.vehicle_vehicle_id == vehID).ToList();///////////////
        //                        long? agentID = ins.FEID;
        //                        insuranceagent insuranceagent = dBContext.insuranceagents.FirstOrDefault(x => x.insuranceAgentId == agentID);
        //                        wyzuser wyzuser = insuranceagent.wyzuser;

        //                        string dealerCode1 = wyzuser.dealerCode;
        //                        string userName1 = wyzuser.userName;


        //                        ServiceAdvisorHistoryInfo serviceAdvisor = new ServiceAdvisorHistoryInfo();
        //                        serviceAdvisor.vehicleId = vehicle.vehicle_id.ToString();

        //                        if (insCall.Count() > 0)
        //                        {
        //                            serviceAdvisor.lastDisposition = insCall.Last().SecondaryDisposition;
        //                            serviceAdvisor.comments = insCall.Last().comments;
        //                        }

        //                        var addressINS = dBContext.addresses.FirstOrDefault(x => x.customer_Id == custID && x.isPreferred == true);
        //                        if (ins.policyPincode != null)
        //                        {
        //                            serviceAdvisor.pincode = ins.policyPincode;
        //                        }
        //                        else if (addressINS != null)//////////////////////////
        //                        {
        //                            serviceAdvisor.pincode = addressINS.pincode.ToString();
        //                        }
        //                        else
        //                        {
        //                            serviceAdvisor.pincode = "0";
        //                        }

        //                        if (customer != null)
        //                        {
        //                            serviceAdvisor.custId = customer.id.ToString();
        //                        }

        //                        if (wyzuser != null)
        //                        {
        //                            serviceAdvisor.userId = wyzuser.id.ToString();
        //                        }

        //                        if (customer != null)
        //                        {
        //                            serviceAdvisor.customerName = customer.customerName;
        //                        }

        //                        var phoneINS = dBContext.phones.FirstOrDefault(x => x.customer_id == custID && x.isPreferredPhone == true);
        //                        if (phoneINS != null)////pref phone
        //                        {
        //                            serviceAdvisor.customerPhone = phoneINS.phoneNumber;
        //                        }

        //                        if (wyzuser.dealerCode != null)
        //                        {
        //                            serviceAdvisor.dealerCode = wyzuser.dealerCode;
        //                        }

        //                        if (vehicle.engineNo != null)
        //                        {
        //                            serviceAdvisor.engineNo = vehicle.engineNo;
        //                        }

        //                        if (vehicle.vehicleRegNo != null)
        //                        {
        //                            serviceAdvisor.vehicalRegNo = vehicle.vehicleRegNo;
        //                        }

        //                        if (vehicle.model != null)
        //                        {
        //                            serviceAdvisor.model = vehicle.model;
        //                        }

        //                        if (vehicle.saleDate != null)
        //                        {
        //                            serviceAdvisor.saleDate = vehicle.saleDate.Value.ToString("dd/MM/yyyy");
        //                        }

        //                        if (vehicle.chassisNo != null)
        //                        {
        //                            serviceAdvisor.chassisNo = vehicle.chassisNo;
        //                        }

        //                        if (ins.appointmentDate != null)
        //                        {
        //                            string scheduledDateTime = ins.appointmentDate.Value.ToString("dd/MM/yyyy");
        //                            serviceAdvisor.serviceScheduledDate = scheduledDateTime;
        //                        }
        //                        serviceAdvisor.appointmentTime = "12:00:00";

        //                        if (addressINS != null)//////////
        //                        {
        //                            serviceAdvisor.customerAddress = addressINS.concatenatedAdress;
        //                        }

        //                        if (ins.wyzuser != null)
        //                        {
        //                            serviceAdvisor.creName = ins.wyzuser.userName;
        //                        }

        //                        long callCount = dBContext.callinteractions.Where(x => x.insuranceAssignedInteraction_id == insuranceID).Count();

        //                        if (callCount > 0)
        //                        {
        //                            callinteraction call = dBContext.callinteractions.Last(x => x.insuranceAssignedInteraction_id != null && x.insuranceAssignedInteraction_id == insuranceID);
        //                            serviceAdvisor.interactionDate = call.callDate + " " + call.callTime;
        //                        }

        //                        serviceAdvisor.appointmentType = "Policy Drop";
        //                        serviceAdvisor.appointmentType = " ";

        //                        if (insurance.insuranceCompanyName != null)
        //                        {
        //                            serviceAdvisor.insuranceCompany = insurance.insuranceCompanyName;
        //                        }

        //                        if (insurance.policyNo != null)
        //                        {
        //                            serviceAdvisor.lastPolicyNo = insurance.policyNo;
        //                        }

        //                        if (insurance.idv != null)
        //                        {
        //                            serviceAdvisor.lastIDV = insurance.idv.ToString();
        //                        }

        //                        if (insurance.premiumAmountBeforeTax != null)
        //                        {
        //                            serviceAdvisor.lastPremium = String.Format("{0:0.00}", insurance.premiumAmountBeforeTax);
        //                        }

        //                        if (insurance.policyIssueDate != null)
        //                        {
        //                            serviceAdvisor.lastPolicyDate = insurance.policyIssueDate.Value.ToString("dd/MM/yyyy");
        //                        }

        //                        if (insurance.policyDueDate != null)
        //                        {
        //                            serviceAdvisor.nextRenewalDate = insurance.policyDueDate.Value.ToString("dd/MM/yyyy");
        //                        }

        //                        serviceAdvisor.serviceBookedId = "P" + ins.id.ToString();

        //                        string currentDateIs = DateTime.Now.ToString("dd / MM / yyyy");

        //                        serviceAdvisor.makeCallFrom = "Insurance Agent";
        //                        serviceAdvisor.uploadedDate = currentDateIs;
        //                        serviceAdvisor.status = "Not yet Called";

        //                        List<documentuploadhistory> documentList = dBContext.documentuploadhistories.Where(x => x.customerId == custID).ToList();
        //                        List<Dictionary<string, string>> docLink = new List<Dictionary<string, string>>();
        //                        foreach (documentuploadhistory doc in documentList)
        //                        {
        //                            Dictionary<string, string> docLinkSingle = new Dictionary<string, string>();
        //                            List<string> filePath = new List<string>();
        //                            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;

        //                            filePath = doc.filePath.Split(';').ToList();
        //                            foreach (var file in filePath)
        //                            {
        //                                string uploadedFileName = (file.Substring(file.IndexOf(doc.user))).Split('\\')[1];
        //                                uploadedFileName = siteRoot + "/UploadedFiles/" + dealerCode1 + "/" + doc.user + "/" + uploadedFileName;
        //                                docLinkSingle.Add(doc.documentName, uploadedFileName);
        //                            }
        //                            docLink.Add(docLinkSingle);
        //                        }
        //                        serviceAdvisor.documentsLink.AddRange(docLink);

        //                        string URLPath1 = dealerCode1 + "/InsuranceAgent/" + userName1 + "/ScheduledCalls/" + "CallInfo/";
        //                        var firebaseURL1 = await firebaseBaseURL.Child(URLPath1).PostAsync(serviceAdvisor);
        //                        string referenceKey = firebaseURL1.Key;
        //                        updateFireBaseOfInsurance(ins, referenceKey, dealer);


        //                    }
        //                }
        //            }
        //        }

        //        return View();
        //    }

        //    public insurance getLatestInsuranceOfVehicle(long? vehID)
        //    {
        //        using (AutoSherDBContext dBContext = new AutoSherDBContext())
        //        {
        //            DateTime? policyDueDate = dBContext.insurances.Where(x => x.vehicle_id == vehID && x.policyDueDate != null).Max(x => x.policyDueDate);
        //            insurance lastInsurance = dBContext.insurances.FirstOrDefault(x => x.policyDueDate == policyDueDate);
        //            return lastInsurance;
        //        }
        //    }
        //    public void updateFireBaseOfAppointment(appointmentbooked appointmentBookedData,string refKey,string dealer)
        //    {
        //        //logger.info("dealer: " + dealer);
        //        //String query = "USE " + dealer + ";";

        //        //Session hibernateSession = em.unwrap(Session.class);
        //        try
        //        {
        //            using(AutoSherDBContext dBContext=new AutoSherDBContext())
        //            {
        //                //hibernateSession.doWork(new org.hibernate.jdbc.Work()
        //                appointmentBookedData.feFireBaseKey = refKey;
        //                if (appointmentBookedData.insuranceagent != null)
        //                {
        //                    long agentID = appointmentBookedData.insuranceagent.insuranceAgentId;
        //                    appointmentBookedData.lastinsuranceagentid = agentID;
        //                }
        //                else
        //                {
        //                    appointmentBookedData.lastinsuranceagentid = 0;
        //                }
        //                dBContext.appointmentbookeds.Add(appointmentBookedData);

        //                long id = appointmentBookedData.appointmentId;
        //                fieldexecutivefirebaseupdation field = dBContext.Fieldexecutivefirebaseupdations.FirstOrDefault(x => x.appointmentbookedid != 0 && x.appointmentbookedid == id);
        //                field.firebasekey = refKey;
        //                field.tobepushed = 1;
        //                dBContext.Fieldexecutivefirebaseupdations.AddOrUpdate(field);
        //                dBContext.SaveChanges();
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }

        //    public void updateFireBaseOfInsurance(insuranceassignedinteraction ins,string referenceKey,string dealer)
        //    {
        //        //logger.info("dealer: " + dealer);
        //        //String query = "USE " + dealer + ";";

        //        //Session hibernateSession = em.unwrap(Session.class);
        //        try
        //        {
        //           // hibernateSession.doWork(new org.hibernate.jdbc.Work()

        //            using (AutoSherDBContext dBContext=new AutoSherDBContext())
        //            {
        //                ins.feFireBaseKey = referenceKey;
        //                long? agentID = ins.FEID;
        //                ins.lastFEID = agentID;
        //                dBContext.insuranceassignedinteractions.Add(ins);

        //                long id = ins.id;
        //                fieldexecutivefirebaseupdation field = dBContext.Fieldexecutivefirebaseupdations.FirstOrDefault(x => x.insassignedid != 0 && x.insassignedid == id);
        //                field.firebasekey = referenceKey;
        //                field.tobepushed = 1;
        //                dBContext.Fieldexecutivefirebaseupdations.AddOrUpdate(field);
        //                dBContext.SaveChanges();
        //            }
        //        }
        //        catch(Exception ex)
        //        {

        //        }
        //    }
        #endregion
        #region Indus ChangeAssigment

        public ActionResult FeChangeAssignment()
        {

            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    ViewBag.FieldLocation = dBContext.fieldwalkinlocations.Where(x => x.typeOfLocation == "1").ToList();
                }
            }
            catch (Exception ex)
            {

            }
           


            return View();
        }
      /* public  List<String> FetchDriverData( int locationId)
        {

            return 
        }
      */
        #endregion
    }
}
