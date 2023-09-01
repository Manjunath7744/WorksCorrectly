using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("payutransaction")]
    public class payUTransaction
    {
        [Key]
        public long id { get; set; }
        public long wyzUser_ID { get; set; }
        public long customer_id { get; set; }
        public long vehicle_id { get; set; }
        public string transaction_ID { get; set; }
        public string merchantkey { get; set; }
        public string merchanrtsalt { get; set; }
        public long amount { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public long zipcode { get; set; }
        public int validation_period { get; set; }
        public int send_email_now { get; set; }
        public DateTime? updatedDate { get; set; }
        public DateTime? updatedTime { get; set; }
        public bool istransactionsuccessful { get; set; }
        //response
        public string requestparams { get; set; }
        public string responseparams { get; set; }
        public string response_transaction_id { get; set; }
        public string response_email_id { get; set; }
        public string response_phone { get; set; }
        public string response_url { get; set; }
        public string response_status { get; set; }
        //payment status
        public string status_payment { get; set; }
        public string msg_payment { get; set; }
        public string mihpayid { get; set; }
        public string request_id { get; set; }
        public string bank_ref_num { get; set; }
        public string amount_payment { get; set; }
        public string productinfo_payment { get; set; }
        public string firstname_payment { get; set; }
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
        public string response_verifyPayment { get; set; }
        public string request_verifyPayment { get; set; }
        public long fkquotationid { get; set; }
        public string invoiceId { get; set; }

        //After payment
        //[Column(TypeName = "bit")]
        public bool customerDbUpdateStatus { get; set; }
        public string customerDbUpdateRequest { get; set; }
        public string customerDbUpdateResponse { get; set; }
        
        public string chassis { get; set; }
        public string enginno { get; set; }
        public string chassisnumbers { get; set; }

    }
}