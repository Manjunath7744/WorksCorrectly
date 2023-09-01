using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.API_Model;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using NLog;

namespace AutoSherpa_project.Controllers
{
    public class ValuesController : ApiController
    {  

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost, Route("api/values/getUniqueId")]
        public IHttpActionResult getNotSynchUniqueId(string id)
        {
            //List<GSMCallInteractionids> callInterUniqueId = new List<GSMCallInteractionids>();
            //dynamic data = new JObject();
            //Logger logger = LogManager.GetLogger("logfile");
            //try
            //{
            //    using (var db = new AutoSherDBContext())
            //    {
            //        string sql = string.Empty;
            //        gsmcallsynch gsmDate = db.gsmcallsynch.FirstOrDefault();

            //        if (gsmDate != null && gsmDate.status == true)
            //        {
            //            sql = @"select id as CallInteractionId,uniqueIdGSM as uniqueId from callinteraction where (date(callMadeDateAndTime)=current_date() || date(callMadeDateAndTime)='" + Convert.ToDateTime(gsmDate.synchdate).ToString("yyyy-MM-dd") + "') and uniqueIdGSM not in (select uniqueidForCallSync from callsyncdata) limit 0," + gsmDate.limit + ";";
            //        }
            //        else
            //        {
            //            sql = @"select id as CallInteractionId,uniqueIdGSM as uniqueId from callinteraction where date(callMadeDateAndTime)=current_date() and uniqueIdGSM not in (select uniqueidForCallSync from callsyncdata) limit 0,100;";
            //        }
            //        //List<string> uniueIdInCallSynch = db.callSyncDatas.Select(m => m.uniqueidForCallSync).ToList();
            //        DateTime curDate = DateTime.Now.Date;


            //        callInterUniqueId = db.Database.SqlQuery<GSMCallInteractionids>(sql).ToList();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("inner exception"))
            //    {
            //        if (ex.InnerException.Message.Contains("inner exception"))
            //        {
            //            data.result = ex.InnerException.InnerException.Message;
            //            logger.Error("Error" + ex.InnerException.InnerException.Message);
            //        }
            //        else
            //        {
            //            data.result = ex.InnerException.Message;
            //            logger.Error("Error" + ex.InnerException.Message);
            //        }
            //    }
            //    else
            //    {
            //        data.result = ex.Message;
            //        logger.Error("Error" + ex.Message);
            //    }
            //    return Ok<JObject>(data);
            //}

            return Ok<string>("Hello123");
        }


