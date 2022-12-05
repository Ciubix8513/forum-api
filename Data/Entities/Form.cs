namespace Api.Data.Entities;

public class Form
{
    public Form(int id, string? username, string? email, string? password, string? reason, DateTime? date)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        Reason = reason;
        Date = date;
    }

    public int Id {get; set;}
    public string? Username {get; set;}
    public string? Email {get; set;}
    public string? Password {get; set;}
    public string? Reason {get; set;}
    public DateTime? Date {get; set;}
}