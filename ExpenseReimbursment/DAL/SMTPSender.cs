using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ExpenseReimbursment.DAL
{
    public static class SMTPSender
    {
        public static string FromAddress = "saitejajoshi@gmail.com";
        public static string ToAddress { get; set; }
        public static string MessageBody { get; set; }
        public static string Subject { get; set; }

        public static void SendEmail()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Credentials = new System.Net.NetworkCredential(FromAddress, "Steja#479");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(FromAddress, "GVB Expense Reimbursement");
            mail.To.Add(new MailAddress(ToAddress));
            mail.Bcc.Add(new MailAddress("gutta@ucmo.edu"));
            mail.Body = MessageBody + "\n\n\n ******This is system generated email. Please do not reply to this email address.*****";
            mail.Subject = Subject;

            smtpClient.Send(mail);
        }
    }
}