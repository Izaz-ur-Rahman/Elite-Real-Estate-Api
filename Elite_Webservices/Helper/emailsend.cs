using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Elite_Webservices.Helper
{
    public class emailsend
    {
        [Obsolete]
    
        public static string SendEmails((string email, string name ,string message,string Emailtype,string subject) recipient)
        {
            try
            {
                var fromemail = ConfigurationSettings.AppSettings["DefaultEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var port = ConfigurationSettings.AppSettings["port"].ToString();
                var host = ConfigurationSettings.AppSettings["host"].ToString();
                fromemail = "nadeemullah@raideit.com";
                password = "Iqra@444";
                port = "587";
                host = "smtp.office365.com";

                // Set your HTML content here with dynamic data
                // </*p><strong>Email:</strong> {recipient.email}</p>*/
                string contents = $@"<!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>{recipient.Emailtype} </title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <div style=""background-color: #f4f4f4; padding: 20px; text-align: center;"">
                                    <img src=""https://raidtime.raideit.net/Content/websiteassets/assets/img/timee-logo-n.png"" style=""width: 20%;"">
                                </div>

                                <div style=""padding: 20px;"">
                                    <p style=""color:black"">Hello {recipient.name},</p>

                                    <p style=""color:black"">We hope this email finds you well.  </p>

                                   
                                     
                                    
                                    <div style=""  background-color: #fff;color:black"">
                                        <p>{recipient.message}</p>
                                    </div>

                                    <p style=""  background-color: #fff;color:black"">Feel free to contact us if you have any questions or concerns.</p>

                                    <p style=""  background-color: #fff;color:black"">Best regards,<br>
                                     raideTime Team</p>
                                </div>

                                <div style=""background-color: #f4f4f4; padding: 10px; text-align: center;color:black"">
                                    <p style=""color: #777;"">This is an automated email. Please do not reply.</p>
                                </div>
                            </body>
                            </html>";

                MailMessage message = new MailMessage();
                

                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(recipient.email));
                message.Subject = recipient.subject;
                message.IsBodyHtml = true; //to make message body as html
                message.Body = contents;
                using (SmtpClient smtp = new SmtpClient(host))
                {
                    smtp.Port = Convert.ToInt32(port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromemail, password);
                    smtp.EnableSsl = true; // Ensure SSL is enabled
                  

                    smtp.Send(message);
                }

                return "send";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }


        [Obsolete]

        public static string SendEmailsWithTemplete((string email, string name, string message, string Emailtype, string subject,string recemial) recipient)
        {
            try
            {
                var fromemail = ConfigurationSettings.AppSettings["DefaultEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var port = ConfigurationSettings.AppSettings["port"].ToString();
                var host = ConfigurationSettings.AppSettings["host"].ToString();
                //fromemail = "nadeemullah@raideit.com";
                //password = "Iqra@444";
                //port = "587";
                //host = "smtp.office365.com";

                // Set your HTML content here with dynamic data
                // </*p><strong>Email:</strong> {recipient.email}</p>*/
                string contents = recipient.message;
                // Check for <iframe> presence in the message
               
                MailMessage message = new MailMessage();


                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(recipient.email));
                if (!string.IsNullOrEmpty(recipient.recemial))
                {
                    message.To.Add(new MailAddress(recipient.recemial));
                }
                message.Subject = recipient.subject;
                message.IsBodyHtml = true; //to make message body as html
                message.Body = contents;
                using (SmtpClient smtp = new SmtpClient(host))
                {
                    smtp.Port = Convert.ToInt32(port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromemail, password);
                    smtp.EnableSsl = true; // Ensure SSL is enabled


                    smtp.Send(message);
                }

                return "send";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [Obsolete]
        public static string SendCampaignEmailsWithTemplete((string email, string name, string message,   string subject ) recipient)
        {
            try
            {
                var fromemail = ConfigurationSettings.AppSettings["DefaultEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var port = ConfigurationSettings.AppSettings["port"].ToString();
                var host = ConfigurationSettings.AppSettings["host"].ToString();
                //fromemail = "nadeemullah@raideit.com";
                //password = "Iqra@444";
                //port = "587";
                //host = "smtp.office365.com";

                // Set your HTML content here with dynamic data
                // </*p><strong>Email:</strong> {recipient.email}</p>*/
                string contents = recipient.message;

                MailMessage message = new MailMessage();


                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(recipient.email));
               
                message.Subject = recipient.subject;
                message.IsBodyHtml = true; //to make message body as html
                message.Body = contents;
                using (SmtpClient smtp = new SmtpClient(host))
                {
                    smtp.Port = Convert.ToInt32(port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromemail, password);
                    smtp.EnableSsl = true; // Ensure SSL is enabled


                    smtp.Send(message);
                }

                return "send";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
       
        [Obsolete]

        public static string statusEmail((string email, string name,string jobid, string jobtitle, string status) recipient)
        {
            try
            {
                //recipient.email = "nizamulhaqnz7@gmail.com";
                var fromemail = ConfigurationSettings.AppSettings["DefaultEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var port = ConfigurationSettings.AppSettings["port"].ToString();
                var host = ConfigurationSettings.AppSettings["host"].ToString();
                string additionalInformationEn = "";
                string additionalInformationAr = "";
                string additionalInformationAr2 = "";
                string additionalInformationAr3 = "";
                string emailsubject = "Thanks for applying to FNRC";
                if (recipient.status == "Applied")
                {

                      additionalInformationEn = "Dear applicant, thank you for your interest in applying for the position at our corporation under number ( " + recipient.jobid + "), we would like to confirm that your application for the available position in the (" + recipient.jobtitle + ") department has been successfully registered. We will review it carefully and will contact you later. ";
                      additionalInformationAr = ", ( " + recipient.jobid + ")    عزيزي مقدم الطلب نشكرك على اهتمامك بتقديم طلب الحصول على الوظيفة في مؤسستنا تحت رقم";
                      additionalInformationAr2 = ", ( " + recipient.jobtitle + ") نود أن نؤكد لك بأنه تم تسجيل طلبك بنجاح للوظيفة المتاحة في قسم ";
                      additionalInformationAr3 = " . سنقوم بمراجعته بعناية وسنتواصل معك لاحقاً ";
                }
                else
                {
                    emailsubject = "Job Application-Rejected";
                    additionalInformationEn = "Dear applicant, we appreciate your interest and efforts in applying for the position at our corporation under number ( "+recipient.jobid+ " ) (" + recipient.jobtitle + "). After review, we regret to inform you that your application has been declined due to not meeting the requirements. We wish you all the best and thank you once again";
                    additionalInformationAr = ", ( " + recipient.jobid + ")    عزيزي مقدم الطلب نشكرك على اهتمامك وجهودك بتقديم طلب الحصول على الوظيفة في مؤسستنا تحت رقم ";
                    additionalInformationAr2 = ". وبعد المراجعة نأسف لإبلاغك بأنه تم رفضه وذلك لعدم استكمال الشروط، نتمنى لك كل التوفيق ونشكرك مرة أخرى ";
                    additionalInformationAr3 = "";
                }
                // Set your HTML content here with dynamic data
                // </*p><strong>Email:</strong> {recipient.email}</p>*/
                string contents = $@"<!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Fnrc Jobs - Application Status Update</title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <div style=""background-color: #f4f4f4; padding: 20px; text-align: center;"">
                                    <h2 style=""color: #333;"">FNRC Jobs</h2>
                                </div>

                                <div style=""padding: 20px;"">
                                    
                                     
                                    <p  style=""font-size: 16px;font-weight: 700;"">{additionalInformationEn}</p>
                                      <br>
                                       <br>
                                    <p  style=""text-align:right;font-size: 16px;font-weight: 700;"">{additionalInformationAr} </p>
                                    <p  style=""text-align:right;font-size: 16px; font-weight: 700;"">{additionalInformationAr2} </p>
                                    <p   style=""text-align:right;font-size: 16px;font-weight: 700;"">{additionalInformationAr3} </p>


                                    <p>Best regards,<br>
                                     FNRC HR Team</p>
                                </div>

                                <div style=""background-color: #f4f4f4; padding: 10px; text-align: center;"">
                                    <p style=""color: #777;"">This is an automated email. Please do not reply.</p>
                                </div>
                            </body>
                            </html>";

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(recipient.email));
                message.Subject = emailsubject;
                message.IsBodyHtml = true; //to make message body as html
                message.Body = contents;
                smtp.Port = int.Parse(port);
                smtp.Host = host; //for gmail host
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromemail, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return "send";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        [Obsolete]

        public static string InterviewEmail((string email, string name, string jobid, string jobtitle, string status,string interviewdate,string interviewTime,string Notes) recipient)
        {
            try
            {
               // recipient.email = "nizamulhaqnz7@gmail.com";
                var fromemail = ConfigurationSettings.AppSettings["DefaultEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var port = ConfigurationSettings.AppSettings["port"].ToString();
                var host = ConfigurationSettings.AppSettings["host"].ToString();
                string additionalInformationEn = "";
                string additionalInformationAr = "";
                string additionalInformationAr2 = "";
                string additionalInformationAr3 = "";
                
                    additionalInformationEn = "Dear applicant, thank you for your interest in applying for the position at our corporation under number ( 2525 ), We are pleased to inform you that your application has been accepted, and we are interested in interviewing you to discuss further details. ";
                  
                string additionalInformationEn1 = "Please bring any relevant documents related to the job. We look forward to meeting you on this date> If you have any additional inquiries, feel free to contact us.";
                    additionalInformationAr = ", ( " + recipient.jobid + ")    عزيزي مقدم الطلب نشكرك على اهتمامك بتقديم طلب الحصول على الوظيفة في مؤسستنا تحت رقم ";
                    additionalInformationAr2 = ". يسرنا ابلاغك بأنه تم قبول طلبك، ونحن مهتمون بمقابلتك لمناقشة تفاصيل أكثر ";
                    additionalInformationAr3 =  ". يرجى احضار أي وثائق قد تكون ذات صلة بالوظيفة. نتطلع إلى لقائك في هذا التاريخ، إذا كان لديك أي استفسارات اضافية فلا تتردد في الاتصال بنا ";
                
                // Set your HTML content here with dynamic data
                // </*p><strong>Email:</strong> {recipient.email}</p>*/
                string contents = $@"<!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Fnrc Jobs - Application Status Update</title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <div style=""background-color: #f4f4f4; padding: 20px; text-align: center;"">
                                    <h2 style=""color: #333;"">FNRC Jobs</h2>
                                </div>

                                <div style=""padding: 20px;"">
                                    
                                     
                                    <p  style=""font-size: 16px;font-weight: 700;"">{additionalInformationEn}</p>
                                      <p style=""font-size: 16px;font-weight: 700;"">Interview Date: {recipient.interviewdate} <span>Time : {recipient.interviewTime}</span> </p>
<span style=""font-size: 16px;font-weight: 700;"">Notes : {recipient.Notes}</span>
                                    <p  style=""font-size: 16px;font-weight: 700;"">{additionalInformationEn1}</p>
                                      <br>
                                       <br>
                                    <p  style=""text-align:right;font-size: 16px;font-weight: 700;"">{additionalInformationAr} </p>
                                    <p  style=""text-align:right;font-size: 16px; font-weight: 700;"">{additionalInformationAr2} </p>
                                    <p  style=""text-align:right;font-size: 16px; font-weight: 700;"">{recipient.interviewdate}:تاريخ المقابلة </p>
                                    <p  style=""text-align:right;font-size: 16px; font-weight: 700;"">{recipient.interviewTime}: الوقت  </p>
                                    <p  style=""text-align:right;font-size: 16px; font-weight: 700;"">{recipient.Notes}: ملاحظات  </p>
                                    
                                      <p   style=""text-align:right;font-size: 16px;font-weight: 700;"">{additionalInformationAr3} </p>


                                    <p>Best regards,<br>
                                    FNRC HR Team</p>
                                </div>

                                <div style=""background-color: #f4f4f4; padding: 10px; text-align: center;"">
                                    <p style=""color: #777;"">This is an automated email. Please do not reply.</p>
                                </div>
                            </body>
                            </html>";

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(recipient.email));
                message.Subject = "Interview schedule";
                message.IsBodyHtml = true; //to make message body as html
                message.Body = contents;
                smtp.Port = int.Parse(port);
                smtp.Host = host; //for gmail host
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromemail, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return "send";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [Obsolete]

        public static string registrationEmail((string email, string name) recipient)
        {
            try
            {
               // recipient.email = "nizamulhaqnz7@gmail.com";
                var fromemail = ConfigurationSettings.AppSettings["DefaultEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var port = ConfigurationSettings.AppSettings["port"].ToString();
                var host = ConfigurationSettings.AppSettings["host"].ToString();
               // string additionalInformation = "Congratulations! Your application has passed the initial screening and has been moved to the next stage of the hiring process. We will contact you shortly to schedule an interview. Thank you for your patience and interest in Fnrc Jobs.";

                // Set your HTML content here with dynamic data
                // </*p><strong>Email:</strong> {recipient.email}</p>*/
                string contents = $@"<!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Welcome to FNRC Jobs Portal - Confirm Your New Account</title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <div style=""background-color: #f4f4f4; padding: 20px; text-align: center;"">
                                    <h2 style=""color: #333;"">FNRC Jobs</h2>
                                </div>

                                <div style=""padding: 20px;"">
                                    <p>Dear {recipient.name},</p>

                                    <p>Congratulations on taking the first step toward finding your dream job with FNRC Jobs Portal! We're thrilled to have you on board. </p>

                                    <p> Thank you for choosing FNRC Jobs Portal. We're dedicated to helping you find the perfect job match.</p>

                                    <p>Best regards,<br>
                                    FNRC HR Team</p>
                                </div>

                                <div style=""background-color: #f4f4f4; padding: 10px; text-align: center;"">
                                    <p style=""color: #777;"">This is an automated email. Please do not reply.</p>
                                </div>
                            </body>
                            </html>";

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromemail);
                message.To.Add(new MailAddress(recipient.email));
                message.Subject = "Welcome to FNRC Jobs Portal - Confirm Your New Account";
                message.IsBodyHtml = true; //to make message body as html
                message.Body = contents;
                smtp.Port = int.Parse(port);
                smtp.Host = host; //for gmail host
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromemail, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return "send";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}