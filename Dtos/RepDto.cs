namespace Api.Dtos;

public class RepDto
{
    public RepDto(int rId, int id, string? reason)
    {
        RId = rId;
        Id = id;
        Reason = reason;
    }

    public int RId {get; set;}
    public int Id {get; set;}
    public string? Reason {get; set;}
}