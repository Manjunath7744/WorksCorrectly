using Newtonsoft.Json;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.API_Model;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class GSMCallSynchJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Logger logger = LogManager.GetLogger("logfile");

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    gsmcallsynchdetails gsmDetails = db.gsmcallsynch.FirstOrDefault();

                    int  limit = 0;
                    limit =gsmDetails.limit;

                    if (limit == 0)
                    {
                        limit = 100;
                    }

                    var gsmCallinter = db.gsmsynchdata.Select(m => new { callinteraction_id = m.Callinteraction_id, unique_id = m.UniqueGsmId }).OrderBy(m=>m.callinteraction_id).Skip(0).Take(limit).ToList();

                    if (gsmCallinter != null)
                    {
                        WebRequest request = WebRequest.Create(gsmDetails.gsmapi);
                        HttpWebRequest httpRequest = (HttpWebRequest)request;

                        httpRequest.Method = "POST";
                        httpRequest.ContentType = "application/json";
                        httpRequest.Accept = "application/json";

                        using (var streamWritter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            string bodyContect = JsonConvert.SerializeObject(gsmCallinter);
                            streamWritter.Write(gsmCallinter);
                            streamWritter.Flush();
                            streamWritter.Close();
                        }

                        HttpWebResponse response = null;
                        response = (HttpWebResponse)httpRequest.GetResponse();

                        string resposonse_string = string.Empty;
                        using (var stream = response.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(stream);
                            resposonse_string = sr.ReadToEnd();
                            sr.Close();
                        }

                        logger.Info("API Request Body: \n " + resposonse_string);
                        GSMRequest gSMRequest = new GSMRequest();
                        if (!string.IsNullOrEmpty(resposonse_string))
                        {
                            gSMRequest = JsonConvert.DeserializeObject<GSMRequest>(resposonse_string);
                        }

                        //logger.Info("Authorization Key: " + authorizationKey);
                        //if (request != null && request.result != null)
                        //{
                        //    logger.Info("Request Body: \n " + JsonConvert.SerializeObject(request));
                        //}
                        //else
                        //{
                        //    logger.Info("Request Body: \n " + "Request Body is Empty");
                        //}


                        //if (authorizationKey != "" && authKey == authorizationKey)
                        //{
                        using (AutoSherDBContext dBContext = new AutoSherDBContext())
                        {

                            var ListOfUniqueIDs = new List<KeyValuePair<string, string>>();
                            var ListOfUpdattedUniqueIds = new List<KeyValuePair<string, string>>();
                            GSMResponse GSMresponse = new GSMResponse();

                                //string listOfResponseStringified = " ";
                            if (gSMRequest != null && gSMRequest.result != null)
                            {
                                foreach (var item in gSMRequest.result)
                                {
                                    callinteraction condition = new callinteraction();
                                    condition = db.callinteractions.FirstOrDefault(m => m.id == item.callinteraction_id);
                                    //ListOfUniqueIDs.Add(new KeyValuePair<string, string>(item.uniqueIdForCallSync.ToString(), item.company_id));
                                    //long maxCallId = 0;
                                    //if (dBContext.callinteractions.Any(m => m.uniqueIdGSM == item.uniqueIdForCallSync))
                                    //{
                                    //    maxCallId = dBContext.callinteractions.Where(m => m.uniqueIdGSM == item.uniqueIdForCallSync).Max(m => m.id);
                                    //}


                                    //if (maxCallId != 0)
                                    //{
                                    //    condition = dBContext.callinteractions.FirstOrDefault(m => m.id == maxCallId);
                                    //}

                                    //callinteraction condition = null;
                                    if (condition != null)
                                    {
                                        callsyncdata callsyncdata = new callsyncdata();
                                        var callInterID = condition.id;
                                        var wyzID = condition.wyzUser_id;
                                        callsyncdata.uniqueidForCallSync = item.uniqueIdForCallSync.ToString();
                                        callsyncdata.callDuration = item.callDuration;
                                        callsyncdata.callDurationUpdated = true;
                                        callsyncdata.callinteraction_id = callInterID;
                                        callsyncdata.callMadeDateAndTime = Convert.ToDateTime(item.callDate.ToString("yyyy-MM-dd") + " " + item.callTime);
                                        callsyncdata.callType = item.callType;
                                        //callsyncdata.csidmap=
                                        callsyncdata.dailedNumber = item.customerPhone;
                                        callsyncdata.filepath = "/wyzAudioData/" + item.filePath;
                                        callsyncdata.isComplaintCall = false;
                                        callsyncdata.moduletype = 1;
                                        callsyncdata.ringTime = item.ringTime;
                                        callsyncdata.updatedDate = DateTime.Now;
                                        callsyncdata.wyzuser_id = wyzID;
                                        dBContext.callSyncDatas.Add(callsyncdata);
                                        dBContext.SaveChanges();
                                        ListOfUpdattedUniqueIds.Add(new KeyValuePair<string, string>(callsyncdata.uniqueidForCallSync, item.company_id));

                                        db.gsmsynchdata.Remove(db.gsmsynchdata.FirstOrDefault(m => m.Callinteraction_id == item.callinteraction_id));
                                        db.SaveChanges();

                                    }
                                }
                            }

                            List<ChassiesNoResponse> listOfResponse = new List<ChassiesNoResponse>();
                            foreach (var IDs in ListOfUpdattedUniqueIds)
                            {
                                ChassiesNoResponse response1 = new ChassiesNoResponse();
                                response1.uniqueId = IDs.Key;
                                response1.update_status = "1";
                                response1.timeStamp = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                                response1.status = null;
                                response1.company_id = IDs.Value;
                                listOfResponse.Add(response1);
                            }
                            var unSuccessfullIds = ListOfUniqueIDs.Except(ListOfUpdattedUniqueIds).ToList();
                            foreach (var IDs in unSuccessfullIds)
                            {
                                ChassiesNoResponse responseUnsuccessfull = new ChassiesNoResponse();

                                responseUnsuccessfull.uniqueId = IDs.Key;
                                responseUnsuccessfull.update_status = "0";
                                responseUnsuccessfull.timeStamp = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                                responseUnsuccessfull.status = null;
                                responseUnsuccessfull.company_id = IDs.Value;
                                listOfResponse.Add(responseUnsuccessfull);
                            }
                            //dynamic listOfResponseStringified = 


                            GSMresponse.result = new List<ChassiesNoResponse>();
                            GSMresponse.result.AddRange(listOfResponse);

                            logger.Info("API Response: \n" + JsonConvert.SerializeObject(GSMresponse));
                        }
                        //}
                        //else
                        //{
                        //    data.result = "Authentication Failed for Key" + authorizationKey;
                        //    logger.Info("Response: " + "Authentication Failed for Key: " + authorizationKey);
                        //}
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("inner exception"))
                {
                    if (ex.InnerException.Message.Contains("inner exception"))
                    {
                        //data.result = ex.InnerException.InnerException.Message;
                        logger.Error("Error" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        //data.result = ex.InnerException.Message;
                        logger.Error("Error" + ex.InnerException.Message);
                    }
                }
                else
                {
                    //data.result = ex.Message;
                    logger.Error("Error" + ex.Message);
                }
            }
        }
    }

    public class GSMCallInteractionids
    {
        public string uniqueId { get; set; }
        public long CallInteractionId { get; set; }
    }
}