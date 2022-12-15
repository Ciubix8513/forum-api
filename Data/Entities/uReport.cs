
namespace Api.Data.Entities;

public class UReport
{
    public UReport(int id, int userId, string? reason)
    {
        Id = id;
        UserId = userId;
        Reason = reason;
    }

    public int Id {get; set;}
    public int UserId {get; set;}
    public string? Reason {get; set;}
} 