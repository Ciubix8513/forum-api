namespace Api.Helpers;

public static class MailSender
{
    public static async Task SendEmail(string to,string subject, string body)
    {
        // var key = Environment.GetEnvironmentVariable("SG_KEY");
        // var client = new SendGridClient(key);
        // var from = new EmailAddress("noreply@ciubix.xyz");
        // var msg = MailHelper.CreateSingleEmail(from,  new EmailAddress(to), subject, body, body);
        // var response = await client.SendEmailAsync(msg);
        
    }
}