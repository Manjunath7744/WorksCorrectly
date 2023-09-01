using AutoSherpa_project.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    public class knwolarityCallRecordingAPIController : ApiController
    {
        // GET: knwolarityCallRecordingAPI
        [System.Web.Http.Route("knwolaritycallDetails/knwolarityCallRecordingAPI/get_call_Logs/")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult get_call_Logs(List<knowlaritycallrecords> callRecords)
        {
            dynamic data = new JObject();
            List<knowlarityResponse> listknwolarityResponses = new List<knowlarityResponse>();

            Logger logger = LogManager.GetLogger("apkRegLogger");
            string JsonString = string.Empty;

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (callRecords != null)
                    {
                        try
                        {
                            logger.Info("Pushing Knowlaityy Details{0} - Data {1}", DateTime.Now, JsonConvert.SerializeObject(callRecords));
                            foreach (var records in callRecords)
                            {
                                knowlarityResponse response = new knowlarityResponse();

                                logger.Info("Knowlaityy Details{0} - Data {1}", DateTime.Now, JsonConvert.SerializeObject(records));

                                try
                                {
                                        db.Knowlaritycallrecords.Add(records);
                                        db.SaveChanges();
                                    response.status = "Success";
                                    response.uuid = records.uuid;
                                    listknwolarityResponses.Add(response);
                                
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
                                    logger.Info("Knowlaityy Details{0} - Data {1}", DateTime.Now, JsonConvert.SerializeObject(records));

                                    logger.Info("Pushing Knowlarity Exception{0}", exception);
                                    response.status = "Failed";
                                    response.uuid = records.uuid;
                                    listknwolarityResponses.Add(response);
                                }
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
                            logger.Info("Pushing Knowlarity  Exception{0}", exception);
                        }
                        return Ok(listknwolarityResponses);
                    }
                    else
                    {
                        data.status = "Failed";
                        data.exception = "Incoming body is null";
                        return Ok<JObject>(data);

                    }
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
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
        }
    }

    public class knowlarityResponse
    {
        public string status;
        public string uuid;
        public string exception;
    }
}