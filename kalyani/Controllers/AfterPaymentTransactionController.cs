using AutoSherpa_project.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Text;
using AutoSherpa_project.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections;
using NLog;

namespace AutoSherpa_project.Controllers
{
    public class AfterPaymentTransactionController : ApiController
    {
        static HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<HttpResponseMessage> ResponceData(afterpaymenttransaction afterPayment)
        {

            Logger logger = LogManager.GetLogger("PayULog");
            try
            {
                logger.Info("AfterTransaction method invoked for transaction id " + afterPayment.Txnid);
                logger.Info("Transaction created on: " + DateTime.Now);
                var customerAddress = "No Data Available";
                if (afterPayment != null)
                {
                    using (var db = new AutoSherDBContext())
                    {
                        bool responseStatus = false;

                        long vehicle_id = db.PayUTransactions.Where(m => m.transaction_ID == afterPayment.Txnid).Select(m => m.vehicle_id).FirstOrDefault();
                        if (vehicle_id != 0)
                        {
                            vehicle vehicle = db.vehicles.Where(m => m.vehicle_id == vehicle_id).FirstOrDefault();
                            long? customer_id = vehicle.customer_id;
                            address address = db.addresses.Where(m => m.customer_Id == null).FirstOrDefault();
                            if (address != null)
                            {
                                customerAddress = address.concatenatedAdress;
                            }
                            GetPaymentValues getPaymentValues = new GetPaymentValues
                            {
                                ReceiptAmt = afterPayment.Amount,
                                Narration = afterPayment.Unmappedstatus,
                                paymentId = afterPayment.Mihpayid,
                                Engineno = vehicle.engineNo,
                                Chassisno = vehicle.chassis,
                                Registerno = vehicle.vehicleRegNo,
                                Customername = vehicle.customer.customerName,
                                CustomerAddress = customerAddress
                            };

                            //responseStatus = await GetAfterPaymentValues(getPaymentValues);

                        }
                        afterPayment.IsClientDbUpdated = true;
                        //afterPayment.ErrorCount = responseStatus ? 0 : 1;
                        afterPayment.CreatedOn = DateTime.Now;

                        db.afterpaymenttransaction.Add(afterPayment);
                        db.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.OK, "Success");
                        logger.Info("AfterTransaction method completed " + DateTime.Now);
                        return message;
                    }
                }
                else
                {
                    logger.Info("afterPayment details are empty in request body");
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured while executing ResponceData method ", ex.ToString()); ;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //static async Task<bool> GetAfterPaymentValues(GetPaymentValues getPaymentValues)
        //{
        //    List<GetPaymentValues> getPaymentValueList = new List<GetPaymentValues>();
        //    bool successStatus = false;
        //    getPaymentValueList.Add(getPaymentValues);
        //    PostPaymentData postPaymentData = new PostPaymentData
        //    {
        //        xmlData = getPaymentValueList
        //    };
        //    using (var client = new HttpClient())
        //    {
        //        var byteArray = Encoding.ASCII.GetBytes("MyIndus:pass*7475#");
        //        var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        //        client.DefaultRequestHeaders.Authorization = header;
        //        HttpResponseMessage response = await client.PostAsJsonAsync(
        //            "http://115.249.3.40/mobileapptest1/Service1.svc/saveInsurance", postPaymentData);
        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            successStatus = true;
        //        }
        //    }

        //    return successStatus;

        //}
    }
}
