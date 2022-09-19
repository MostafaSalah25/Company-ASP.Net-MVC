using System.Net;
using System.Net.Mail;
using Company.DAL.Entities;
namespace Company.Presentation_Layer.Helper
{
    public class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.sendgrid.net", 587); 

            Client.EnableSsl = true; 
            Client.Credentials = new NetworkCredential("apikey", "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");
            Client.Send("mostafasalah2500@gmail.com", email.To, email.Title, email.Body);

        } 
    } 

}

