using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace AutoSherpa_project.Models.Schedulers
{
    public class schedulerCommonFunction
    {
        public void stopScheduler(string schedulerName)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == schedulerName);
                    schedulerDetails.IsItRunning = false;
                    db.schedulers.AddOrUpdate(schedulerDetails);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string exception = string.Empty;

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
                    exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                }

                logger.Error(schedulerName +"scheduler StatusChanging Error:\n" + exception);
            }
        }
          public void startScheduler(string schedulerName)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == schedulerName);

                    schedulerDetails.IsItRunning = true;
                    schedulerDetails.LastRun = DateTime.Now;
                    db.schedulers.AddOrUpdate(schedulerDetails);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string exception = string.Empty;

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
                    exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                }

                logger.Error(schedulerName +"scheduler StatusChanging Error:\n" + exception);
            }
        }
        public void sendEmail(long totalRecords, long discardedRecords, string dealerCode)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.Body = "Dear" + dealerCode + ",  \n Total Auto  SMS Found  - " + totalRecords + "\n Total Sent - " + (totalRecords - discardedRecords) + "\nTotal Failed - " + discardedRecords;
                mailMessage.Subject = "Auto SMS Report of " + dealerCode;
                mailMessage.From = new MailAddress("noreply@autosherpas.com");
                mailMessage.To.Add("crm@autosherpas.com");
                mailMessage.To.Add("sumathi.s@wyzmindz.com");
                mailMessage.To.Add("sriranjan@wyzmindz.com");
                smtp.Host = "smtp.zoho.com";
                smtp.Port = Convert.ToInt32(587);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
               
                smtp.Credentials = new System.Net.NetworkCredential("noreply@autosherpas.com", "Sriranjan@951");
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }

        }

        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }

        
        
    }
}