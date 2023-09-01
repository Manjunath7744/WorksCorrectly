using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Quartz;
using NLog;
using Newtonsoft.Json;
using AutoSherpa_project.Models.Schedulers;
using AutoSherpa_project.Controllers;

namespace AutoSherpa_project.Models.Scheduler
{
    [DisallowConcurrentExecution]
    public class FieldSchedulerJob :schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        ////////////////public async Task<ActionResult> FirebaseUpdation(IJobExecutionContext context)

        {

            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");
            logger.Info("\n FE History Data Pushing Running Entered --> DateTime: " + DateTime.Now);
            

                if (siteRoot != "/")
            {
                try
                {
                    string dealerCode = string.Empty;
                    var firebaseBaseURL = new FirebaseClient("https://wyzcrm-2feff.firebaseio.com/");
                    using (AutoSherDBContext dBContext = new AutoSherDBContext())
                    {
                        dealer dealerData = dBContext.dealers.FirstOrDefault();
                        dealerCode = dealerData.dealerCode;
                        if (dealerCode != "INDUS")
                        {
                            string baseURL = string.Empty;
                            if (HomeController.wyzCRM1.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm1"];
                            }
                            else if (HomeController.wyzCRM.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm"];
                            }
                            else if (HomeController.autosherpa1.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa1"];
                            }
                            else if (HomeController.WyzCrmNew.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_wyzNew"];
                            }
                            else if (HomeController.autosherpa3.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa3"];
                            }

                            firebaseBaseURL = new FirebaseClient(baseURL);
                        }

                        schedulers schedulerDetails = dBContext.schedulers.FirstOrDefault(m => m.dealerid == dealerData.id && m.scheduler_name == "fe-push");

                        if (schedulerDetails.isActive == true/* && schedulerDetails.IsItRunning == false*/)
                        {
                            logger.Info("\n FE History Data Pushing Running Started --> DateTime: " + DateTime.Now);
                            int maxlength = 100;
                            startScheduler("fe-push");

                            if (schedulerDetails.datalenght != 0)
                            {
                                maxlength = schedulerDetails.datalenght;
                            }
                            fieldexecutivefirebaseupdation fieldexecutivefirebaseupdation = new fieldexecutivefirebaseupdation();

                            List<fieldexecutivefirebaseupdation> appointmentIDs = dBContext.Fieldexecutivefirebaseupdations.Where(x => x.appointmentbookedid != 0 && x.tobepushed == 0).OrderBy(m => m.id).Skip(0).Take(maxlength).ToList();
                            List<fieldexecutivefirebaseupdation> policyDrop = dBContext.Fieldexecutivefirebaseupdations.Where(x => x.inspolicydrop_id != 0 && x.tobepushed == 0).OrderBy(m => m.id).Skip(0).Take(maxlength).ToList();

                            logger.Info("Current going data\n Appointment Data:{0}\n PolicyDrop data:{1}", JsonConvert.SerializeObject(appointmentIDs), JsonConvert.SerializeObject(policyDrop));

                            // Pushing data having appointment id - Insurance Agent
                            if (appointmentIDs.Count > 0)
                            {
                                foreach (fieldexecutivefirebaseupdation item in appointmentIDs)
                                {
                                    try
                                    {
                                        string key = item.lastfirebase_key;

                                        appointmentbooked appointmentBookedData = new appointmentbooked();
                                        if (dBContext.appointmentbookeds.Any(x => x.appointmentId == item.appointmentbookedid))
                                        {
                                            appointmentBookedData = dBContext.appointmentbookeds.Include("insuranceagent").Include("insuranceagent.wyzuser").Include("wyzuser").FirstOrDefault(x => x.appointmentId == item.appointmentbookedid);
                                            insurance insurance = getLatestInsuranceOfVehicle(appointmentBookedData.vehicle_id);

                                            if (item.agentId != 0) // checking does this data already there in firebase/pushed, if true, deleting old data from firebase.
                                            {
                                                insuranceagent insAgent = dBContext.insuranceagents.FirstOrDefault(x => x.insuranceAgentId == item.agentId);
                                                wyzuser user = insAgent.wyzuser;
                                                dealerCode = user.dealerCode;
                                                string userName = user.userName;
                                                if (dealerCode != "POPULAR" || dealerCode != "POPULAR")
                                                {
                                                    string URLPath = dealerCode + "/InsuranceAgent/" + userName + "/ScheduledCalls/" + "CallInfo/" + key;
                                                    await firebaseBaseURL.Child(URLPath).DeleteAsync();//delete
                                                }                                       //item.firebasekey = null;
                                                                                                   //item.tobepushed = 1;
                                                                                                   //dBContext.SaveChanges();

                                                if (item.isCancelled == true)
                                                {
                                                    dBContext.Fieldexecutivefirebaseupdations.Remove(item);
                                                    dBContext.SaveChanges();
                                                    continue;
                                                }
                                            }

                                            insuranceagent insuranceAgentKNN = appointmentBookedData.insuranceagent;
                                            wyzuser wyzuserKNN = appointmentBookedData.insuranceagent.wyzuser;

                                            long? vehID = appointmentBookedData.vehicle_id;
                                            long appointmentID = appointmentBookedData.appointmentId;
                                            long? custID = appointmentBookedData.customer_id;

                                            FEFirebaseData serviceAdvisorHistoryInfo = new FEFirebaseData();


                                            vehicle vehicle = dBContext.vehicles.FirstOrDefault(x => x.vehicle_id == vehID);

                                            List<insurancecallhistorycube> insurancecallhistorycubes = dBContext.insurancecallhistorycubes.Where(x => x.vehicle_vehicle_id == vehID).ToList();

                                            serviceAdvisorHistoryInfo.vehicleId = appointmentBookedData.vehicle_id.ToString();//can directly take vehID

                                            if (insurancecallhistorycubes.Count > 0)
                                            {
                                                serviceAdvisorHistoryInfo.lastDisposition = insurancecallhistorycubes.Last().SecondaryDisposition;
                                                serviceAdvisorHistoryInfo.comments = insurancecallhistorycubes.Last().comments;
                                                serviceAdvisorHistoryInfo.paymentType = insurancecallhistorycubes.Last().paymentType;
                                            }



                                            if (appointmentBookedData.pincodeValue != null)
                                            {
                                                serviceAdvisorHistoryInfo.pincode = appointmentBookedData.pincodeValue;
                                            }
                                            else //prefered address
                                            {
                                                var address = dBContext.addresses.FirstOrDefault(x => x.customer_Id == custID && x.isPreferred == true);
                                                if (address != null)
                                                {
                                                    serviceAdvisorHistoryInfo.pincode = address.pincode.ToString();
                                                }
                                                else
                                                {
                                                    serviceAdvisorHistoryInfo.pincode = "0";
                                                }
                                            }

                                            if (appointmentBookedData.customer != null)
                                            {
                                                serviceAdvisorHistoryInfo.custId = appointmentBookedData.customer_id.ToString();
                                            }

                                            if (appointmentBookedData.insuranceagent.wyzuser != null)
                                            {
                                                serviceAdvisorHistoryInfo.userId = appointmentBookedData.insuranceagent.wyzUser_id.ToString();
                                            }

                                            if (appointmentBookedData.customer != null)
                                            {
                                                serviceAdvisorHistoryInfo.customerName = appointmentBookedData.customer.customerName;
                                            }

                                            List<string> phone = dBContext.phones.Where(x => x.customer_id == appointmentBookedData.customer_id).Select(m => m.phoneNumber).ToList();
                                            if (phone.Count > 0)///////////////////////////////////////
                                            {
                                                serviceAdvisorHistoryInfo.customerPhone = string.Join(",", phone);
                                            }
                                            else
                                            {
                                                serviceAdvisorHistoryInfo.customerPhone = "";
                                            }

                                            serviceAdvisorHistoryInfo.dealerCode = validateString(appointmentBookedData.wyzuser.dealerCode);
                                            serviceAdvisorHistoryInfo.engineNo = validateString(vehicle.engineNo);
                                            serviceAdvisorHistoryInfo.chassisNo = validateString(vehicle.chassisNo);

                                            serviceAdvisorHistoryInfo.vehicalRegNo = validateString(vehicle.vehicleRegNo);
                                            serviceAdvisorHistoryInfo.model = validateString(vehicle.model);


                                            if (vehicle.saleDate != null)
                                            {
                                                serviceAdvisorHistoryInfo.saleDate = vehicle.saleDate.Value.ToString("dd-MM-yyyy");
                                            }


                                            if (appointmentBookedData.appointmentDate != null)
                                            {
                                                string scheduledDateTime = appointmentBookedData.appointmentDate.Value.ToString("dd-MM-yyyy");
                                                serviceAdvisorHistoryInfo.aptScheduledDate = scheduledDateTime;
                                            }

                                            serviceAdvisorHistoryInfo.appointmentTime = validateString(appointmentBookedData.appointmentFromTime);
                                            serviceAdvisorHistoryInfo.customerAddress = validateString(appointmentBookedData.addressOfVisit);
                                            serviceAdvisorHistoryInfo.currentAddress = validateString(appointmentBookedData.addressOfVisit);
                                            //serviceAdvisorHistoryInfo.appointmentTime= appointmentBookedData.appointmentFromTime != null? appointmentBookedData.appointmentFromTime:null;


                                            //if (appointmentBookedData.renewalType != null)
                                            //{
                                            //    serviceAdvisorHistoryInfo.serviceType = appointmentBookedData.renewalType;
                                            //}

                                            if (appointmentBookedData.wyzuser != null)
                                            {
                                                serviceAdvisorHistoryInfo.creName = appointmentBookedData.wyzuser.firstName + "(" + appointmentBookedData.wyzuser.userName + ")";
                                                serviceAdvisorHistoryInfo.crePhoneNumber = appointmentBookedData.wyzuser.phoneNumber;
                                            }
                                            if (dBContext.callinteractions.Count(x => x.appointmentbooked.appointmentId == appointmentID) > 0)
                                            {
                                                long maxCallId = dBContext.callinteractions.Where(x => x.appointmentbooked.appointmentId == appointmentID).Max(m => m.id);
                                                if (maxCallId > 0)///////////////////////////////////////////////////////
                                                {
                                                    callinteraction call = dBContext.callinteractions.FirstOrDefault(x => x.id == maxCallId);
                                                    serviceAdvisorHistoryInfo.interactionDate = call.callDate + " " + call.callTime;
                                                }
                                            }

                                            serviceAdvisorHistoryInfo.appointmentType = appointmentBookedData.purpose;

                                            if (insurance != null)
                                            {
                                                serviceAdvisorHistoryInfo.insuranceCompany = validateString(insurance.insuranceCompanyName);

                                                serviceAdvisorHistoryInfo.lastPolicyNo = validateString(insurance.policyNo);
                                                serviceAdvisorHistoryInfo.lastIDV = insurance.idv.ToString();
                                                serviceAdvisorHistoryInfo.lastPremium = String.Format("{0:0.00}", insurance.premiumAmountBeforeTax);

                                                if (insurance.policyIssueDate != null)
                                                {
                                                    serviceAdvisorHistoryInfo.lastPolicyDate = insurance.policyIssueDate.Value.ToString("dd-MM-yyyy");

                                                }

                                                if (insurance.policyDueDate != null)
                                                {
                                                    serviceAdvisorHistoryInfo.nextRenewalDate = insurance.policyDueDate.Value.ToString("dd-MM-yyyy");
                                                }

                                                if (insurance.RiskInceptionDate != null && insurance.policyDueDate != null)
                                                {
                                                    serviceAdvisorHistoryInfo.lastCoverage = Convert.ToDateTime(insurance.RiskInceptionDate).ToString("dd-MM-yyyy") + " to " + Convert.ToDateTime(insurance.policyDueDate).ToString("dd-MM-yyyy");
                                                }
                                            }
                                            else
                                            {
                                                serviceAdvisorHistoryInfo.insuranceCompany = "";

                                                serviceAdvisorHistoryInfo.lastPolicyNo = "";
                                                serviceAdvisorHistoryInfo.lastIDV = "";
                                                serviceAdvisorHistoryInfo.lastPremium = "0.00";
                                                serviceAdvisorHistoryInfo.lastPolicyDate = "";
                                                serviceAdvisorHistoryInfo.nextRenewalDate = "";
                                            }


                                            serviceAdvisorHistoryInfo.renewalMode = validateString(appointmentBookedData.renewalMode);
                                            serviceAdvisorHistoryInfo.discountValue = validateString(appointmentBookedData.discountValue);
                                            serviceAdvisorHistoryInfo.renewalType = validateString(appointmentBookedData.renewalType);


                                            //*************************************** newly added Manoj - 11-08-2020 ***************************

                                            serviceAdvisorHistoryInfo.premiumWithValue = appointmentBookedData.premiumwithdiscount;
                                            //serviceAdvisorHistoryInfo.discountCoupon = validateString(appointmentBookedData.discountValue) + validateString(appointmentBookedData.coupon);
                                            serviceAdvisorHistoryInfo.coupon = validateString(appointmentBookedData.coupon);

                                            //****************************************************************************************************

                                            if (appointmentBookedData.premiumwithTax != 0)/////////******************
                                            {
                                                serviceAdvisorHistoryInfo.premium = appointmentBookedData.premiumwithTax.ToString();
                                            }

                                            serviceAdvisorHistoryInfo.appointmentbookedId = appointmentBookedData.appointmentId.ToString();
                                            serviceAdvisorHistoryInfo.makeCallFrom = "Insurance Agent";
                                            serviceAdvisorHistoryInfo.uploadedDate = DateTime.Now.ToString("dd-MM-yyyy");
                                            serviceAdvisorHistoryInfo.disposition = "Not yet Called";
                                            serviceAdvisorHistoryInfo.status = "Unattempted";

                                            string dealerCodeKNN = wyzuserKNN.dealerCode;
                                            string userNameKNN = wyzuserKNN.userName;
                                            List<documentuploadhistory> documentList = dBContext.documentuploadhistories.Where(x => x.customerId == custID).ToList();
                                            List<Dictionary<string, string>> docLink = new List<Dictionary<string, string>>();

                                            if (documentList.Count > 0)
                                            {
                                                serviceAdvisorHistoryInfo.documentsLink = new List<Dictionary<string, string>>();
                                                foreach (documentuploadhistory doc in documentList)
                                                {
                                                    Dictionary<string, string> docLinkSingle = new Dictionary<string, string>();
                                                    List<string> filePath = new List<string>();
                                                    //string siteRoot = HttpRuntime.AppDomainAppVirtualPath;

                                                    filePath = doc.filePath.Split(';').ToList();
                                                    foreach (var file in filePath)
                                                    {
                                                        string uploadedFileName = (file.Substring(file.IndexOf(doc.user))).Split('\\')[1];
                                                        uploadedFileName = siteRoot + "/UploadedFiles/" + dealerCodeKNN + "/" + doc.user + "/" + uploadedFileName;
                                                        docLinkSingle.Add(doc.documentName, uploadedFileName);
                                                    }
                                                    docLink.Add(docLinkSingle);
                                                }
                                                serviceAdvisorHistoryInfo.documentsLink.AddRange(docLink);
                                                serviceAdvisorHistoryInfo.documentUploaded = "Yes";
                                            }
                                            else
                                            {
                                                serviceAdvisorHistoryInfo.documentUploaded = "No";
                                            }
                                            serviceAdvisorHistoryInfo.firebaseKeysToRemove = getFirebaseKeyToRemove();

                                            if (dealerData.dealerCode != "POPULAR")
                                            {
                                                int item_id = dBContext.Fieldexecutivefirebaseupdations.Where(x => x.id == item.id && x.tobepushed == 0).Count();

                                                if (item_id > 0)
                                                {
                                                    var firebaseURLKNN = await firebaseBaseURL.Child(dealerCodeKNN + "/InsuranceAgent/" + userNameKNN + "/" +
                                                         "ScheduledCalls" + "/" + "CallInfo").PostAsync(serviceAdvisorHistoryInfo);
                                                    string refKey = firebaseURLKNN.Key;

                                                    logger.Info("\n Below Data got pushed in FE:\n" + JsonConvert.SerializeObject(item));

                                                    //refKey = "123";

                                                    //*********************** new Function Removed Updating here *****************************
                                                    item.firebasekey = refKey;
                                                }
                                            }
                                            item.tobepushed = 1;
                                            dBContext.Fieldexecutivefirebaseupdations.AddOrUpdate(item);
                                            dBContext.SaveChanges();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string exception = string.Empty;

                                        if (ex.InnerException != null)
                                        {
                                            if (ex.InnerException.InnerException != null)
                                            {
                                                exception = ex.InnerException.InnerException.Message;
                                            }
                                            else
                                            {
                                                exception = ex.InnerException.Message;
                                            }
                                        }
                                        else
                                        {
                                            exception = ex.Message;
                                        }

                                        if (ex.StackTrace.Contains(':'))
                                        {
                                            exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                                        }

                                        logger.Error("FE app file push scheduler error(innerBlock-apt block): \n" + exception);
                                    }

                                }
                            }

                            // Pushing data having insurance assignedinteraction id - PolicyDrop
                            if (policyDrop.Count > 0)
                            {
                                foreach (fieldexecutivefirebaseupdation item in policyDrop)
                                {
                                    try
                                    {
                                        string key = item.lastfirebase_key;
                                        if (dBContext.insPolicyDrop.Any(x => x.id == item.inspolicydrop_id))
                                        {
                                            inspolicydrop indPolicyDrop = dBContext.insPolicyDrop.FirstOrDefault(m => m.id == item.inspolicydrop_id);
                                            if (item.agentId != 0)
                                            {
                                                long? lastAgentID = item.agentId;
                                                insuranceagent insAgent = dBContext.insuranceagents.FirstOrDefault(x => x.insuranceAgentId == lastAgentID);
                                                wyzuser user = insAgent.wyzuser;

                                                dealerCode = user.dealerCode;
                                                string userName = user.userName;
                                                if (dealerCode != "POPULAR")
                                                {
                                                    string URLPath = dealerCode + "/InsuranceAgent/" + userName + "/ScheduledCalls/" + "CallInfo/" + key;
                                                    await firebaseBaseURL.Child(URLPath).DeleteAsync();//delete
                                                                                                       //item.firebasekey = null;
                                                                                                       //item.tobepushed = 1;
                                                                                                       //dBContext.SaveChanges();
                                                }
                                            }

                                            long? vehID = indPolicyDrop.vehicle_id;
                                            long? custID = indPolicyDrop.customer_id;

                                            vehicle vehicle = dBContext.vehicles.FirstOrDefault(m => m.vehicle_id == vehID);
                                            customer customer = dBContext.customers.FirstOrDefault(m => m.id == custID);

                                            insurance insurance = getLatestInsuranceOfVehicle(vehID);

                                            List<insurancecallhistorycube> insCall = dBContext.insurancecallhistorycubes.Where(x => x.vehicle_vehicle_id == vehID).ToList();

                                            long? agentID = indPolicyDrop.agent_id;
                                            insuranceagent insuranceagent = dBContext.insuranceagents.Include("wyzuser").FirstOrDefault(x => x.insuranceAgentId == agentID);
                                            wyzuser Angentwyzuser = insuranceagent.wyzuser;

                                            string dealerCode1 = Angentwyzuser.dealerCode;
                                            string userName1 = Angentwyzuser.userName;

                                            FEFirebaseData serviceAdvisor = new FEFirebaseData();
                                            serviceAdvisor.vehicleId = vehicle.vehicle_id.ToString();

                                            if (insCall.Count() > 0)
                                            {
                                                serviceAdvisor.lastDisposition = insCall.Last().SecondaryDisposition;
                                                serviceAdvisor.paymentType = insCall.Last().paymentType;
                                            }

                                            serviceAdvisor.comments = indPolicyDrop.creRemarks;

                                            serviceAdvisor.pincode = (indPolicyDrop.pincode ?? default(int)).ToString();

                                            if (customer != null)
                                            {
                                                serviceAdvisor.custId = customer.id.ToString();
                                            }

                                            if (Angentwyzuser != null)
                                            {
                                                serviceAdvisor.userId = Angentwyzuser.id.ToString();
                                            }

                                            if (customer != null)
                                            {
                                                serviceAdvisor.customerName = customer.customerName;
                                            }

                                            List<string> phone = dBContext.phones.Where(x => x.customer_id == custID).Select(m => m.phoneNumber).ToList();
                                            if (phone.Count > 0)///////////////////////////////////////
                                            {
                                                serviceAdvisor.customerPhone = string.Join(",", phone);
                                            }
                                            else
                                            {
                                                serviceAdvisor.customerPhone = "";
                                            }

                                            serviceAdvisor.dealerCode = validateString(Angentwyzuser.dealerCode);
                                            serviceAdvisor.engineNo = validateString(vehicle.engineNo);
                                            serviceAdvisor.chassisNo = validateString(vehicle.chassisNo);

                                            serviceAdvisor.vehicalRegNo = validateString(vehicle.vehicleRegNo);
                                            serviceAdvisor.model = validateString(vehicle.model);


                                            if (indPolicyDrop.appointment_date != null)
                                            {
                                                string scheduledDateTime = Convert.ToDateTime(indPolicyDrop.appointment_date).ToString("dd-MM-yyyy");
                                                serviceAdvisor.aptScheduledDate = scheduledDateTime;
                                            }

                                            serviceAdvisor.appointmentTime = validateString(indPolicyDrop.appointment_time);

                                            //if(ins.pickUPID!=0)
                                            //{
                                            //    pickupdrop pickUp = dBContext.pickupdrops.FirstOrDefault(m => m.id == ins.pickUPID);

                                            //    if(pickUp!=null)
                                            //    {
                                            //        serviceAdvisor.appointmentTime = pickUp.timeFrom + " - " + pickUp.timeTo;
                                            //    }
                                            //    else
                                            //    {
                                            //        serviceAdvisor.appointmentTime = "";
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    serviceAdvisor.appointmentTime = "";
                                            //}


                                            serviceAdvisor.customerAddress = validateString(indPolicyDrop.address);
                                            serviceAdvisor.currentAddress = validateString(indPolicyDrop.address);
                                            //if (addressINS != null)//////////
                                            //{
                                            //    serviceAdvisor.customerAddress = addressINS.concatenatedAdress;
                                            //}
                                            wyzuser CreUser = dBContext.wyzusers.FirstOrDefault(m => m.id == indPolicyDrop.wyzuser_id);
                                            if (CreUser != null)
                                            {
                                                serviceAdvisor.creName = CreUser.firstName + "(" + CreUser.userName + ")";
                                                serviceAdvisor.crePhoneNumber = CreUser.phoneNumber;
                                            }

                                            //long maxCallId = dBContext.callinteractions.Where(x => x.insuranceAssignedInteraction_id == ins.id).Max(m => m.id);

                                            //if (maxCallId > 0)
                                            //{
                                            //    callinteraction call = dBContext.callinteractions.FirstOrDefault(x => x.id == maxCallId);
                                            //    serviceAdvisor.interactionDate = call.callDate + " " + call.callTime;
                                            //}

                                            serviceAdvisor.interactionDate = indPolicyDrop.updated_datetime.ToString("dd-MM-yyyy");

                                            serviceAdvisor.appointmentType = "Policy Drop";
                                            //serviceAdvisor.appointmentType = " ";

                                            if (insurance != null)
                                            {
                                                serviceAdvisor.insuranceCompany = validateString(insurance.insuranceCompanyName);
                                                serviceAdvisor.lastPolicyNo = validateString(insurance.policyNo);
                                                serviceAdvisor.lastIDV = insurance.idv.ToString();
                                                serviceAdvisor.lastPremium = String.Format("{0:0.00}", insurance.premiumAmountBeforeTax);


                                                if (insurance.policyIssueDate != null)
                                                {
                                                    serviceAdvisor.lastPolicyDate = insurance.policyIssueDate.Value.ToString("dd-MM-yyyy");
                                                }

                                                if (insurance.policyDueDate != null)
                                                {
                                                    serviceAdvisor.nextRenewalDate = insurance.policyDueDate.Value.ToString("dd-MM-yyyy");
                                                }

                                                if (insurance.RiskInceptionDate != null && insurance.policyDueDate != null)
                                                {
                                                    serviceAdvisor.lastCoverage = Convert.ToDateTime(insurance.RiskInceptionDate).ToString("dd-MM-yyyy") + " to " + Convert.ToDateTime(insurance.policyDueDate).ToString("dd-MM-yyyy");
                                                }
                                            }
                                            else
                                            {
                                                serviceAdvisor.insuranceCompany = "";
                                                serviceAdvisor.lastPolicyNo = "";
                                                serviceAdvisor.lastIDV = "";
                                                serviceAdvisor.lastPremium = "0.00";

                                                serviceAdvisor.lastPolicyDate = "";
                                                serviceAdvisor.nextRenewalDate = "";
                                            }


                                            serviceAdvisor.appointmentbookedId = "PolicyDrop_" + indPolicyDrop.id.ToString();

                                            string currentDateIs = DateTime.Now.ToString("dd-MM-yyyy");

                                            serviceAdvisor.makeCallFrom = "Insurance Agent";
                                            serviceAdvisor.uploadedDate = currentDateIs;
                                            serviceAdvisor.disposition = "Not yet Called ";
                                            serviceAdvisor.status = "Unattempted";

                                            serviceAdvisor.discountValue = "0";
                                            serviceAdvisor.premiumWithValue = 0;
                                            //serviceAdvisorHistoryInfo.discountCoupon = validateString(appointmentBookedData.discountValue) + validateString(appointmentBookedData.coupon);
                                            serviceAdvisor.coupon = "0";

                                            List<documentuploadhistory> documentList = dBContext.documentuploadhistories.Where(x => x.customerId == custID).ToList();
                                            List<Dictionary<string, string>> docLink = new List<Dictionary<string, string>>();

                                            if (documentList.Count() > 0)
                                            {
                                                serviceAdvisor.documentsLink = new List<Dictionary<string, string>>();
                                                foreach (documentuploadhistory doc in documentList)
                                                {

                                                    Dictionary<string, string> docLinkSingle = new Dictionary<string, string>();
                                                    List<string> filePath = new List<string>();
                                                    //string siteRoot = HttpRuntime.AppDomainAppVirtualPath;

                                                    filePath = doc.filePath.Split(';').ToList();
                                                    foreach (var file in filePath)
                                                    {
                                                        string uploadedFileName = (file.Substring(file.IndexOf(doc.user))).Split('\\')[1];
                                                        uploadedFileName = siteRoot + "/UploadedFiles/" + dealerCode1 + "/" + doc.user + "/" + uploadedFileName;
                                                        docLinkSingle.Add(doc.documentName, uploadedFileName);
                                                    }
                                                    docLink.Add(docLinkSingle);
                                                }
                                                serviceAdvisor.documentsLink.AddRange(docLink);
                                                serviceAdvisor.documentUploaded = "Yes";
                                            }
                                            else
                                            {
                                                serviceAdvisor.documentUploaded = "No";
                                            }

                                            serviceAdvisor.firebaseKeysToRemove = getFirebaseKeyToRemove();
                                            if (dealerData.dealerCode != "POPULAR")
                                            {
                                                int item_id = dBContext.Fieldexecutivefirebaseupdations.Where(x => x.id == item.id && x.tobepushed == 0).Count();

                                                if (item_id > 0) {
                                                string URLPath1 = dealerCode1 + "/InsuranceAgent/" + userName1 + "/ScheduledCalls/" + "CallInfo/";
                                                var firebaseURL1 = await firebaseBaseURL.Child(URLPath1).PostAsync(serviceAdvisor);
                                                string referenceKey = firebaseURL1.Key;

                                                logger.Info("\n Below Data got pushed in FE:\n" + JsonConvert.SerializeObject(item));

                                                item.firebasekey = referenceKey;
                                                }
                                            }
                                            item.tobepushed = 1;
                                            dBContext.Fieldexecutivefirebaseupdations.AddOrUpdate(item);
                                            dBContext.SaveChanges();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string exception = string.Empty;

                                        if (ex.InnerException != null)
                                        {
                                            if (ex.InnerException.InnerException != null)
                                            {
                                                exception = ex.InnerException.InnerException.Message;
                                            }
                                            else
                                            {
                                                exception = ex.InnerException.Message;
                                            }
                                        }
                                        else
                                        {
                                            exception = ex.Message;
                                        }

                                        if (ex.StackTrace.Contains(':'))
                                        {
                                            exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                                        }

                                        logger.Error("FE app file push scheduler error(innerBlock-insAssigned block): \n" + exception);
                                    }

                                }
                            }

                            stopScheduler("fe-push");
                        }
                        else
                        {
                            logger.Info("\n fe-push Inactive / Not Exist / Already Running");
                        }

                    }
                    }
                //}
                catch (Exception ex)
                {
                    string exception = string.Empty;

                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null)
                        {
                            exception = ex.InnerException.InnerException.Message;
                        }
                        else
                        {
                            exception = ex.InnerException.Message;
                        }
                    }
                    else
                    {
                        exception = ex.Message;
                    }

                    if (ex.StackTrace.Contains(':'))
                    {
                        exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                    }

                    logger.Error("FE app file push scheduler error(outer block): \n" + exception);
                    stopScheduler("fe-push");
                }
                //ChangeRunningStatus();
            }
            logger.Info("\n FE History Data Pushing Completed DateTime: " + DateTime.Now);
        }

