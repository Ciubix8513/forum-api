namespace Api.Dtos;

public class FormGetDto
{
    public FormGetDto(int id, string? username, string? reason, DateTime? date)
    {
        Id = id;
        Username = username;
        Reason = reason;
        Date = date;
    }

    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Reason { get; set; }
    public DateTime? Date { get; set; }
}