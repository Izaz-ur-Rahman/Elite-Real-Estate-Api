using Elite_Webservices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;

namespace Elite_Webservices.Helper
{
    public class ContactusEmail
    {
        EliteCMSEntities db = new EliteCMSEntities();
        public async Task<int> SendContactUsToAdminAsync(ContactUsemail contact)
        {
            try
            {
                var fromemail = ConfigurationSettings.AppSettings["FromEmail"].ToString();
                var ReceiverEmail = ConfigurationSettings.AppSettings["ReceiverEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var Port = ConfigurationSettings.AppSettings["Port"].ToString();
                var host = ConfigurationSettings.AppSettings["Host"].ToString();

                var mailMessage = new System.Net.Mail.MailMessage
                {
                    From = new MailAddress(fromemail),
                    Subject = "New Contact Us Submission",
                    Body = $"Name: {contact.FirstName} {contact.LastName}\n" +
                           $"Phone: {contact.PhoneNumber}\n" +
                           $"Email: {contact.Email}\n" +
                           $"Message: {contact.Message}",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(ReceiverEmail);

                using (SmtpClient smtp = new SmtpClient(host))
                {
                    smtp.Port = Convert.ToInt32(Port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromemail, password);
                    smtp.EnableSsl = true; 

                    smtp.Send(mailMessage);

                    // Map ContactUsemail to ContactU before saving
                    var contactEntity = new Elite_Webservices.Models.ContactU
                    {
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        PhoneNumber = contact.PhoneNumber,
                        Email = contact.Email,
                        Message = contact.Message,
                        PageSource = "Contact Us Form",
                        Status = "New"
                    };

                    db.ContactUs.Add(contactEntity);
                    db.SaveChanges();
                }
                
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending admin email: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> SendThankYouEmailAsync(string recipientName, string recipientEmail)
        {
            try
            {
                var fromemail = ConfigurationSettings.AppSettings["FromEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var Port = ConfigurationSettings.AppSettings["Port"].ToString();
                var host = ConfigurationSettings.AppSettings["Host"].ToString();


                EliteCMSEntities db = new EliteCMSEntities();
                var emailTemplate =  db.Cms_EmailTemplete.Where(x => x.Id == 1).ToList();
                if (emailTemplate == null || string.IsNullOrEmpty(emailTemplate.FirstOrDefault().Details))
                    return 0;

             
                string subject = emailTemplate.FirstOrDefault().Subject;
                string body = $"Dear {recipientName},<br><br>{emailTemplate.FirstOrDefault().Details}";

              

                var message = new System.Net.Mail.MailMessage
                {
                    From = new MailAddress(fromemail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(recipientEmail);

                using (SmtpClient smtp = new SmtpClient(host))
                {
                    smtp.Port = Convert.ToInt32(Port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromemail, password);
                    smtp.EnableSsl = true; 


                    smtp.Send(message);
                }

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending Thank You email: {ex.Message}");
                return 0;
            }
        }


        public async Task<int> QuotationEmailAsync(QuotationModel contact)
        {
            try
            {
                var fromemail = ConfigurationSettings.AppSettings["FromEmail"].ToString();
                var ReceiverEmail = ConfigurationSettings.AppSettings["ReceiverEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var Port = ConfigurationSettings.AppSettings["Port"].ToString();
                var host = ConfigurationSettings.AppSettings["Host"].ToString();

               
                var mailMessage = new System.Net.Mail.MailMessage
                {
                    From = new MailAddress(fromemail),
                    Subject = "Quotation Contact",
                    Body = $"Name: {contact.FullName}\n" +
                   $"Phone: {contact.PhoneNumber}\n" +
                   $"Email: {contact.Email}\n" +
                   $"Category: {contact.Category}\n" +

                   $"Budget Range: {contact.BudgetMin}{contact.Currency}" +
                   $" to {contact.BudgetMax}{contact.Currency}\n" +
                   (string.IsNullOrEmpty(contact.Country) ? "" : $"Country: {contact.Country}\n") +
                   $"Message: {contact.Message}",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(ReceiverEmail);

                using (SmtpClient smtp = new SmtpClient(host))
                {
                    smtp.Port = Convert.ToInt32(Port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromemail, password);
                    smtp.EnableSsl = true;


                    smtp.Send(mailMessage);

                    var quotationEntity = new Elite_Webservices.Models.Quotation
                    {
                        FullName = contact.FullName,
                        Email = contact.Email,
                        PhoneNumber = contact.PhoneNumber,
                        Category = contact.Category,
                        Currency = contact.Currency,
                        BudgetMin = contact.BudgetMin,
                        BudgetMax = contact.BudgetMax,
                        Message = contact.Message,
                        PageSource = contact.PageSource,
                        Country = contact.Country == null ? "" : contact.Country,
                        Source ="Website",
                        Status = "New"
                    };

                    db.Quotations.Add(quotationEntity);
                    db.SaveChanges();
                }

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending admin email: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> SendAgentEmailAsync(AgentContactemail contact)
        {
            try
            {
                var fromemail = ConfigurationSettings.AppSettings["FromEmail"].ToString();
                var password = ConfigurationSettings.AppSettings["password"].ToString();
                var Port = ConfigurationSettings.AppSettings["Port"].ToString();
                var host = ConfigurationSettings.AppSettings["Host"].ToString();


                EliteCMSEntities db = new EliteCMSEntities();
                var emailTemplate = db.Cms_EmailTemplete.Where(x => x.Id == 1).ToList();
                if (emailTemplate == null || string.IsNullOrEmpty(emailTemplate.FirstOrDefault().Details))
                    return 0;


                string subject = emailTemplate.FirstOrDefault().Subject;
                string body = $"Dear {contact.FullName},<br><br>{emailTemplate.FirstOrDefault().Details}";



                var message = new System.Net.Mail.MailMessage
                {
                    From = new MailAddress(fromemail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(contact.AgentEmail);

                using (SmtpClient smtp = new SmtpClient(host))
                {
                    smtp.Port = Convert.ToInt32(Port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromemail, password);
                    smtp.EnableSsl = true;


                    smtp.Send(message);

                    // Map ContactUsemail to ContactU before saving
                    var contactEntity = new Elite_Webservices.Models.ContactU
                    {
                        FirstName = contact.FullName,
                        PhoneNumber = contact.PhoneNumber,
                        Email = contact.Email,
                        Message = contact.Message,
                        PageSource = "Agent Contact",
                        PropertyName= contact.PropertyName,
                        AgentName= contact.AgentName,
                        Status = "New"
                    };

                    db.ContactUs.Add(contactEntity);
                    db.SaveChanges();
                }

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending agent email: {ex.Message}");
                return 0;
            }
        }

    }


    public class ContactUsemail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

    public class AgentContactemail
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string PropertyName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentName { get; set; }
    }
    public class QuotationModel
    {
       // public  int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public string BudgetMin { get; set; }
        public string BudgetMax { get; set; }
        public string Message { get; set; }
        public string PageSource { get; set; }
        public string Country { get; set; }


    }
}