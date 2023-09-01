using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoSherpa_project.Models;
using NLog;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace AutoSherpa_project.Models.PayuDataDump
{
    public class AutoMySqlToMssqlJob : IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                JobDataMap smsParameter = context.JobDetail.JobDataMap;                              
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                payUTransaction transaction = new payUTransaction();
                
                string payuDetails = dataMap.GetString("payuDetails");
                transaction = JsonConvert.DeserializeObject<payUTransaction>(payuDetails);

                   using (var db = new AutoSherDBContext())
                {

                    logger.Info("\n\n Pulling data from  Autosherpasmysql to IndusMssql " + transaction.id + "  DateTime - " + DateTime.Now);
                    
                    try
                    {
                       
                            InsertMsSqlRead(transaction);

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

                }
                logger.Info("\n\n Pulling data from autosherpaMysql to IndusMssql Ended    DateTime - " + DateTime.Now);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- AutoSMS OutBlock Exception --------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- AutoSMS OutBlock Exception --------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- AutoSMS OutBlock Exception --------\n" + ex.Message);
                }

            }
            //  logger.Info("\n\n AutoSMS Code Ended: " + DateTime.Now);
        }

        public int InsertMsSqlRead(payUTransaction transactiondetals)
        {
            logger.Info("\n\n -------- Data Extraction Started from mySQl  From Limit-{0} - To Limit {1}-------------  : ");

            string constring = @"Data Source=115.249.3.35;Initial Catalog=Auto Sherpa;uid='sa';password='sa@191*ims';Connection Timeout=6000;MultipleActiveResultSets=true";


            String InsertQuery = string.Empty;
            int rowEffect = 0;

            InsertQuery = "INSERT INTO [dbo].[payutransaction] " +
                          "([merchantkey],[merchanrtsalt],[amount],[productinfo],[firstname],[email],[address1],[city],[state],[country],[phone],[zipcode],[validation_period],[send_email_now],[updatedDate],[updatedTime],[transaction_ID],[requestparams],[responseparams],[response_transaction_id],[response_email_id],[response_phone],[response_url],[response_status],[istransactionsuccessful],[mihpayid],[request_id],[bank_ref_num],[amount_payment],[productinfo_payment],[firstname_payment],[bankcode],[udf1],[udf3],[udf4],[udf5],[field2],[field9],[error_code],[added_on],[payment_source],[card_type],[error_Message],[net_amount_debit],[disc],[mode],[PG_TYPE],[card_no],[name_on_card],[udf2],[field5],[field7],[status],[unmappedstatus],[Merchant_UTR],[Settled_at],[response_verifyPayment],[status_payment],[msg_payment],[request_verifyPayment],[fkquotationid],[invoiceId],[customerDbUpdateRequest],[customerDbUpdateResponse],[customerDbUpdateStatus],[chassisnumber],[enginno],[chassis]) " +
                          "VALUES ('" + transactiondetals.merchantkey + "', '" + transactiondetals.merchanrtsalt + "', " + transactiondetals.amount + ", '" + transactiondetals.productinfo + "', '" + transactiondetals.firstname + "', '" + transactiondetals.email + "', '" + transactiondetals.address1 + "', '" + transactiondetals.city + "', '" + transactiondetals.state + "', '" + transactiondetals.country + "', '" + transactiondetals.phone + "', " + transactiondetals.zipcode + ", " + transactiondetals.validation_period + ", " + transactiondetals.send_email_now + ", '" + transactiondetals.updatedDate.ToString() + "', '" + transactiondetals.updatedTime + "', '" + transactiondetals.transaction_ID + "', '" + transactiondetals.requestparams + "', '" + transactiondetals.responseparams + "', '" + transactiondetals.response_transaction_id + "', '" + transactiondetals.response_email_id + "', '" + transactiondetals.response_phone + "', '" + transactiondetals.response_url + "', '" + transactiondetals.response_status + "', '" + transactiondetals.istransactionsuccessful + "', '" + transactiondetals.mihpayid + "', '" + transactiondetals.request_id + "', '" + transactiondetals.bank_ref_num + "', '" + transactiondetals.amount_payment + "', '" + transactiondetals.productinfo_payment + "', '" + transactiondetals.firstname_payment + "', '" + transactiondetals.bankcode + "', '" + transactiondetals.udf1 + "', '" + transactiondetals.udf3 + "', '" + transactiondetals.udf4 + "', '" + transactiondetals.udf5 + "', '" + transactiondetals.field2 + "', '" + transactiondetals.field9 + "', '" + transactiondetals.error_code + "', '" + transactiondetals.added_on + "', '" + transactiondetals.payment_source + "', '" + transactiondetals.card_type + "', '" + transactiondetals.error_Message + "', '" + transactiondetals.net_amount_debit + "', '" + transactiondetals.disc + "', '" + transactiondetals.mode + "', '" + transactiondetals.PG_TYPE + "', '" + transactiondetals.card_no + "', '" + transactiondetals.name_on_card + "', '" + transactiondetals.udf2 + "', '" + transactiondetals.field5 + "', '" + transactiondetals.field7 + "', '" + transactiondetals.status + "', '" + transactiondetals.unmappedstatus + "', '" + transactiondetals.Merchant_UTR + "', '" + transactiondetals.Settled_at + "', '" + transactiondetals.response_verifyPayment + "', '" + transactiondetals.status_payment + "', '" + transactiondetals.msg_payment + "', '" + transactiondetals.request_verifyPayment + "', " + transactiondetals.fkquotationid + ", '" + transactiondetals.invoiceId + "', '" + transactiondetals.customerDbUpdateRequest + "', '" + transactiondetals.customerDbUpdateResponse + "', '" + transactiondetals.customerDbUpdateStatus + "', '" + transactiondetals.chassis + "' , '" + transactiondetals.enginno + "' , '" + transactiondetals.chassisnumbers + "')";

            using (SqlConnection destinationConnection = new SqlConnection(constring))
            using (var dbcm = new SqlCommand(InsertQuery, destinationConnection))
            {
                destinationConnection.Open();
                rowEffect = dbcm.ExecuteNonQuery();
            }


            return rowEffect;
        }
    }
}
