namespace Api.Dtos;

public class UserDataDto
{
    public UserDataDto(int id, string username, string? bio, int postCount, string? pFP)
    {
        Id = id;
        Username = username;
        Bio = bio;
        PostCount = postCount;
        PFP = pFP;
    }

    public int Id {get;set;}
    public string Username {get;set;}
    public string? Bio {get;set;}
    public int PostCount {get;set;}
    public string? PFP {get;set;}
}