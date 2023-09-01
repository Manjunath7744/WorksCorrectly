using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using NLog;
using Quartz;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using AutoSherpa_project.Models.ViewModels;
using System.IO;
using Newtonsoft.Json;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class KatariaAutoEmail : IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                long cusID, userId, vehID, calldispositionid, emailTemplateId, workshopId;
                string notrequiredreason, DealerCode, innerRequiredReason, response_string = string.Empty, fromEmail = string.Empty;
                string toEmailAddress = string.Empty, subject = string.Empty, fromEmailAddress = string.Empty, emailBody = string.Empty, email_CC = string.Empty, baseURL = string.Empty, apiKey = string.Empty; ;
                int role;
                bool isSendBlueAPI = false;

                JobDataMap dataMap = context.JobDetail.JobDataMap;

                userId = dataMap.GetLong("WyzId");
                vehID = dataMap.GetLong("vehicleId");
                cusID = dataMap.GetLong("custId");
                calldispositionid = dataMap.GetLong("calldispositionid");
                notrequiredreason = dataMap.GetString("notrequiredreason");
                DealerCode = dataMap.GetString("DealerCode");
                innerRequiredReason = dataMap.GetString("innerRequiredReason");
                fromEmail = dataMap.GetString("fromEmail");
                role = dataMap.GetInt("role");
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    emailinteraction emailinter = new emailinteraction();

                    try
                    {
                        
                        //if (string.IsNullOrEmpty(fromEmail))
                        //{
                        //    //fromEmail = "noreply@katariaservice.in";
                        //}
                        if (!(string.IsNullOrEmpty(notrequiredreason)))
                        {
                            string dispoReason = notrequiredreason.Trim();
                            if (!(string.IsNullOrEmpty(innerRequiredReason)))
                            {
                                dispoReason = innerRequiredReason.Trim();
                            }
                            if (db.calldispositiondatas.Count(m => m.disposition == dispoReason) > 0)
                            {
                                calldispositionid = db.calldispositiondatas.FirstOrDefault(m => m.disposition == dispoReason).dispositionId;
                            }
                            else
                            {
                                response_string = "disposition not present in callDisposition table";
                            }

                        }

                        emailinter.wyzUser_id = userId;
                        emailinter.customer_id = cusID;
                        emailinter.vehicle_id = vehID;
                        emailinter.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                        emailinter.interactionDateAndTime = DateTime.Now;
                        emailinter.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                        int emailCredentialCount = db.emailcredentials.Count(m => m.isdefaultemail == true);
                        int userEmailCount = db.emails.Count(m => m.customer_id == cusID && m.isPreferredEmail == true);


                        if (emailCredentialCount != 0 && userEmailCount != 0)
                        {
                            var callDispositionDetails = db.calldispotitionautotemplates.FirstOrDefault(m => m.dispositionid_fk == calldispositionid && (m.roletype == role || m.roletype == 0));
                            if (callDispositionDetails != null && callDispositionDetails.isenabledemailtemplate && callDispositionDetails.emailtemplateid != 0)
                            {
                               
                                if ((role == 1) && (innerRequiredReason == "Dissatisfied with Last Service"))
                                {
                                    emailTemplateId = 5;
                                }
                                else
                                {
                                    emailTemplateId = callDispositionDetails.emailtemplateid;
                                }
                                emailinter.emailType = emailTemplateId.ToString();
                                var emailCredentials = db.emailcredentials.FirstOrDefault(m => m.isdefaultemail == true);
                                var wyzuserDetails = db.wyzusers.FirstOrDefault(m => m.id == userId);
                                //email_CC = db.wyzusers.FirstOrDefault(m => m.userName == wyzuserDetails.creManager).emailId;
                                workshopId = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehID).vehicleWorkshop_id ?? default(long);
                                subject = getEmailBodyAndSub(cusID, vehID, workshopId, emailTemplateId, 0, 0, 1);


                                //stored procedure for body
                                //if (emailType == "DISSATPREVIOUS" || emailType == "DISSATPOSTSALES")
                                //{
                                //    emailBody = getEmailBodyAndSub(wyzUserID_var, vehID_var, locID_var, smsID_var, insID_var, docID_var, "3");
                                //}
                                //else
                                //{
                                emailBody = getEmailBodyAndSub(cusID, vehID, workshopId, emailTemplateId, 0, 0, 2);
                                //}
                                if ((role == 4) && (calldispositionid == 224))
                                {
                                    var emailDetails = db.psfescalationemails.FirstOrDefault(m => m.workshopid_fk == workshopId);
                                    toEmailAddress = emailDetails.toemail;
                                    email_CC = emailDetails.ccemail;

                                }
                                else if ((role == 1) && (innerRequiredReason == "Dissatisfied with Last Service"))
                                {
                                    toEmailAddress = db.emails.FirstOrDefault(m => m.customer_id == cusID && m.isPreferredEmail == true).emailAddress;
                                    var locationId = db.assignedinteractions.Where(m => m.customer_id == emailinter.customer_id && m.vehical_Id == emailinter.vehicle_id).Select(m => m.location_id).FirstOrDefault();
                                    var ccmail = db.workshops.Where(m => m.id == locationId).Select(m => m.escalationMails).FirstOrDefault();
                                    email_CC = ccmail;
                                }
                                else
                                {
                                    toEmailAddress = db.emails.FirstOrDefault(m => m.customer_id == cusID && m.isPreferredEmail == true).emailAddress;
                                }
                                fromEmailAddress = emailCredentials.fromemail;
                               
                                
                                    using (MailMessage mailMessage = new MailMessage())
                                    {
                                        List<string> Emails = new List<string>();
                                        if (!(String.IsNullOrEmpty(toEmailAddress)))
                                        {
                                            if (toEmailAddress.Contains(','))
                                            {
                                                Emails = toEmailAddress.Split(',').ToList();
                                                foreach (var email in Emails)
                                                {
                                                    mailMessage.To.Add(email);
                                                }
                                            }
                                            else
                                            {
                                                mailMessage.To.Add(toEmailAddress);
                                            }
                                        }

                                        if (!(String.IsNullOrEmpty(email_CC)))
                                        {
                                            if (email_CC.Contains(','))
                                            {
                                                Emails = email_CC.Split(',').ToList();
                                                foreach (var email in Emails)
                                                {
                                                    mailMessage.CC.Add(email);
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(email_CC))
                                                {
                                                    mailMessage.CC.Add(email_CC);
                                                }

                                            }
                                        }
                                        mailMessage.Subject = subject;
                                        mailMessage.Body = emailBody;

                                        emailinter.fromEmailAddress = fromEmailAddress;
                                        emailinter.toEmailAddress = toEmailAddress;
                                        emailinter.emailSubject = subject;
                                        emailinter.emailContent = emailBody;
                                        emailinter.emailStatus = true;
                                        emailinter.cc = email_CC;


                                        mailMessage.From = new MailAddress(fromEmailAddress);
                                        using (SmtpClient smtp = new SmtpClient())
                                        {
                                            smtp.Host = emailCredentials.hostapi;
                                            smtp.Port = Convert.ToInt32(emailCredentials.portnumber);
                                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                            smtp.UseDefaultCredentials = true;
                                            smtp.Credentials = new System.Net.NetworkCredential(emailCredentials.userEmail  , emailCredentials.userPassword);
                                            smtp.EnableSsl = true;
                                            smtp.Send(mailMessage);

                                            response_string = "Email Sent";
                                            

                                        }
                                    
                                }
                            }

                        }
                        else
                        {
                            response_string = "No From/To Email Credentials Present";
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                response_string = ex.InnerException.InnerException.Message;
                            }
                            else
                            {
                                response_string = ex.InnerException.Message;
                            }
                        }
                        else
                        {
                            response_string = ex.Message;
                        }
                    }
                    emailinter.reason = response_string;
                    db.emailinteractions.Add(emailinter);
                    db.SaveChanges();
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

                logger.Info("Auto Email Error(Outerloop): " + exception);
            }
        }

        public string getEmailBodyAndSub(long wyzUserID, long vehID, long locID, long emailTemplateId,
                                         long insID, long docID, long procedureType)
        {
            string returnString = string.Empty;
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("inwyzuser_id",wyzUserID),
                        new MySqlParameter("invehicle_id",vehID),
                        new MySqlParameter("inlocid",locID),
                        new MySqlParameter("insmsid",emailTemplateId),
                        new MySqlParameter("ininsid",insID),
                        new MySqlParameter("indocid",docID)
                    };
                    if (procedureType == 1)
                    {
                        returnString = dBContext.Database.SqlQuery<string>(@"call sendemailsubject(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid,@indocid);", param).FirstOrDefault();
                    }
                    else if (procedureType == 2)
                    {
                        returnString = dBContext.Database.SqlQuery<string>(@"call sendemailbody(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid,@indocid);", param).FirstOrDefault();
                    }
                    else if (procedureType == 3)
                    {
                        returnString = dBContext.Database.SqlQuery<string>(@"call sendemailbodyforpsf(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid,@indocid);", param).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return returnString;
        }
    }
}