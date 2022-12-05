using System.Net;
using System.Net.Mail;
using System.Text;

namespace Api.Helpers;

public static class MailSender
{
    static readonly string _sendingAddress = "Ciubix8514@gmail.com";
    static readonly string _smtpAddress = "smtp.gmail.com";
    static readonly int _smtpPort = 587;
    public static void SendEmail(string Contents, string Subject, string Destination)
    {
        MailMessage msg = new(_sendingAddress, Destination);
        msg.Body = Contents;
        msg.Subject = Subject;
        msg.BodyEncoding = Encoding.UTF8;
        msg.IsBodyHtml = false;
        SmtpClient client = new(_smtpAddress,_smtpPort);
        NetworkCredential credential = new("testemailsender509@gmail.com","Password1!23");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = credential;
        client.Send(msg);
    }
}