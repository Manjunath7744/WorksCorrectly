using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
using System.Net;
using System.IO;
using AutoSherpa_project.Models.ViewModels;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class knowlarityScheduler : schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;

            logger.Info("\n knowlaritycallsynch CallSynch Started at: " + DateTime.Now);
            if (siteRoot != "/")
            {
                try
                {
                    using (var db = new AutoSherDBContext())
                    {
                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == "knowlaritycallsynch");

                        if (schedulerDetails != null && schedulerDetails.isActive == true)
                        {
                            startScheduler("knowlaritycallsynch");
                            logger.Info("\n knowlarity Call Synch Entered --> DateTime: " + DateTime.Now);
                            List<knowlaritycallrecords> callinteractionDetails = db.Database.SqlQuery<knowlaritycallrecords>("select id, recordingpath, calldate, callduration, calltime, dialednumber, agentnumber, uuid, iscaldetailsupdated,(select Callinteraction_id from gsmsynchdata where UniqueGsmId=uuid) as callinteractionid,(select callFor from gsmsynchdata where UniqueGsmId=uuid) as callfor from knowlaritycallrecords where iscaldetailsupdated=0 and uuid in(select UniqueGsmId from gsmsynchdata);").ToList();
                            foreach (var interactionDetails in callinteractionDetails)
                            {
                                knowlaritycallrecords knowlaritycallrecord = db.Knowlaritycallrecords.FirstOrDefault(m => m.id == interactionDetails.id);
                                knowlaritycallrecord.iscaldetailsupdated = true;
                                knowlaritycallrecord.callfor = interactionDetails.callfor;
                                knowlaritycallrecord.callinteractionid = interactionDetails.callinteractionid;
                                db.Knowlaritycallrecords.AddOrUpdate(knowlaritycallrecord);
                                if (interactionDetails.callfor == 1)
                                {
                                    if (db.callhistorycubes.Count(m => m.Call_interaction_id == interactionDetails.callinteractionid) > 0)
                                    {
                                        callhistorycube callHistory = db.callhistorycubes.FirstOrDefault(m => m.Call_interaction_id == interactionDetails.callinteractionid);
                                        callHistory.callDuration = interactionDetails.callduration;
                                        callHistory.ringtime = interactionDetails.calltime;
                                        callHistory.isCallDurationUpdated = true;
                                        callHistory.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistory.filepath = interactionDetails.recordingpath;
                                        callHistory.callType = "Outbound";
                                        callHistory.dailedNumber = interactionDetails.dialednumber;
                                        callHistory.callStatus = true;
                                        db.callhistorycubes.AddOrUpdate(callHistory);
                                    }
                                }
                                else if (interactionDetails.callfor == 2)
                                {
                                     if (db.insurancecallhistorycubes.Count(m => m.cicallinteraction_id == interactionDetails.callinteractionid) > 0)
                                    {
                                        insurancecallhistorycube callHistoryIns = db.insurancecallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == interactionDetails.callinteractionid);
                                        callHistoryIns.callDuration = interactionDetails.callduration;
                                        callHistoryIns.ringTime = interactionDetails.calltime;
                                        callHistoryIns.isCallDurationUpdated = true;
                                        callHistoryIns.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistoryIns.filepath = interactionDetails.recordingpath;
                                        callHistoryIns.callType = "Outbound";
                                        callHistoryIns.dailedNumber = interactionDetails.dialednumber;
                                        callHistoryIns.callStatus = true;
                                        db.insurancecallhistorycubes.AddOrUpdate(callHistoryIns);
                                    }
                                }
                                else if (interactionDetails.callfor == 4)
                                {
                                     if (db.psfcallhistorycubes.Count(m => m.cicallinteraction_id == interactionDetails.callinteractionid) > 0)
                                    {
                                        psfcallhistorycube callHistoryPSF = db.psfcallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == interactionDetails.callinteractionid);

                                        callHistoryPSF.callDuration = interactionDetails.callduration;
                                        callHistoryPSF.ringTime = interactionDetails.calltime;
                                        callHistoryPSF.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistoryPSF.filepath = interactionDetails.recordingpath;
                                        callHistoryPSF.callType = "Outbound";
                                        callHistoryPSF.dailedNumber = interactionDetails.dialednumber;
                                        db.psfcallhistorycubes.AddOrUpdate(callHistoryPSF);
                                    }
                                }
                                else if (interactionDetails.callfor == 5)
                                {
                                    if (db.psfcallhistorycubes.Count(m => m.cicallinteraction_id == interactionDetails.callinteractionid) > 0)
                                    {
                                        postsalescallhistory callHistoryPSF = db.Postsalescallhistories.FirstOrDefault(m => m.cicallinteraction_id == interactionDetails.callinteractionid);

                                        callHistoryPSF.callDuration = interactionDetails.callduration;
                                        callHistoryPSF.ringTime = interactionDetails.calltime;
                                        callHistoryPSF.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistoryPSF.filepath = interactionDetails.recordingpath;
                                        callHistoryPSF.callType = "Outbound";
                                        callHistoryPSF.dailedNumber = interactionDetails.dialednumber;
                                        db.Postsalescallhistories.AddOrUpdate(callHistoryPSF);
                                    }
                                }
                                db.SaveChanges();
                            }
                            stopScheduler("knowlaritycallsynch");
                        }
                        //else
                        {
                            logger.Info("\n knowlaritycallsynch Call Synch Inactive / Not Exist / Already Running");
                        }
                    }

                }
                catch (Exception ex)
                {
                    stopScheduler("knowlaritycallsynch");
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
                    logger.Error("\n knowlaritycallsynch (Outer Loop): " + exception);
                }
            }
            logger.Info("\n knowlaritycallsynch CallSynch Stopped at: " + DateTime.Now);
        }

    }

}