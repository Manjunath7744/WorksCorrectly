using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class GetPaymentValues
    {
        public string ReceiptAmt { get; set; }
        public string Narration { get; set; }
        public string paymentId { get; set; }
        public string Engineno { get; set; }
        public string Chassisno { get; set; }
        public string Registerno { get; set; }
        public string Customername { get; set; }
        public string CustomerAddress { get; set; }
    }
    public class PostPaymentData
    {
        public List<GetPaymentValues> xmlData { get; set; }
    }
}