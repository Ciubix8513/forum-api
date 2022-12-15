namespace Api.Dtos;

public class RepGetDto
{
    public RepGetDto(int repId, int id, string reason)
    {
        RepId = repId;
        Id = id;
        Reason = reason;
    }

    public int RepId{get;set;}
    public int Id {get; set;}
    public string Reason {get; set;}
}