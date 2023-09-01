using AutoSherpa_project.Models.PayuDataDump;
using AutoSherpa_project.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class payUTransactionJob : schedulerCommonFunction, IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");

            logger.Info("\n payUTransaction verify_payment Job Started: " + DateTime.Now);

            if (siteRoot != "/")
            {
                try
                {
                    using (var db = new AutoSherDBContext())
                    {

                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == "payUTransactionPayment");

                        if (schedulerDetails != null && schedulerDetails.isActive == true)
                        {

                            startScheduler("payUTransactionPayment");

                            int maxLength = 20000;

                            if (schedulerDetails.datalenght != 0)
                            {
                                maxLength = schedulerDetails.datalenght;
                            }






                            responseDetailsVM responseDetailss = new responseDetailsVM();
                            transactionDetailsVM nestedtransactionDetails = new transactionDetailsVM();
                            dynamic transactionDetails = new JObject();
                            List<payUTransaction> payUTransactionDetails = new List<payUTransaction>();
                            DateTime startdate = DateTime.Today.AddDays(-5);

                            payUTransactionDetails = db.PayUTransactions.Where(m => m.istransactionsuccessful == false && m.updatedDate > startdate).ToList();
                         //  payUTransactionDetails = db.PayUTransactions.Where(m => m.transaction_ID == "txnid16374202424012023164420187").ToList();


                            foreach (var payDetails in payUTransactionDetails)
                            {
                                try
                                {
                                    //string baseURL = "https://test.payu.in/merchant/postservice.php?form=2";
                                    string baseURL = "https://info.payu.in/merchant/postservice.php?form=2";
                                    string var1 = payDetails.response_transaction_id;
                                    //var bodyContent = JsonConvert.SerializeObject(var1);
                                    //string hashkey = Generatehash512("xdvfZj|verify_payment|" + var1 + "|4irFC3YpS8ZTMoFXeIRHMaqTlGYsbBqb");
                                    string hashkey = Generatehash512("iEWnBr|verify_payment|" + var1 + "|GTFIWCdXopWiciJtnhdWmVhTlB58bBb8");

                                    WebRequest request = WebRequest.Create(baseURL);
                                    request.Method = "POST";
                                    var postdata = "key=iEWnBr&command=verify_payment&hash=" + hashkey + "&var1=" + var1;
                                    logger.Info("\n\n --------Autosherpa PayUInvoice verify_payment request key,command,hash and var1 --------\n" + postdata);

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
                                        //transactionDetails = JsonConvert.DeserializeObject(response_from_server);
                                        transactionDetails = JObject.Parse(response_from_server);


                                        var rateInfo = transactionDetails.transaction_details[payDetails.transaction_ID];

                                        nestedtransactionDetails = JsonConvert.DeserializeObject<transactionDetailsVM>(rateInfo.ToString());
                                        payDetails.response_verifyPayment = response_from_server;



                                        logger.Info("\n\n --------Autosherpa PayUInvoice verify_payment data response--------\n" + response_from_server);
                                    }

                                    payDetails.request_verifyPayment = postdata;
                                    payDetails.status_payment = transactionDetails.status;
                                    payDetails.msg_payment = transactionDetails.msg;
                                    payDetails.mihpayid = nestedtransactionDetails.mihpayid;
                                    payDetails.request_id = nestedtransactionDetails.request_id;
                                    payDetails.bank_ref_num = nestedtransactionDetails.bank_ref_num;
                                    payDetails.amount_payment = nestedtransactionDetails.amount;
                                    payDetails.productinfo_payment = nestedtransactionDetails.productinfo;
                                    payDetails.firstname_payment = nestedtransactionDetails.firstname;
                                    payDetails.bankcode = nestedtransactionDetails.bankcode;
                                    payDetails.udf1 = nestedtransactionDetails.udf1;
                                    payDetails.udf3 = nestedtransactionDetails.udf3;
                                    payDetails.udf5 = nestedtransactionDetails.udf5;
                                    payDetails.field2 = nestedtransactionDetails.field2;
                                    payDetails.field9 = nestedtransactionDetails.field9;
                                    payDetails.error_code = nestedtransactionDetails.error_code;
                                    payDetails.added_on = nestedtransactionDetails.added_on;
                                    payDetails.payment_source = nestedtransactionDetails.payment_source;
                                    payDetails.card_type = nestedtransactionDetails.card_type;
                                    payDetails.error_Message = nestedtransactionDetails.error_Message;
                                    payDetails.net_amount_debit = nestedtransactionDetails.net_amount_debit;
                                    payDetails.disc = nestedtransactionDetails.disc;
                                    payDetails.mode = nestedtransactionDetails.mode;
                                    payDetails.PG_TYPE = nestedtransactionDetails.PG_TYPE;
                                    payDetails.card_no = nestedtransactionDetails.card_no;
                                    payDetails.udf2 = nestedtransactionDetails.udf2;
                                    payDetails.field5 = nestedtransactionDetails.field5;
                                    payDetails.field7 = nestedtransactionDetails.field7;
                                    payDetails.status = nestedtransactionDetails.status;
                                    payDetails.unmappedstatus = nestedtransactionDetails.unmappedstatus;
                                    payDetails.Merchant_UTR = nestedtransactionDetails.Merchant_UTR;
                                    payDetails.Settled_at = nestedtransactionDetails.Settled_at;
                                    if (payDetails.status == "success")
                                    {
                                        payDetails.istransactionsuccessful = true;
                                    }
                                    else
                                    {
                                        payDetails.istransactionsuccessful = false;
                                    }
                                    //Sending the data to Customer based on the PayU transaction response details
                                    var customerAddress = "No Data Available";
                                    bool customerDbUpdateStatus = false;
                                    long vehicle_id = payDetails.vehicle_id;
                                    if (vehicle_id != 0)
                                    {
                                        vehicle vehicle = db.vehicles.Where(m => m.vehicle_id == vehicle_id).FirstOrDefault();
                                        long? customer_id = vehicle.customer_id;
                                        address address = db.addresses.Where(m => m.customer_Id == null).FirstOrDefault();
                                        if (address != null)
                                        {
                                            customerAddress = address.concatenatedAdress;
                                        }
                                        if (payDetails.status.ToLower() == "success" || payDetails.status.ToLower() == "failure" || payDetails.status.ToLower() == "pending")
                                        {

                                            GetPaymentValues getPaymentValues = new GetPaymentValues
                                            {
                                                ReceiptAmt = Convert.ToString(payDetails.amount) ?? "0",
                                                Narration = payDetails.unmappedstatus ?? "",
                                                paymentId = payDetails.mihpayid ?? "",
                                                Engineno = vehicle.engineNo ?? "",
                                                Chassisno = vehicle.chassis ?? "",
                                                Registerno = vehicle.vehicleRegNo ?? "",
                                                Customername = vehicle.customer.customerName ?? "",
                                                CustomerAddress = customerAddress
                                            };
                                            List<GetPaymentValues> getPaymentValueList = new List<GetPaymentValues>();
                                            //bool successStatus = false;
                                            getPaymentValueList.Add(getPaymentValues);
                                            PostPaymentData postPaymentData = new PostPaymentData
                                            {
                                                xmlData = getPaymentValueList
                                            };
                                            var response1 = await GetAfterPaymentValues(postPaymentData);
                                           
                                            customerDbUpdateStatus = response1 != null && response1.SaveInsuranceResult!=null && response1.SaveInsuranceResult ?.FirstOrDefault().Result == "sucess" ? true : false;
                                          
                                             payDetails.customerDbUpdateStatus = customerDbUpdateStatus;
                                            payDetails.customerDbUpdateRequest = Convert.ToString(JsonConvert.SerializeObject(postPaymentData));
                                            payDetails.customerDbUpdateResponse = Convert.ToString(JsonConvert.SerializeObject(response1));
                                        }
                                    }
                                    //********************** end of customer db update ******************************************

                                    db.PayUTransactions.AddOrUpdate(payDetails);
                                    db.SaveChanges();
                                  

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
                                    //payDetails.status = "Failed";
                                    db.PayUTransactions.AddOrUpdate(payDetails);
                                    db.SaveChanges();
                                }
                                finally
                                {
                                    autoInsertMssql(payDetails);
                                }

                            }
                            stopScheduler("payUTransactionPayment");

                        }

                        else
                        {
                            logger.Info("\n payUTransactionPayment Synch Inactive / Not Exist / Already Running");
                        }
                    }

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null)
                        {
                            logger.Info("\n\n -------- Autosherpa payUInvoice verify_payment OutBlock Exception --------\n" + ex.InnerException.InnerException.Message);
                        }
                        else
                        {
                            logger.Info("\n\n -------- Autosherpa payUInvoice verify_payment OutBlock Exception --------\n" + ex.InnerException.Message);
                        }
                    }
                    else
                    {
                        logger.Info("\n\n -------- Autosherpa payUInvoice verify_payment OutBlock Exception --------\n" + ex.Message);
                    }
                    logger.Info("\n\n Autosherpa payUInvoice verify_payment Code Ended: " + DateTime.Now);
                }

            }

        }


        public void autoInsertMssql(payUTransaction payuDetails)
        {

            IScheduler autoMysqlJob = StdSchedulerFactory.GetDefaultScheduler().Result;
            autoMysqlJob.Start();
            IJobDetail jobDetail = JobBuilder.Create <AutoMySqlToMssqlJob>().Build();

            string trigername = "IndusMysqlToMssqlTrigger" + DateTime.Now.Millisecond + payuDetails.id;
            string trigerGroupName = "IndusMysqlToMssqlGroup" + DateTime.Now.Millisecond + payuDetails.id;

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName).StartNow().WithSimpleSchedule().Build();

            jobDetail.JobDataMap["payuDetails"] = JsonConvert.SerializeObject(payuDetails);

            autoMysqlJob.ScheduleJob(jobDetail, trigger);

        }

        private static async Task<InsuranceSaveResponse> GetAfterPaymentValues(PostPaymentData postPaymentData)
        {
            InsuranceSaveResponse saveInsuranceResults = new InsuranceSaveResponse();
            try
            {
                               using (var client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes("MyIndus:pass*7475#"); 
                     var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;
                    HttpResponseMessage response = await client.PostAsJsonAsync(
                 "http://mobile.indusmis.in/Service1.svc/saveInsurance", postPaymentData);

                       // "http://115.249.3.40/mobileapptest1/Service1.svc/saveInsurance", postPaymentData);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var saveInsuranceResult = await response.Content.ReadAsStringAsync();
                        saveInsuranceResults = JsonConvert.DeserializeObject<InsuranceSaveResponse>(saveInsuranceResult);
                        return saveInsuranceResults;
                    }

                }
                               var abc=JsonConvert.SerializeObject(postPaymentData);
            }
            catch (Exception ex)
            {
                return saveInsuranceResults;

            }
            return saveInsuranceResults;
        }

     public class InsuranceSaveResponse
        {
            public List<SaveInsuranceResults> SaveInsuranceResult { get; set; }
        }
        public class SaveInsuranceResults
        {
            public string Result { get; set; }
            public string Rid { get; set; }
        }
        public class transactionDetailsVM
        {
            public string mihpayid { get; set; }
            public string request_id { get; set; }
            public string bank_ref_num { get; set; }
            public string amount { get; set; }
            public string productinfo { get; set; }
            public string firstname { get; set; }
            public string bankcode { get; set; }
            public string udf1 { get; set; }
            public string udf3 { get; set; }
            public string udf4 { get; set; }
            public string udf5 { get; set; }
            public string field2 { get; set; }
            public string field9 { get; set; }
            public string error_code { get; set; }
            public string added_on { get; set; }
            public string payment_source { get; set; }
            public string card_type { get; set; }
            public string error_Message { get; set; }
            public string net_amount_debit { get; set; }
            public string disc { get; set; }
            public string mode { get; set; }
            public string PG_TYPE { get; set; }
            public string card_no { get; set; }
            public string name_on_card { get; set; }
            public string udf2 { get; set; }
            public string field5 { get; set; }
            public string field7 { get; set; }
            public string status { get; set; }
            public string unmappedstatus { get; set; }
            public string Merchant_UTR { get; set; }
            public string Settled_at { get; set; }
           
        }

    }

    public class PayUTransactionRunJob
    {
        public static IScheduler payUTransactionScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            //Getting Defauls Scheduler Job
            try
            {
                //string cronSchedule = "0 0 0/4 1/1 * ? *";
                //logger.Info("\n\n --------individualReport AutoSMS Scheduler Started -------------|AT: " + cronSchedule);

                payUTransactionScheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<payUTransactionJob>().Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("payUTransactionPaymentTrigger", "payUTransactionPaymentGroup").StartNow().Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("payUTransactionPaymentTrigger", "payUTransactionPaymentGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(5).RepeatForever()).Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("individualReportcronSMSTrigger", "individualReportcronSMSGroup")
                //.StartNow().WithCronSchedule(cronSchedule).Build();

                payUTransactionScheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {


                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n --------individualReport AutoSMS Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n --------individualReport AutoSMS SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n --------individualReport AutoSMS Scheduler -------\n" + ex.Message);
                }

            }

        }
    }

}