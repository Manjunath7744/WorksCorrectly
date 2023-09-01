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

namespace AutoSherpa_project.Models.Schedulers
{
    public class tatateleservicesScheduler : schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;

            logger.Info("\n tatateleservice CallSynch Started at: " + DateTime.Now);
            if (siteRoot != "/")
            {
                try
                {
                    using (var db = new AutoSherDBContext())
                    {
                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == "tatateleservicecallsynch");

                        if (schedulerDetails != null && schedulerDetails.isActive == true && schedulerDetails.IsItRunning == false)
                        {

                            startScheduler("tatateleservicecallsynch");
                            logger.Info("\n tatateleservice Call Synch Entered --> DateTime: " + DateTime.Now);

                            tatateleservicescredintials apiDetails = db.Tatateleservicescredintials.FirstOrDefault(m => m.apiType == "calllogs");

                            string baseURL = string.Empty;
                            string Authorization = apiDetails.authorizationkey;

                            dynamic requestbody = new JObject();

                            var callinteractionDetails = db.Database.SqlQuery<gsmsynchdata>("select  * from gsmsynchdata where TenantUrl='tatateleservices' and date(CallMadeDateTime) = curdate() and length(UniqueGsmId)>1 and Callinteraction_id not in(select callinteractionId  from tatateleservicescallrecords);").ToList();

                            foreach (var interactionDetails in callinteractionDetails)
                            {
                                baseURL = apiDetails.apiurl + interactionDetails.UniqueGsmId;
                                WebRequest request = WebRequest.Create(baseURL);
                                var httprequest = (HttpWebRequest)request;

                                httprequest.PreAuthenticate = true;
                                httprequest.Method = "GET";
                                httprequest.ContentType = "application/json";
                                httprequest.Headers["Authorization"] = Authorization;
                                httprequest.Accept = "application/json";


                                HttpWebResponse response = null;
                                response = (HttpWebResponse)httprequest.GetResponse();

                                string response_string = string.Empty;
                                using (Stream strem = response.GetResponseStream())
                                {
                                    StreamReader sr = new StreamReader(strem);
                                    response_string = sr.ReadToEnd();
                                    sr.Close();
                                }
                                dynamic api_result = JObject.Parse(response_string);
                                var callrecords = JsonConvert.SerializeObject(api_result.results);
                                callrecords = callrecords.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                                tatateleservicescallrecords recordDetails = JsonConvert.DeserializeObject<tatateleservicescallrecords>(callrecords);
                                recordDetails.callinteractionId = interactionDetails.Callinteraction_id;
                                recordDetails.callFor = interactionDetails.callFor;
                                db.Tatateleservicescallrecords.Add(recordDetails);

                                if (recordDetails.callFor == 1)
                                {
                                    if (db.callhistorycubes.Count(m => m.Call_interaction_id == interactionDetails.Callinteraction_id) > 0)
                                    {
                                        callhistorycube callHistory = db.callhistorycubes.FirstOrDefault(m => m.Call_interaction_id == interactionDetails.Callinteraction_id);
                                        callHistory.callDuration = recordDetails.call_duration;
                                        callHistory.ringtime = recordDetails.time;
                                        callHistory.isCallDurationUpdated = true;
                                        callHistory.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistory.filepath = recordDetails.recording_url;
                                        callHistory.callType = recordDetails.direction;
                                        callHistory.dailedNumber = recordDetails.client_number;
                                        callHistory.callStatus = true;
                                        db.callhistorycubes.AddOrUpdate(callHistory);
                                    }
                                }
                                else if (recordDetails.callFor == 2)
                                {
                                    if (db.insurancecallhistorycubes.Count(m => m.cicallinteraction_id == interactionDetails.Callinteraction_id) > 0)
                                    {
                                        insurancecallhistorycube callHistoryIns = db.insurancecallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == interactionDetails.Callinteraction_id);
                                        callHistoryIns.callDuration = recordDetails.call_duration;
                                        callHistoryIns.ringTime = recordDetails.time;
                                        callHistoryIns.isCallDurationUpdated = true;
                                        callHistoryIns.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistoryIns.filepath = recordDetails.recording_url;
                                        callHistoryIns.callType = recordDetails.direction;
                                        callHistoryIns.dailedNumber = recordDetails.client_number;
                                        callHistoryIns.callStatus = true;
                                        db.insurancecallhistorycubes.AddOrUpdate(callHistoryIns);
                                    }
                                }
                                else if (recordDetails.callFor == 4)
                                {
                                    if (db.psfcallhistorycubes.Count(m => m.cicallinteraction_id == interactionDetails.Callinteraction_id) > 0)
                                    {
                                        psfcallhistorycube callHistoryPSF = db.psfcallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == interactionDetails.Callinteraction_id);

                                        callHistoryPSF.callDuration = recordDetails.call_duration;
                                        callHistoryPSF.ringTime = recordDetails.time;
                                        callHistoryPSF.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistoryPSF.filepath = recordDetails.recording_url;
                                        callHistoryPSF.callType = recordDetails.direction;
                                        callHistoryPSF.dailedNumber = recordDetails.client_number;
                                        db.psfcallhistorycubes.AddOrUpdate(callHistoryPSF);
                                    }
                                }
                                else if (recordDetails.callFor == 5)
                                {
                                    if (db.psfcallhistorycubes.Count(m => m.cicallinteraction_id == interactionDetails.Callinteraction_id) > 0)
                                    {
                                        postsalescallhistory callHistoryPSF = db.Postsalescallhistories.FirstOrDefault(m => m.cicallinteraction_id == interactionDetails.Callinteraction_id);

                                        callHistoryPSF.callDuration = recordDetails.call_duration;
                                        callHistoryPSF.ringTime = recordDetails.time;
                                        callHistoryPSF.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
                                        callHistoryPSF.filepath = recordDetails.recording_url;
                                        callHistoryPSF.callType = recordDetails.direction;
                                        callHistoryPSF.dailedNumber = recordDetails.client_number;
                                        db.Postsalescallhistories.AddOrUpdate(callHistoryPSF);
                                    }
                                }
                                db.SaveChanges();
                            }
                            stopScheduler("tatateleservicecallsynch");
                        }
                        else
                        {
                            logger.Info("\n tatateleservice Call Synch Inactive / Not Exist / Already Running");
                        }
                    }

                }
                catch (Exception ex)
                {
                    stopScheduler("tatateleservicecallsynch");
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
            logger.Info("\n tatateleservice CallSynch Stopped at: " + DateTime.Now);
        }

    }

}