        public string validateString(string stringVar)
        {
            if (string.IsNullOrEmpty(stringVar))
            {
                return "";
            }
            else
            {
                return stringVar;
            }
        }


        public string getFirebaseKeyToRemove()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<string> fks = db.Dataremovals.Where(m => m.removed == true).Select(m => m.firebasekey).ToList();

                    if (fks.Count > 0)
                    {
                        return string.Join(",", fks);
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public insurance getLatestInsuranceOfVehicle(long? vehID)
        {
            insurance lastInsurance = null;
            using (AutoSherDBContext db = new AutoSherDBContext())
            {

                if (db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehID).Count() != 0)
                {
                    //long lastInsurence = db.insurances.Include("workshop").Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehId).Max(k => k.id);
                    //callLog.LatestInsurance = db.insurances.SingleOrDefault(m => m.id == lastInsurence);

                    var lastInsurence = db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehID).OrderByDescending(k => k.policyIssueDate).FirstOrDefault();

                    if (lastInsurence != null)
                    {
                        lastInsurance = lastInsurence;
                    }

                }
                //DateTime? policyDueDate = dBContext.insurances.Where(x => x.vehicle_id == vehID && x.policyDueDate != null).Max(x => x.policyDueDate);
                //insurance lastInsurance = dBContext.insurances.FirstOrDefault(x => x.policyDueDate == policyDueDate);
                //return lastInsurance;
            }
            return lastInsurance;
        }

    }
}
