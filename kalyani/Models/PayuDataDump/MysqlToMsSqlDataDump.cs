using MySql.Data.MySqlClient;
using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.PayuDataDump
{
    public class MysqlToMsSqlDataDumpJob
    {
        public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {

            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                //string cronSchedule = "0 0 11 1/1 * ? *";
                string cronSchedule = "0 0,45 0,18 ? * * *";
                logger.Info("\n\n -------- Cron Job MySql To MsSql {0} : ", cronSchedule);

                scheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<MysqlToMsSqlDataDump>().Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("PayuTrasactionDump", "PayuTrasactionDumpGroup").StartNow().WithCronSchedule(cronSchedule).Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("PayuTrasactionDump", "PayuTrasactionDumpGroup").StartNow().Build();
                scheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("Cron Job MySql To MsSql {0} InnerException.InnerException.Message" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("Cron Job MySql To MsSql {0} InnerException.Message" + ex.InnerException.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("Cron Job MySql To MsSql {0} Message" + ex.Message);
                }
            }
        }
    }
    public class MysqlToMsSqlDataDump : IJob
    {

        Logger logger = LogManager.GetLogger("apkRegLogger");

        public async Task Execute(IJobExecutionContext context)
        {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
           //if (siteRoot != "/")
           // {

                logger.Info("\n\n -------- Pulling data from  Autosherpasmysql to IndusMssql -------------AT  : " + DateTime.Now);
                try
                {
                    var transactionDetails = FetchTodaypayUTransactions();
                    foreach (var trasaction in transactionDetails)
                    {
                        InsertMsSqlRead(trasaction);

                    }


                    


                    logger.Info("\n\n --------  Done-------------");


                }
                catch (Exception ex)
                {
                    string exception = "";
                    if (ex.Message.Contains("inner exception"))
                    {
                        exception = ex.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.Message;
                    }
                    logger.Info("\n\n --------  Extraction Error Outer Exeption{0} -------------: ", exception);
                }
                logger.Info("\n\n -------- Pulling data from autosherpaMysql to IndusMssql Ended -------------AT  : " + DateTime.Now);

            //}
        }



            public int InsertMsSqlRead(payUTransaction transactiondetals)
            {
                logger.Info("\n\n -------- Data Extraction Started from mySQl  From Limit-{0} - To Limit {1}-------------  : ");

                string constring = @"Data Source=115.249.3.35;Initial Catalog=Auto Sherpa;uid='sa';password='sa@191*ims';Connection Timeout=6000;MultipleActiveResultSets=true";


                String InsertQuery = string.Empty;
                int rowEffect = 0;

                InsertQuery = "INSERT INTO [dbo].[payutransaction] " +
                              "([merchantkey],[merchanrtsalt],[amount],[productinfo],[firstname],[email],[address1],[city],[state],[country],[phone],[zipcode],[validation_period],[send_email_now],[updatedDate],[updatedTime],[transaction_ID],[requestparams],[responseparams],[response_transaction_id],[response_email_id],[response_phone],[response_url],[response_status],[istransactionsuccessful],[mihpayid],[request_id],[bank_ref_num],[amount_payment],[productinfo_payment],[firstname_payment],[bankcode],[udf1],[udf3],[udf4],[udf5],[field2],[field9],[error_code],[added_on],[payment_source],[card_type],[error_Message],[net_amount_debit],[disc],[mode],[PG_TYPE],[card_no],[name_on_card],[udf2],[field5],[field7],[status],[unmappedstatus],[Merchant_UTR],[Settled_at],[response_verifyPayment],[status_payment],[msg_payment],[request_verifyPayment],[fkquotationid],[invoiceId],[customerDbUpdateRequest],[customerDbUpdateResponse],[customerDbUpdateStatus],[chassisnumber],[enginno],[chassis]) " +
                              "VALUES ('" + transactiondetals.merchantkey + "', '" + transactiondetals.merchanrtsalt  + "', " + transactiondetals.amount  + ", '" + transactiondetals.productinfo + "', '" + transactiondetals.firstname + "', '" + transactiondetals.email + "', '" + transactiondetals.address1  + "', '" + transactiondetals.city + "', '" + transactiondetals.state  + "', '" + transactiondetals.country  + "', '" + transactiondetals.phone  + "', " + transactiondetals.zipcode  + ", " + transactiondetals.validation_period  + ", " + transactiondetals.send_email_now  + ", '" + transactiondetals.updatedDate.ToString() + "', '" + transactiondetals.updatedTime + "', '" + transactiondetals.transaction_ID  + "', '" + transactiondetals.requestparams  + "', '" + transactiondetals.responseparams + "', '" + transactiondetals.response_transaction_id + "', '" + transactiondetals.response_email_id + "', '" + transactiondetals.response_phone  + "', '" + transactiondetals.response_url + "', '" + transactiondetals.response_status  + "', '" + transactiondetals.istransactionsuccessful  + "', '" + transactiondetals.mihpayid  + "', '" + transactiondetals.request_id  + "', '" + transactiondetals.bank_ref_num  + "', '" + transactiondetals.amount_payment  + "', '" + transactiondetals.productinfo_payment + "', '" + transactiondetals.firstname_payment  + "', '" + transactiondetals.bankcode  + "', '" + transactiondetals.udf1  + "', '" + transactiondetals.udf3 + "', '" + transactiondetals.udf4 + "', '" + transactiondetals.udf5 + "', '" + transactiondetals.field2  + "', '" + transactiondetals.field9 + "', '" + transactiondetals.error_code  + "', '" + transactiondetals.added_on  + "', '" + transactiondetals.payment_source  + "', '" + transactiondetals.card_type  + "', '" + transactiondetals.error_Message  + "', '" + transactiondetals.net_amount_debit  + "', '" + transactiondetals.disc + "', '" + transactiondetals.mode  + "', '" + transactiondetals.PG_TYPE  + "', '" + transactiondetals.card_no  + "', '" + transactiondetals.name_on_card + "', '" + transactiondetals.udf2 + "', '" + transactiondetals.field5 + "', '" + transactiondetals.field7  + "', '" + transactiondetals.status + "', '" + transactiondetals.unmappedstatus  + "', '" + transactiondetals.Merchant_UTR  + "', '" + transactiondetals.Settled_at  + "', '" + transactiondetals.response_verifyPayment  + "', '" + transactiondetals.status_payment  + "', '" + transactiondetals.msg_payment  + "', '" + transactiondetals.request_verifyPayment + "', " + transactiondetals.fkquotationid  + ", '" + transactiondetals.invoiceId  + "', '" + transactiondetals.customerDbUpdateRequest  + "', '" + transactiondetals.customerDbUpdateResponse + "', '" + transactiondetals.customerDbUpdateStatus  + "', '" + transactiondetals.chassis + "' , '" + transactiondetals.enginno + "' , '" + transactiondetals.chassisnumbers + "')";

                using (SqlConnection destinationConnection = new SqlConnection(constring))
                using (var dbcm = new SqlCommand(InsertQuery, destinationConnection))
                {
                    destinationConnection.Open();
                    rowEffect = dbcm.ExecuteNonQuery();
                }
               

                return rowEffect;
            }
            public IList<payUTransaction> FetchTodaypayUTransactions()
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var transactionDetails = db.Database.SqlQuery<payUTransaction>("select * from payutransaction   where id in (28947,28967,28959,28948,28958,28951,28944,28718,28812,28365,28681,29099,29098,28968,29091,29092,29076,29088,28873,29077,29070,29081,29074,28882,28961,28636,29064,29191,29193,29188,29042,28986,29087,29062,29003,29040,29280,29335,29290,29328,29313,29028,29124,29026,29006,29189,29190,29013,29310,29490,29297,29245,29258,29585,29586,29672,29338,29580,29574,29059,29284,29172,29281,29704,29337,29488,29442,29613,29612,29569,29560,29571,29347,29423,29728,29591,29781,29483,29543,29308,29513,29776,29787,29766,29759,29761,29757,29751,29743,29331,29274,29581,29407,29437,29578,29577,29747)").ToList();

                    return transactionDetails;
                }


            }
        }
    }
