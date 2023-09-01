using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class EventsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");

            logger.Info("\n Events Status Cron Job Started: " + DateTime.Now);

            if (siteRoot != "/")
            {
                try
                {
                    using (var db = new AutoSherDBContext())
                    {
                        dealer dealerDetails = db.dealers.FirstOrDefault();

                        schedulers scheduler = db.schedulers.FirstOrDefault(m => m.scheduler_name == "Events");

                        if (scheduler != null && scheduler.isActive == true)
                        {
                            List<eventstaus> todayEventList = db.Database.SqlQuery<eventstaus>("select * from eventstaus where  EventStartdate >= date_sub( current_date() ,interval 11 hour);").ToList();

                            if (todayEventList != null && todayEventList.Count() > 0)
                            {
                                EmailSender emailSender = db.emailSender.FirstOrDefault(m=>m.IsActive==true);
                                if (emailSender != null)
                                {
                                    List<EmailReceipient> emailReceipients = db.emailReceipient.ToList();
                                    if (emailReceipients != null && emailReceipients.Count() > 0)
                                    {

                                        string htmlMailDiv = "<div>" +
                                                        "<style>" +
                                                            "table{border-collapse: collapse;margin-top: 25px;margin-left: 8px;}" +
                                                            "tr,td,th{border: 1px solid black;padding: 5px 8px;}" +
                                                        "</style>" +
                                                        "<h4>Dear Team</h4>" +
                                                        "<span>Below table shows the <b>"+dealerDetails.dealerCode+" event status</b> with names, If event status is <span style='color: red; font-weight: bold; font - family: consolas;'>Failure</span> , please reach-out product team.</span>" +
                                                               "<table>" +
                                                                   "<thead>" +
                                                                       "<tr>" +
                                                                           "<th>Sl.</th>" +
                                                                           "<th>Event Name</th>" +
                                                                           "<th>Event Status</th>" +
                                                                       "</tr>" +
                                                                   "</thead>" +
                                                                   "<tbody>" +
                                                                    "THE_TABLE_BODY" +
                                                                   "</tbody>" +
                                                               "</table>" +
                                                               "<div style='margin-top:28px;'>" +
                                                    "<span style='font-size: 12.5pt;font-weight: bold;'>Regards</span><br/><span>Product Team</span></div>"+
                                                           "</div>", TableBody = "";
                                        int i = 1;
                                        foreach (var events in todayEventList)
                                        {
                                            try
                                            {

                                                string EventStatus = "", EventStatusHTML = "";

                                                string eachRow = null;

                                                eachRow = "<tr><td>" + i + "</td><td>" + events.Eventname + "</td>";

                                                if (string.IsNullOrEmpty(events.Error_status))
                                                {
                                                    eachRow = eachRow + "<td><span style='color:green;font-weight:550;'>Success</span></td>";
                                                }
                                                else
                                                {
                                                    eachRow = eachRow + "<td><span style='color:red;font-weight:550;'>Failed</span></td>";
                                                }

                                                eachRow = eachRow + "</tr>";
                                                TableBody = TableBody + eachRow;
                                                i += 1;

                                            }
                                            catch (Exception ex)
                                            {
                                                string exception;
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
                                            }
                                        }

                                        htmlMailDiv= htmlMailDiv.Replace("THE_TABLE_BODY", TableBody);
                                        foreach (var receiver in emailReceipients)
                                        {
                                            EventEmailInteraction Interaction = new EventEmailInteraction();
                                            try
                                            {

                                                using (MailMessage mailMessage = new MailMessage())
                                                {
                                                    List<string> Emails = new List<string>();
                                                    if (receiver.ToEmailAddress.Contains(';'))
                                                    {
                                                        Emails = receiver.ToEmailAddress.Split(';').ToList();
                                                        foreach (var email in Emails)
                                                        {
                                                            if (!string.IsNullOrEmpty(email))
                                                            {
                                                                mailMessage.To.Add(email);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mailMessage.To.Add(receiver.ToEmailAddress);
                                                    }

                                                    if (receiver.CCEmailAddress.Contains(';'))
                                                    {
                                                        Emails = receiver.CCEmailAddress.Split(';').ToList();
                                                        foreach (var email in Emails)
                                                        {
                                                            if (string.IsNullOrEmpty(email))
                                                            {
                                                                mailMessage.CC.Add(email);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!string.IsNullOrEmpty(receiver.CCEmailAddress))
                                                        {
                                                            mailMessage.CC.Add(receiver.CCEmailAddress);
                                                        }

                                                    }
                                                    mailMessage.IsBodyHtml = true;
                                                    mailMessage.Subject = dealerDetails.dealerCode + " events running status -" + DateTime.Now.ToString("dd-MM-yyyy");
                                                    mailMessage.Body = htmlMailDiv;
                                                    mailMessage.From = new MailAddress(emailSender.UserEmail);

                                                    Interaction.SentDate = DateTime.Now;
                                                    Interaction.SentTime = DateTime.Now.ToString("dd-MM-yyyy");
                                                    Interaction.Subject = mailMessage.Subject;
                                                    Interaction.Body = mailMessage.Body;
                                                    Interaction.ToEmailAddress = receiver.ToEmailAddress;
                                                    Interaction.FromEmailAddress = emailSender.UserEmail;
                                                    Interaction.CCEmailAddress = receiver.CCEmailAddress;

                                                    using (SmtpClient smtp = new SmtpClient())
                                                    {
                                                        smtp.Host = emailSender.HostAPI;
                                                        smtp.Port = Convert.ToInt32(emailSender.PortNumber);
                                                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                        smtp.UseDefaultCredentials = true;
                                                        smtp.Credentials = new System.Net.NetworkCredential(emailSender.UserEmail, emailSender.UserPassword);
                                                        smtp.EnableSsl = true;
                                                        smtp.Send(mailMessage);

                                                        Interaction.GateWayResponse = "Sent Successfully";
                                                        Interaction.SentStatus = true;
                                                    }

                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                string exception;
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
                                                Interaction.GateWayResponse = exception;
                                                Interaction.SentStatus = false;
                                            }
                                            db.eventEmailInteraction.Add(Interaction);
                                            db.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        logger.Info("\n Events Job: No Email_receipient Found... aborted");
                                    }
                                }
                                else
                                {
                                    logger.Info("\n Events Job: No EmailSender Found... aborted");
                                }
                                //List<EmailReceipient>
                            }
                        }
                        else
                        {
                            logger.Info("\n Events Job Aborted because of empty or job was disabled...");
                        }
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


                    logger.Info("\n Events outer try exception: \n" + exception);
                }
            }
            logger.Info("\n Events Job: Finished.....");
        }
    }
}