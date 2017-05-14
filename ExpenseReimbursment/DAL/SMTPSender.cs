using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ExpenseReimbursment.DAL
{
    public static class SMTPSender
    {
        public static string FromAddress = "gvb4676@gmail.com";
        public static string ToAddress { get; set; }
        public static string MessageBody { get; set; }
        public static string Subject { get; set; }

        public static void SendEmail()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            NetworkCredential creds = new NetworkCredential(FromAddress, "Gvb@Admin");
            smtpClient.Credentials = creds;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Port = 587;
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(FromAddress, "GVB Expense Reimbursement", System.Text.Encoding.UTF8);
            mail.To.Add(new MailAddress(ToAddress));
            mail.Body = MessageBody + "\n\n\n ******This is system generated email. Please do not reply to this email address.*****";
            mail.Subject = Subject;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Priority = MailPriority.High;
            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.Write(ex.InnerException.Message);
            }
            
        }
    }
}