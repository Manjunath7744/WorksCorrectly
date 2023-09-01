using MySql.Data.MySqlClient;
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
    public class AutoEmail : IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                long cusID, userId, vehID;
                string emailType, fromEmailId, password, DealerCode, toEmailId, toCCemail, toemailSubject, toemailBody;

                JobDataMap dataMap = context.JobDetail.JobDataMap;

                userId = dataMap.GetLong("WyzId");
                vehID = dataMap.GetLong("vehicleId");
                cusID = dataMap.GetLong("custId");
                emailType = dataMap.GetString("emailType");
                fromEmailId = dataMap.GetString("fromEmailId");
                password = dataMap.GetString("password");
                toEmailId = dataMap.GetString("toEmailId");
                DealerCode = dataMap.GetString("DealerCode");
                toCCemail = dataMap.GetString("toCCemail");
                toemailSubject = dataMap.GetString("toemailSubject");
                toemailBody = dataMap.GetString("toemailBody");



                //userId = context.Scheduler.Context.GetLong("WyzId");
                //vehID = context.Scheduler.Context.GetLong("vehicleId");
                //cusID = context.Scheduler.Context.GetLong("custId");

                //emailType = context.Scheduler.Context.GetString("emailType");
                //fromEmailId = context.Scheduler.Context.GetString("fromEmailId");
                //password = context.Scheduler.Context.GetString("password");
                //toEmailId = context.Scheduler.Context.GetString("toEmailId");
                //DealerCode = context.Scheduler.Context.GetString("DealerCode");


                bool dissPrvSaleDay = false;
                string toEmailAddress = "", subject = "", fromEmailAddress = "", emailBodySub = "", email_CC = "";
                try
                {
                    using (AutoSherDBContext dBContext = new AutoSherDBContext())
                    {
                        List<emailinteraction> emailInter = new List<emailinteraction>();
                        if (emailType == "DISSATPREVIOUS")
                        {
                            string str = @"select count(*) from emailinteraction where vehicle_id=" + vehID + " && customer_id=" + cusID + " && emailType='" + emailType + "' && date(interactionDateAndTime)=current_date() && emailStatus=1;";
                            long dayCount = dBContext.Database.SqlQuery<long>(str).FirstOrDefault();

                            if (dayCount > 0)
                            {
                                dissPrvSaleDay = true;
                            }
                        }

                        if (dissPrvSaleDay == false)
                        {
                            string workshopIDl = string.Empty;
                            string wyzUserID_var, vehID_var, locID_var, smsID_var = "", insID_var;
                            long docID_var;
                            long workshopID=0;
                            var role1 = dBContext.wyzusers.FirstOrDefault(m => m.id == userId).role1;
                            if ((DealerCode == "HARPREETFORD" || DealerCode == "HANSHYUNDAI" || DealerCode == "PODDARCARWORLD") && (emailType == "DISSATPREVIOUSSERVICE" || emailType == "SMRDISSATWITHINSURANCE") || (role1 == "2"))

                            {
                                //(dBContext.services.Count(m => m.vehicle_vehicle_id == vehID && (m.lastServiceType.Contains("FS1") || m.lastServiceType.Contains("FS2") || m.lastServiceType.Contains("FS3") || m.lastServiceType.Contains("PMS") || m.lastServiceType.Contains("PS"))
                                var lastserviceTypeDetails = dBContext.services.Where(m => m.vehicle_vehicle_id == vehID).OrderByDescending(m => m.id).FirstOrDefault();
                                if ((lastserviceTypeDetails!=null) && (lastserviceTypeDetails.lastServiceType!=null) && (lastserviceTypeDetails.lastServiceType.Contains("FS1") || lastserviceTypeDetails.lastServiceType.Contains("FS2") || lastserviceTypeDetails.lastServiceType.Contains("FS3") || lastserviceTypeDetails.lastServiceType.Contains("PMS") || lastserviceTypeDetails.lastServiceType.Contains("PS")))
                                {
                                    workshopID  = lastserviceTypeDetails.workshop_id??default(long);
                                    //workshopIDl = (workshopIDs).ToString();
                                }
                                else
                                {
                                     workshopID = dBContext.vehicles.FirstOrDefault(m => m.vehicle_id == vehID).vehicleWorkshop_id ?? default(long);
                                    //workshopIDl = (workshopIDs).ToString();
                                }

                            }
                            else
                            {
                                var workshopIDs = dBContext.vehicles.FirstOrDefault(m => m.vehicle_id == vehID).vehicleWorkshop_id;
                                workshopIDl = (workshopIDs).ToString();
                                workshopID = Convert.ToInt64(workshopIDl);
                            }
                             
                            string emailTo = "";
                            string emailCC = "";


                            //getting To address and CC
                            //if (DealerCode == "HARPREETFORD" || DealerCode == "HANSHYUNDAI" || DealerCode == "testdb")
                            //{
                            //    emailTo = toEmailId;
                            //    emailCC = toCCemail;
                            //}
                            //else
                            //{
                            if ((DealerCode == "HARPREETFORD" || DealerCode == "HANSHYUNDAI" || DealerCode == "PODDARCARWORLD") && (emailType == "DISSATPREVIOUSSERVICE" || emailType == "DISSATCLAIM" || emailType == "SMRDISSATWITHINSURANCE" || emailType == "INSDISSATPREVIOUSSERVICE" || emailType == "INSDISSATWITHINSURANCE"))
                            {
                                if (role1 == "2")
                                {
                                    emailTo = dBContext.workshops.SingleOrDefault(m => m.id == workshopID).insEscalationMails;

                                }
                                else
                                {
                                    emailTo = dBContext.workshops.SingleOrDefault(m => m.id == workshopID).escalationMails;
                                }
                            }

                            else
                            { 
                            if (string.IsNullOrEmpty(toEmailId))
                            {
                                //emailTo = dBContext.workshops.SingleOrDefault(m => m.id == workshopID).escalationMails;
                                emailTo = dBContext.emails.Where(m => m.isPreferredEmail == true && m.customer_id == cusID).FirstOrDefault().emailAddress;
                            }
                            else
                            {
                                emailTo = toEmailId;
                            }
                            }
                            if ((DealerCode == "HARPREETFORD" || DealerCode == "HANSHYUNDAI") && (emailType == "NONCONTACT") && role1 == "1")
                            {
                                emailCC = string.Empty;
                            }
                            else if ((string.IsNullOrEmpty(toCCemail)) && (emailType!= "DISSATPREVIOUS") && (dBContext.workshops.FirstOrDefault(m => m.id == workshopID).escalationCC != null) && (role1 == "1"))
                            {
                                emailCC = email_CC = dBContext.workshops.SingleOrDefault(m => m.id == workshopID).escalationCC;
                            }
                            else if ((string.IsNullOrEmpty(toCCemail)) && (emailType != "DISSATPREVIOUS") && (dBContext.workshops.FirstOrDefault(m => m.id == workshopID).insEscalationCC != null))
                            {
                                emailCC = email_CC = dBContext.workshops.SingleOrDefault(m => m.id == workshopID).insEscalationCC;
                            }   
                            else
                            {
                                emailCC = toCCemail;
                            }
                            //}
                            //Stored procedure variables
                            wyzUserID_var = userId.ToString();
                            vehID_var = vehID.ToString();
                            locID_var = workshopID.ToString();
                            //emailType = "UPLOAD_DOC";
                            if (dBContext.emailtemplates.Any(m => m.emailType == emailType))
                            {

                                smsID_var = dBContext.emailtemplates.SingleOrDefault(m => m.emailType == emailType).id.ToString();

                            }
                            else
                            {
                                return;
                            }

                            insID_var = "0";
                            docID_var = 0;
                            var emailCredentials = dBContext.emailcredentials.FirstOrDefault(m => m.userEmail == fromEmailId);
                            //stored procedure for subject
                            string emailSubject = getEmailBodyAndSub(wyzUserID_var, vehID_var, locID_var, smsID_var, insID_var, docID_var, "1");
                            string emailBody = string.Empty;
                            //stored procedure for body
                            if (emailType == "DISSATPREVIOUS" || emailType== "DISSATPOSTSALES")
                            {
                                emailBody = getEmailBodyAndSub(wyzUserID_var, vehID_var, locID_var, smsID_var, insID_var, docID_var, "3");
                            }
                            else
                            {
                                emailBody = getEmailBodyAndSub(wyzUserID_var, vehID_var, locID_var, smsID_var, insID_var, docID_var, "2");
                            }

                            subject = emailSubject;
                            emailBodySub = emailBody;

                            if(emailType== "DISSATPOSTSALES")
                            {
                                emailSubject = toemailSubject;
                                emailBody = emailBody + toemailBody;
                            }

                            fromEmailAddress = fromEmailId;
                            toEmailAddress = emailTo;
                            email_CC = emailCC;
                            if (string.IsNullOrEmpty(password))
                            {
                                password = emailCredentials.userPassword;
                            }

                            using (MailMessage mailMessage = new MailMessage())
                            {
                                List<string> Emails = new List<string>();
                                if (emailTo.Contains(','))
                                {
                                    Emails = emailTo.Split(',').ToList();
                                    foreach (var email in Emails)
                                    {
                                        mailMessage.To.Add(email);
                                    }
                                }
                                else
                                {
                                    mailMessage.To.Add(emailTo);
                                }

                                if (emailCC.Contains(','))
                                {
                                    Emails = emailCC.Split(',').ToList();
                                    foreach (var email in Emails)
                                    {
                                        mailMessage.CC.Add(email);
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(emailCC))
                                    {
                                        mailMessage.CC.Add(emailCC);
                                    }

                                }

                                mailMessage.Subject = emailSubject;
                                mailMessage.Body = emailBody;

                                mailMessage.From = new MailAddress(emailCredentials.userEmail);
                                using (SmtpClient smtp = new SmtpClient())
                                {
                                    smtp.Host = emailCredentials.hostapi;
                                    smtp.Port = Convert.ToInt32(emailCredentials.portnumber);
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.UseDefaultCredentials = true;
                                    smtp.Credentials = new System.Net.NetworkCredential(emailCredentials.userEmail, password);
                                    smtp.EnableSsl = true;
                                    smtp.Send(mailMessage);

                                    //smtp.Host = emailCredentials.hostapi;
                                    //smtp.Port = int.Parse(emailCredentials.portnumber);
                                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    //smtp.UseDefaultCredentials = false;
                                    //smtp.Credentials = new System.Net.NetworkCredential(email.EmailFrom, email.Password);
                                    //smtp.EnableSsl = true;
                                    //smtp.Send(mailMessage);


                                    emailinteraction emailinter = new emailinteraction();
                                    emailinter.wyzUser_id = userId;
                                    emailinter.customer_id = cusID;
                                    emailinter.vehicle_id = vehID;
                                    emailinter.toEmailAddress = toEmailAddress;
                                    emailinter.emailSubject = subject;
                                    emailinter.emailContent = emailBodySub;
                                    emailinter.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                                    emailinter.interactionDateAndTime = DateTime.Now;
                                    emailinter.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                                    emailinter.emailStatus = true;
                                    emailinter.cc = email_CC;
                                    emailinter.fromEmailAddress = fromEmailAddress;
                                    emailinter.emailType = emailType;
                                    emailinter.reason = "Email Sent";
                                    dBContext.emailinteractions.Add(emailinter);
                                    dBContext.SaveChanges();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    using (var db = new AutoSherDBContext())
                    {
                        emailinteraction emailinter = new emailinteraction();

                        emailinter.wyzUser_id = userId;
                        emailinter.customer_id = cusID;
                        emailinter.vehicle_id = vehID;
                        emailinter.toEmailAddress = toEmailAddress;
                        emailinter.emailSubject = subject;
                        emailinter.emailContent = emailBodySub;
                        emailinter.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                        emailinter.interactionDateAndTime = DateTime.Now;
                        emailinter.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                        if (toEmailAddress == string.Empty && (DealerCode == "HARPREETFORD" || DealerCode == "HANSHYUNDAI") && (emailType == "NONCONTACT"))
                        {
                            emailinter.exceptionResponse = "No 'To' mail address";
                        }
                        else
                        {
                            emailinter.exceptionResponse = ex.Message;
                        }
                        emailinter.emailStatus = false;
                        emailinter.fromEmailAddress = fromEmailAddress;
                        emailinter.cc = email_CC;
                        emailinter.emailType = emailType;
                        emailinter.reason = "Email not sent";
                        db.emailinteractions.Add(emailinter);
                        db.SaveChanges();
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

                logger.Info("Auto Email Error(Outerloop): " + exception);
            }
        }

        public string getEmailBodyAndSub(string wyzUserID_var, string vehID_var, string locID_var, string smsID_var,
                                         string insID_var, long docID_var, string procedureType_var)
        {
            string returnString = string.Empty;
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("inwyzuser_id",wyzUserID_var),
                        new MySqlParameter("invehicle_id",vehID_var),
                        new MySqlParameter("inlocid",locID_var),
                        new MySqlParameter("insmsid",smsID_var),
                        new MySqlParameter("ininsid",insID_var),
                        new MySqlParameter("indocid",docID_var)
                    };
                    if (procedureType_var == "1")
                    {
                        returnString = dBContext.Database.SqlQuery<string>(@"call sendemailsubject(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid,@indocid);", param).FirstOrDefault();
                    }
                    else if (procedureType_var == "2")
                    {
                        returnString = dBContext.Database.SqlQuery<string>(@"call sendemailbody(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid,@indocid);", param).FirstOrDefault();
                    }
                    else if (procedureType_var == "3")
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