using Newtonsoft.Json;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class serviceCouponForInsuranceJob : schedulerCommonFunction, IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            using (AutoSherDBContext db = new AutoSherDBContext())
            {
                try
                {
                    JobDataMap dataMap = context.JobDetail.JobDataMap;
                    string data = (string)dataMap["ServiceCouponSaving"];
                    serviceCouponVM serviceCoupon = JsonConvert.DeserializeObject<serviceCouponVM>(data);

                    string baseURL = "http://vt_web.indusmis.in/Api/SavePolicyCouponData";

                    WebRequest request = WebRequest.Create(baseURL);
                    HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.MediaType = "application/json";
                    httpWebRequest.Accept = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        var bodyContent = JsonConvert.SerializeObject(serviceCoupon);
                        streamWriter.Write(bodyContent);
                        //requestbody = bodyContent;
                        logger.Info("\n\n --------Autosherpa service coupon data  --------\n" + bodyContent);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    HttpWebResponse response = null;
                    response = (HttpWebResponse)httpWebRequest.GetResponse();

                    string response_string = string.Empty;
                    using (Stream strem = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(strem);

                        response_string = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null)
                        {
                            logger.Info("\n\n -------- Autosherpa service coupon OutBlock Exception --------\n" + ex.InnerException.InnerException.Message);
                        }
                        else
                        {
                            logger.Info("\n\n -------- Autosherpa service coupon OutBlock Exception --------\n" + ex.InnerException.Message);
                        }
                    }
                    else
                    {
                        logger.Info("\n\n -------- Autosherpa service coupon OutBlock Exception --------\n" + ex.Message);
                    }

                }
            }
            logger.Info("\n\n Autosherpa service coupon Code Ended: " + DateTime.Now);
        }
    }

    public class serviceCouponVM
    {
        public string PolicyNo { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        public string PolicyIssueDate { get; set; }
        public string CouponStatus { get; set; }
        public string LoyaltyType { get; set; }
        //public long vehicleid { get; set; }
    }
}