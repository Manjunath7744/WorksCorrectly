using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class createPayUInvoiceJob : schedulerCommonFunction, IJob
    {
        //string requestbody = string.Empty;
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                responseDetailsVM responseDetails = new responseDetailsVM();

                JobDataMap dataMap = context.JobDetail.JobDataMap;
                string data = (string)dataMap["SendPayUInvoice"];
                payUTransaction payUDetails = JsonConvert.DeserializeObject<payUTransaction>(data);

                customerDetailsVM payInvoiceRequest = new customerDetailsVM();
                payInvoiceRequest.amount = payUDetails.amount.ToString();
                payInvoiceRequest.txnid = payUDetails.transaction_ID.ToString();
                payInvoiceRequest.productinfo = payUDetails.productinfo;
                
                payInvoiceRequest.firstname = payUDetails.firstname;
                payInvoiceRequest.email = payUDetails.email;
                payInvoiceRequest.phone = payUDetails.phone;
                var address1Count = payUDetails.address1;
                if (payUDetails.address1 != null && payUDetails.address1 != "" && address1Count.Count() >= 100)
                {
                    payInvoiceRequest.address1 = payUDetails.address1.Substring(0, 100);
                }
                else
                {
                    payInvoiceRequest.address1 = payUDetails.address1;
                }
                payInvoiceRequest.city = payUDetails.city;
                 payInvoiceRequest.state = payUDetails.state;
                payInvoiceRequest.country = payUDetails.country;
                payInvoiceRequest.zipcode = payUDetails.zipcode.ToString();
                payInvoiceRequest.validation_period = payUDetails.validation_period.ToString();
                payInvoiceRequest.send_email_now = payUDetails.send_email_now.ToString();
                //payUDetails.merchantkey = "g8yc8J";
                //payUDetails.merchanrtsalt = "Z0URrhnTGA96DyiIyeSWA5Koeb6OTmSq";

                var bodyContent = JsonConvert.SerializeObject(payInvoiceRequest);
                string hashkey = Generatehash512("iEWnBr|create_invoice|" + bodyContent + "|GTFIWCdXopWiciJtnhdWmVhTlB58bBb8");
                

                //string baseURL = "https://test.payu.in/merchant/postservice.php?form=2";
                string baseURL = "https://info.payu.in/merchant/postservice.php?form=2";

                WebRequest request = WebRequest.Create(baseURL);
                request.Method = "POST";
                var postdata = "key=iEWnBr&command=create_invoice&hash=" + hashkey + "&var1=" + bodyContent;
                logger.Info("\n\n --------Autosherpa PayUInvoice data request key,command,hash and var1 --------\n" + postdata);

                var fbr = Encoding.ASCII.GetBytes(postdata);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = fbr.Length;
                Stream datastream = request.GetRequestStream();
                datastream.Write(fbr, 0, fbr.Length);
                datastream.Close();

                WebResponse response = request.GetResponse();
                using (datastream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(datastream);
                    string response_from_server = reader.ReadToEnd();
                    response_from_server = response_from_server.Replace("Transaction Id", "response_transaction_id");
                    response_from_server = response_from_server.Replace("Email Id", "response_email_id");
                    responseDetails = JsonConvert.DeserializeObject<responseDetailsVM>(response_from_server);
                    payUDetails.responseparams = response_from_server;
                    logger.Info("\n\n --------Autosherpa PayUInvoice data response--------\n" + response_from_server);
                }
                using (var db = new AutoSherDBContext())
                {
                    payUDetails.updatedDate = DateTime.Now;
                    payUDetails.response_transaction_id = responseDetails.response_transaction_id;
                    payUDetails.response_email_id = responseDetails.response_email_id;
                    payUDetails.response_url = responseDetails.URL;
                    payUDetails.invoiceId = responseDetails.URL.Substring(48);
                    payUDetails.response_phone = responseDetails.Phone;
                    payUDetails.response_status = responseDetails.Status;
                    payUDetails.requestparams = postdata;
                    db.PayUTransactions.AddOrUpdate(payUDetails);
                    db.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- Autosherpa payUInvoice OutBlock Exception --------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- Autosherpa payUInvoice OutBlock Exception --------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- Autosherpa payUInvoice OutBlock Exception --------\n" + ex.Message);
                }

            }
            logger.Info("\n\n Autosherpa payUInvoice Code Ended: " + DateTime.Now);
        }

    }
    public class responseDetailsVM
    {
        public string response_transaction_id { get; set; }
        public string response_email_id { get; set; }
        public string Phone { get; set; }
        public string URL { get; set; }
        public string Status { get; set; }
    }
    public class customerDetailsVM
    {
        public string txnid { get; set; }
        public string amount { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string zipcode { get; set; }
        public string validation_period { get; set; }
        public string send_email_now { get; set; }
    }
}