        // POST api/values
        public IHttpActionResult Post([FromBody]GSMRequest request)
        {
            string authKey = "xNdmsApiKey="+"c81bbc41f0995cef1733767ac938d046";
            
            dynamic data = new JObject();
            Logger logger = LogManager.GetLogger("logfileGSM");
            GSMResponse GSMresponse = new GSMResponse();
            try
            {
                string authorizationKey = "";

                if(Request.Headers.Contains("Authorization"))
                {
                    authorizationKey = Request.Headers.GetValues("Authorization").FirstOrDefault();
                }
                
                if(authorizationKey.Contains("+\""))
                {
                    authorizationKey = authorizationKey.Split('"')[2];
                }

                List<ChassiesNo> chass = null;

                if(request!=null && request.result!=null)
                {
                    chass = request.result;
                }

                logger.Info("Authorization Key: " + authorizationKey);
                if(request != null && request.result!=null)
                {
                    logger.Info("Request Body: \n " + JsonConvert.SerializeObject(request));
                }
                else
                {
                    logger.Info("Request Body: \n " + "Request Body is Empty");
                }
                

                if (authorizationKey!="" && authKey== authorizationKey)
                {
                    using (AutoSherDBContext dBContext = new AutoSherDBContext())
                    {

                        var ListOfUniqueIDs = new List<KeyValuePair<string, string>>();
                        var ListOfUpdattedUniqueIds = new List<KeyValuePair<string, string>>();
                        
                        
                        //string listOfResponseStringified = " ";
                        if(request!=null && request.result!=null)
                        {
                            foreach (var item in chass)
                            {
                                callinteraction condition = new callinteraction();
                                ListOfUniqueIDs.Add(new KeyValuePair<string, string>(item.uniqueIdForCallSync.ToString(), item.company_id));
                                long maxCallId = 0;

                                //if (dBContext.callinteractions.Any(m => m.uniqueIdGSM == item.uniqueIdForCallSync))
                                //{
                                //    maxCallId = dBContext.callinteractions.Where(m => m.uniqueIdGSM == item.uniqueIdForCallSync).Max(m => m.id);
                                //}

                                gsmsynchdata gsm = new gsmsynchdata();
                                
                                //if(dBContext.gsmsynchdata.Any(m=>m.UniqueGsmId==item.uniqueIdForCallSync))
                                //{
                                    gsm = dBContext.gsmsynchdata.FirstOrDefault(m => m.UniqueGsmId == item.uniqueIdForCallSync);

                                //}


                                if (gsm != null)
                                {
                                    maxCallId = gsm.Callinteraction_id;
                                    condition = dBContext.callinteractions.FirstOrDefault(m => m.id == maxCallId);
                                }
                                //else
                                //{
                                //    if (dBContext.callSyncDatas.Any(m => m.uniqueidForCallSync == item.uniqueIdForCallSync))
                                //    {
                                //        ListOfUpdattedUniqueIds.Add(new KeyValuePair<string, string>(item.uniqueIdForCallSync, item.company_id));
                                //    }
                                //}
                                //callinteraction condition = null;
                                if (condition != null && maxCallId!=0)
                                {
                                    callsyncdata callsyncdata = new callsyncdata();
                                    var callInterID = condition.id;
                                    var wyzID = condition.wyzUser_id;
                                    callsyncdata.uniqueidForCallSync = item.uniqueIdForCallSync;
                                    callsyncdata.callDuration = item.callDuration;
                                    callsyncdata.callDurationUpdated = true;
                                    callsyncdata.callinteraction_id = callInterID;
                                    callsyncdata.callMadeDateAndTime = Convert.ToDateTime(item.callDate.ToString("yyyy-MM-dd") + " " + item.callTime);
                                    callsyncdata.callType = item.callType;
                                    //callsyncdata.csidmap=
                                    callsyncdata.dailedNumber = item.customerPhone;
                                    callsyncdata.filepath = "http://" + gsm.TenantUrl + item.filePath;
                                    callsyncdata.isComplaintCall = false;
                                    callsyncdata.moduletype = 1;
                                    callsyncdata.ringTime = item.ringTime;
                                    callsyncdata.updatedDate = DateTime.Now;
                                    callsyncdata.wyzuser_id = wyzID;
                                    callsyncdata.isgsmsdata = true;
                                    dBContext.callSyncDatas.Add(callsyncdata);
                                    dBContext.SaveChanges();
                                    ListOfUpdattedUniqueIds.Add(new KeyValuePair<string, string>(callsyncdata.uniqueidForCallSync, item.company_id));

                                    dBContext.gsmsynchdata.Remove(gsm);
                                    dBContext.SaveChanges();
                                    //if (!dBContext.callSyncDatas.Any(m=>m.uniqueidForCallSync==item.uniqueIdForCallSync))
                                    //{
                                        
                                    //}
                                    //else
                                    //{
                                    //    ListOfUpdattedUniqueIds.Add(new KeyValuePair<string, string>(item.uniqueIdForCallSync, item.company_id));
                                    //    dBContext.gsmsynchdata.Remove(gsm);
                                    //    dBContext.SaveChanges();
                                    //}
                                }
                            }
                        }
                        
                        
                       
                        List<ChassiesNoResponse> listOfResponse = new List<ChassiesNoResponse>();
                        foreach (var IDs in ListOfUpdattedUniqueIds)
                        {
                            ChassiesNoResponse response = new ChassiesNoResponse();
                            response.uniqueId = IDs.Key;
                            response.update_status = "1";
                            response.timeStamp = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                            response.status = null;
                            response.company_id = IDs.Value;
                            listOfResponse.Add(response);
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

                        logger.Info("Response: \n"+ JsonConvert.SerializeObject(GSMresponse));
                    }
                }
                else
                {
                    data.result = "Authentication Failed for Key"+authorizationKey;
                    logger.Info("Response: "+"Authentication Failed for Key: " + authorizationKey);
                }
            }
            catch(Exception ex)
            {
                if(ex.Message.Contains("inner exception"))
                {
                    if(ex.InnerException.Message.Contains("inner exception"))
                    {
                        data.result = ex.InnerException.InnerException.Message;
                        logger.Error("Error" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        data.result = ex.InnerException.Message;
                        logger.Error("Error" + ex.InnerException.Message);
                    }
                }
                else
                {
                    data.result = ex.Message;
                    logger.Error("Error" + ex.Message);
                }
                return Ok<JObject>(data);
            }
            
            return Ok<GSMResponse>(GSMresponse);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

    }
}

        //using (AutoSherDBContext dbContext = new AutoSherDBContext())
        //{
        //  List<ChassiesNoResponse> listOfResponse = new List<ChassiesNoResponse>();
        //    List<ChassiesNoResponse> listOfUnsuccessfullResponse = new List<ChassiesNoResponse>();
        //    callhistorycube callhistorycubes = new callhistorycube();
        //    callinteraction callinteractions = new callinteraction();
        //    insurancecallhistorycube insurancecallhistorycubes = new insurancecallhistorycube();
        //    psfcallhistorycube psfcallhistorycubes = new psfcallhistorycube();
        //    //https://stackoverflow.com/questions/21404734/how-to-add-and-get-header-values-in-webapi
        //    HttpRequestMessage req = new HttpRequestMessage();
        //    var headers = req.Headers;

//    var ListOfUniqueIDs = new List<KeyValuePair<long, string>>();
//    var ListOfUpdattedUniqueIds = new List<KeyValuePair<long, string>>();
//    string listOfResponseStringified = " ";


//    foreach (var item in chass)
//    {
//        {
//            ListOfUniqueIDs.Add(new KeyValuePair<long, string>(item.uniqueIdForCallSync, "forRefOfKeyVal"));
//        }
//    }

//    //if (headers.Contains("StaticKey"))
//    if(true)
//    {
//        foreach(var chassiNo in chass)
//        {
//            long interactionId = 0;
//            var condition = dbContext.callinteractions.FirstOrDefault(m => m.uniqueIdGSM == chassiNo.uniqueIdForCallSync);
//            if (condition!=null)
//            {
//                interactionId = dbContext.callinteractions.FirstOrDefault(m => m.uniqueIdGSM == chassiNo.uniqueIdForCallSync).id;
//                if (interactionId != 0)
//                {
//                    callinteractions = condition;
//                    callinteractions.agentName = chassiNo.agentName;
//                    callinteractions.callType = chassiNo.callType;
//                    callinteractions.dailedNoIs = chassiNo.customerPhone;
//                    callinteractions.callDate = chassiNo.callDate.ToString();
//                    callinteractions.callDuration = chassiNo.callDuration;
//                    callinteractions.callTime = chassiNo.callTime;
//                    callinteractions.filePath = chassiNo.filePath;
//                    callinteractions.ringTime = chassiNo.ringTime;
//                    //interactionId = callinteractions.id;
//                    ListOfUpdattedUniqueIds.Add(new KeyValuePair<long, string>(chassiNo.uniqueIdForCallSync, "forRefOfKeyVal"));
//                    dbContext.SaveChanges();

//                    //var conditionOne = dbContext.callhistorycubes.Where(m => m.Call_interaction_id == interactionId).FirstOrDefault<callhistorycube>();
//                    var conditionOne = dbContext.callhistorycubes.FirstOrDefault(m => m.Call_interaction_id == interactionId);
//                    if (conditionOne != null) //Call_interaction_id)
//                    {
//                        //agent name
//                        callhistorycubes = conditionOne;
//                        callhistorycubes.callType = chassiNo.callType;
//                        callhistorycubes.dailedNumber = chassiNo.customerPhone;
//                        callhistorycubes.callDate = chassiNo.callDate;
//                        callhistorycubes.callDuration = chassiNo.callDuration;
//                        callhistorycubes.callTime
//                            = chassiNo.callTime;
//                        callhistorycubes.filepath = chassiNo.filePath;
//                        callhistorycubes.ringtime = chassiNo.ringTime;
//                        dbContext.SaveChanges();
//                    }
//                    else if (interactionId == insurancecallhistorycubes.cicallinteraction_id)
//                    {
//                        insurancecallhistorycubes.callType = chassiNo.callType;
//                        insurancecallhistorycubes.dailedNumber = chassiNo.customerPhone;
//                        insurancecallhistorycubes.callDate = chassiNo.callDate;
//                        insurancecallhistorycubes.callDuration = chassiNo.callDuration;
//                        insurancecallhistorycubes.callTime = Convert.ToDateTime(chassiNo.callTime).TimeOfDay;
//                        insurancecallhistorycubes.filepath = chassiNo.filePath;
//                        insurancecallhistorycubes.ringTime = chassiNo.ringTime;
//                        dbContext.SaveChanges();
//                    }
//                    else if (interactionId == psfcallhistorycubes.cicallinteraction_id)
//                    {
//                        psfcallhistorycubes.callType = chassiNo.callType;
//                        psfcallhistorycubes.dailedNumber = chassiNo.customerPhone;
//                        psfcallhistorycubes.callDate = chassiNo.callDate;
//                        psfcallhistorycubes.callDuration = chassiNo.callDuration;
//                        psfcallhistorycubes.callTime = chassiNo.callTime;
//                        psfcallhistorycubes.filepath = chassiNo.filePath;
//                        psfcallhistorycubes.ringTime = chassiNo.ringTime;
//                        dbContext.SaveChanges();
//                    }
//                    //dbContext.SaveChanges();

//                   ChassiesNoResponse response = new ChassiesNoResponse();
//                    ChassiesNoResponse responseUnsuccessfull = new ChassiesNoResponse();
//               foreach (var item in ListOfUpdattedUniqueIds)
//                    {
//                        response.uniqueId = item.Key;
//                        response.update_status = 1;
//                        response.timeStamp = DateTime.Now;
//                        response.status = null;
//                        listOfResponse.Add(response);
//                    }
//                    var unSuccessfullIds = ListOfUniqueIDs.Except(ListOfUpdattedUniqueIds).ToList();
//                    foreach (var item in unSuccessfullIds)
//                    {
//                        responseUnsuccessfull.uniqueId = item.Key;
//                        responseUnsuccessfull.update_status = 0;
//                        responseUnsuccessfull.timeStamp = DateTime.Now;
//                        responseUnsuccessfull.status = null;
//                        listOfResponse.Add(responseUnsuccessfull);
//                    }
//                    listOfResponseStringified = JsonConvert.SerializeObject(listOfResponse);
//                }
//            }


//            // check this if (chassiNo.uniqueIdForCallSync == callinteractions.uniqueIdGSM!=null) {
//            }
//        }

//    return Ok<string>(listOfResponseStringified);
//}

////}
