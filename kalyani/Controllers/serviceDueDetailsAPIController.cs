using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoSherpa_project.Models;
using Newtonsoft.Json.Linq;
using NLog;
namespace AutoSherpa_project.Controllers
{

    public class serviceDueDetailsAPIController :  ApiController
    {
        #region GetServiceDueDetails
        [Route("ServiceDetailsConf/serviceDueDetailsAPI/GetServiceDueDetails")]
        [HttpPost]
        public IHttpActionResult GetServiceDueDetails([FromBody] serviceDueDetails serviceDue)
        {
            dynamic data = new JObject();
            List<masterservicedetails> serviceDueDetails = new List<masterservicedetails>();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.Masterservicedetails.Count(m => m.vehicleRegNo == serviceDue.vehicleRegNo) > 0)
                    {
                        serviceDueDetails = db.Masterservicedetails.Where(m => m.vehicleRegNo == serviceDue.vehicleRegNo).ToList();
                    }
                    if (db.Masterservicedetails.Count(m => m.PhoneNumber == serviceDue.phoneNumber || m.PhoneNumber2 == serviceDue.phoneNumber || m.phoneNumber3 == serviceDue.phoneNumber) > 0)
                    {
                        serviceDueDetails = db.Masterservicedetails.Where(m => m.PhoneNumber == serviceDue.phoneNumber || m.PhoneNumber2 == serviceDue.phoneNumber || m.phoneNumber3 == serviceDue.phoneNumber).ToList();
                    }
                }
                if (serviceDueDetails.Count > 0)
                {
                    return Ok<List<masterservicedetails>>(serviceDueDetails);
                }
                else
                {
                    data.message = "Records Not Found";
                    return Ok<JObject>(data);
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


                data.message = exception;
                return Ok<JObject>(data);
            }
        }
        #endregion

    }
}