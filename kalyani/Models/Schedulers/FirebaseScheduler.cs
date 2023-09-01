using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Firebase.Database;
using Quartz;
using AutoSherpa_project.Controllers;
using NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class FirebaseScheduler :schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;

            logger.Info("\n Android CallSynch Started at: " + DateTime.Now);
            if (siteRoot!="/")
            {
                try
                {

                    using (var db = new AutoSherDBContext())
                    {
                        dealer dealerData = db.dealers.FirstOrDefault();

                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.dealerid == dealerData.id && m.scheduler_name == "callsynch");
                        
                        if(schedulerDetails!=null && schedulerDetails.isActive==true )
                        {
                            logger.Info("\n Android Call Synch Entered --> DateTime: " + DateTime.Now);
                            startScheduler("callsynch");

                            string dealerName = dealerData.dealerCode;
                            string baseURL = string.Empty;
                            if (HomeController.wyzCRM1.Contains(dealerName))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm1"];
                            }
                            else if (HomeController.wyzCRM.Contains(dealerName))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm"];
                            }
                            else if (HomeController.autosherpa1.Contains(dealerName))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa1"];
                            }

                            else if (HomeController.autosherpa3.Contains(dealerName))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa3"];
                            }
                            else if (HomeController.WyzCrmNew.Contains(dealerName))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_wyzNew"];
                            }


                            var firebaseClient = new FirebaseClient(baseURL);


                            var crenames = db.wyzusers.Where(m => m.role == "CRE").Select(m => m.userName).OrderBy(m => m).ToList();
                            foreach (var cre in crenames)
                            {
                                string urlLink = dealerName;

                                logger.Info("\n CallSynch Outgoing CRE: " + cre);

                                urlLink += "/CRE/" + cre + "/WebHistory/CallInfo/";
                                //urlLink += "/CRE/" + "gurpreet_cre" + "/WebHistory/CallInfo/";


                                try
                                {
                                    var fireInst = await firebaseClient
                                   .Child(urlLink).OnceAsync<JObject>();
                                    
                                    if (fireInst.Count != 0 || fireInst.Count > 0)
                                    {
                                        //using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                                        //{
                                        //try
                                        //{
                                        foreach (var data in fireInst)
                                        {
                                            try
                                            {
                                                logger.Info("\n CallSynch incoming current date: " + JsonConvert.SerializeObject(data));

                                                firebaseData fireData = new firebaseData();
                                                fireData = JsonConvert.DeserializeObject<firebaseData>(data.Object.ToString());
                                                string filePath = string.Empty;
                                                if (!string.IsNullOrEmpty(fireData.filePath))
                                                {
                                                    filePath = (fireData.filePath.Replace('+', '_')).Replace('-', '_');
                                                    filePath = "/wyzAudioData/" + filePath;
                                                }

                                                if (fireData.uniqueidForCallSync == 0 && string.IsNullOrEmpty(fireData.callDate) && string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(fireData.callTime))
                                                {
                                                    await firebaseClient
                                                         .Child(urlLink + data.Key).DeleteAsync();
                                                }
                                                else
                                                {
                                                    callinteraction callInter = db.callinteractions.FirstOrDefault(m => m.uniqueidForCallSync == fireData.uniqueidForCallSync);
                                                    if (callInter != null && fireData.uniqueidForCallSync != 0)
                                                    {
                                                        //callinteraction callInter = db.callinteractions.Include("customer").Include("customer.phones").FirstOrDefault(m => m.uniqueidForCallSync == fireData.uniqueidForCallSync);
                                                        callInter.callDate = fireData.callDate.Replace('/', '-');
                                                        callInter.callDuration = fireData.callDuration;
                                                        callInter.callTime = fireData.callTime;
                                                        callInter.callType = fireData.callType;
                                                        callInter.ringTime = fireData.ringTime;
                                                        callInter.mediaFileLob = fireData.mediaFileLob;
                                                        callInter.chasserCall = false;
                                                        callInter.latitude = fireData.latitude;
                                                        callInter.longitude = fireData.logitude;
                                                        callInter.filePath = filePath;
                                                        callInter.fileSize = fireData.fileSize;
                                                        callInter.firebaseKey = data.Key;
                                                        callInter.callStatus = true;

                                                        db.callinteractions.AddOrUpdate(callInter);
                                                        db.SaveChanges();
                                                        long filesize = 0;
                                                        string size = string.Empty;
                                                        if (!string.IsNullOrEmpty(fireData.fileSize))
                                                        {
                                                            fireData.fileSize = fireData.fileSize.Replace(".00", "");
                                                            for (int i = 0; i < fireData.fileSize.Length; i++)
                                                            {
                                                                if (Char.IsDigit(fireData.fileSize[i]))
                                                                    size += fireData.fileSize[i];
                                                            }

                                                            //filesize = long.Parse(fireData.fileSize.Replace("[^0-9]", ""));
                                                            filesize = long.Parse(size);
                                                        }

                                                        callhistorycube callHistory = db.callhistorycubes.FirstOrDefault(m => m.Call_interaction_id == callInter.id);
                                                        if (callHistory != null)
                                                        {
                                                            //callhistorycube callHistory = db.callhistorycubes.FirstOrDefault(m => m.Call_interaction_id == callInter.id);
                                                            callHistory.callDuration = fireData.callDuration;
                                                            callHistory.ringtime = fireData.ringTime;
                                                            callHistory.isCallDurationUpdated = true;
                                                            callHistory.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                                            callHistory.filepath = filePath;
                                                            callHistory.callType = fireData.callType;
                                                            callHistory.fileSize = filesize;
                                                            if (!string.IsNullOrEmpty(fireData.customerPhone))
                                                            {
                                                                callHistory.dailedNumber = fireData.customerPhone;
                                                            }

                                                            callHistory.callStatus = true;

                                                            db.callhistorycubes.AddOrUpdate(callHistory);
                                                            db.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            insurancecallhistorycube callHistoryIns = db.insurancecallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == callInter.id);

                                                            if (callHistoryIns != null)
                                                            {
                                                                callHistoryIns.callDuration = fireData.callDuration;
                                                                callHistoryIns.ringTime = fireData.ringTime;
                                                                callHistoryIns.isCallDurationUpdated = true;
                                                                callHistoryIns.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                                                callHistoryIns.filepath = filePath;
                                                                callHistoryIns.callType = fireData.callType;
                                                                callHistoryIns.fileSize = filesize;
                                                                callHistoryIns.callStatus = true;
                                                                callHistoryIns.dailedNumber = callInter.customer.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
                                                                db.insurancecallhistorycubes.AddOrUpdate(callHistoryIns);
                                                                db.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                try
                                                                {
                                                                    psfcallhistorycube callHistoryPSF = db.psfcallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == callInter.id);

                                                                    if (callHistoryPSF != null)
                                                                    {
                                                                        callHistoryPSF.callDuration = fireData.callDuration;
                                                                        callHistoryPSF.ringTime = fireData.ringTime;
                                                                        callHistoryPSF.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                                                        callHistoryPSF.filepath = filePath;
                                                                        callHistoryPSF.callType = fireData.callType;
                                                                        callHistoryPSF.fileSize = filesize;
                                                                        callHistoryPSF.dailedNumber = callInter.customer.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
                                                                        db.psfcallhistorycubes.AddOrUpdate(callHistoryPSF);
                                                                        db.SaveChanges();
                                                                    }
                                                                }
                                                                catch(Exception ex)
                                                                {
                                                                    string exception = "";
                                                                    if (ex.Message.Contains("inner exception"))
                                                                    {
                                                                        if (ex.InnerException.Message.Contains("inner exception"))
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
                                                                    logger.Error("\nFireCallsynch DataLoop(PFS): " + exception);
                                                                    logger.Info("\n Received Firebase Data Cause Exception: \n" + JsonConvert.SerializeObject(data.Object));
                                                                }
                                                            }
                                                        }

                                                        callsyncdata callsyncdata = new callsyncdata();
                                                        string callMadeDateTime = fireData.callDate + " " + fireData.callTime;

                                                        callsyncdata.uniqueidForCallSync = fireData.uniqueidForCallSync.ToString();
                                                        callsyncdata.callDuration = fireData.callDuration;
                                                        callsyncdata.callDurationUpdated = true;
                                                        callsyncdata.callinteraction_id = callInter.id;
                                                        callsyncdata.callMadeDateAndTime = Convert.ToDateTime(callMadeDateTime);
                                                        callsyncdata.callType = fireData.callType;
                                                        //callsyncdata.csidmap=
                                                        callsyncdata.dailedNumber = fireData.customerPhone;
                                                        callsyncdata.filepath = filePath;
                                                        callsyncdata.isComplaintCall = false;
                                                        callsyncdata.moduletype = 1;
                                                        callsyncdata.ringTime = fireData.ringTime;
                                                        callsyncdata.updatedDate = DateTime.Now;
                                                        callsyncdata.wyzuser_id = callInter.wyzUser_id;
                                                        callsyncdata.isgsmsdata = false;
                                                        db.callSyncDatas.Add(callsyncdata);
                                                        db.SaveChanges();

                                                        await firebaseClient
                                                       .Child(urlLink + data.Key).DeleteAsync();
                                                    }
                                                    else
                                                    {
                                                        callinteraction callInterNew = new callinteraction();

                                                        //if(db.phones.Any(m=>m.phoneNumber==fireData.customerPhone))
                                                        //{
                                                        //    callInter.
                                                        //}
                                                        callInterNew.dailedNoIs = fireData.customerPhone;
                                                        callInterNew.callDate = fireData.callDate.Replace('/', '-');
                                                        callInterNew.callDuration = fireData.callDuration;
                                                        callInterNew.callTime = fireData.callTime;
                                                        callInterNew.callType = fireData.callType;
                                                        callInterNew.ringTime = fireData.ringTime;
                                                        callInterNew.mediaFileLob = fireData.mediaFileLob;
                                                        callInterNew.latitude = fireData.latitude;
                                                        callInterNew.longitude = fireData.logitude;
                                                        callInterNew.filePath = filePath;
                                                        callInterNew.fileSize = fireData.fileSize;
                                                        callInterNew.firebaseKey = data.Key;
                                                        callInterNew.chasserCall = false;
                                                        callInterNew.dealerCode = fireData.dealerCode;
                                                        callInterNew.wyzUser_id = db.wyzusers.FirstOrDefault(m => m.userName == cre).id;
                                                        callInterNew.isCallinitaited = fireData.callType;
                                                        callInterNew.callStatus = true; 

                                                        db.callinteractions.Add(callInterNew);
                                                        db.SaveChanges();

                                                        //callsyncdata callsyncdata = new callsyncdata();

                                                        //string callMadeDateTime = fireData.callDate + " " + fireData.callTime;

                                                        //callsyncdata.uniqueidForCallSync = fireData.uniqueidForCallSync.ToString();
                                                        //callsyncdata.callDuration = fireData.callDuration;
                                                        //callsyncdata.callDurationUpdated = true;
                                                        //callsyncdata.callinteraction_id = callInter.id;
                                                        //callsyncdata.callMadeDateAndTime = Convert.ToDateTime(callMadeDateTime);
                                                        //callsyncdata.callType = fireData.callType;
                                                        ////callsyncdata.csidmap=
                                                        //callsyncdata.dailedNumber = fireData.customerPhone;
                                                        //callsyncdata.filepath = filePath;
                                                        //callsyncdata.isComplaintCall = false;
                                                        //callsyncdata.moduletype = 1;
                                                        //callsyncdata.ringTime = fireData.ringTime;
                                                        //callsyncdata.updatedDate = DateTime.Now;
                                                        //callsyncdata.wyzuser_id = callInter.wyzUser_id;
                                                        //db.callSyncDatas.Add(callsyncdata);
                                                        //db.SaveChanges();
                                                        await firebaseClient
                                                        .Child(urlLink + data.Key).DeleteAsync();
                                                    }
                                                    //logger.Info("Synchronized Data: \n", JsonConvert.SerializeObject(fireData));
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                string exception = "";

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
                                                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                                                }

                                                logger.Error("\nFireCallsynch DataLoop: " + exception);
                                                logger.Info("\n Received Firebase Data Cause Exception: \n" + JsonConvert.SerializeObject(data.Object));
                                            }
                                            //dbTransaction.Commit();
                                        }
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    dbTransaction.Rollback();
                                        //}
                                        //dbTransaction.Commit();
                                        //}


                                    }
                                }
                                catch (Exception ex)
                                {
                                    string exception = "";
                                    if (ex.Message.Contains("inner exception"))
                                    {
                                        if (ex.InnerException.Message.Contains("inner exception"))
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
                                    logger.Error("\nFireCallsynch: " + exception);
                                }

                            }

                            stopScheduler("callsynch");

                        }
                        else
                        {
                            logger.Info("\ncallsynch Inactive / Not Exist / Already Running");
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    stopScheduler("callsynch");
                    string exception = "";
                    if (ex.Message.Contains("inner exception"))
                    {
                        if (ex.InnerException.Message.Contains("inner exception"))
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
                    logger.Error("\nFireCallsynch(Outer Loop): " + exception);
                }
            }
            logger.Info("\n Android CallSynch Stopped at: " + DateTime.Now);
        }

    }
}