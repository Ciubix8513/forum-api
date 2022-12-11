namespace Api.Dtos;

public class PostsGetDto
{
    public PostsGetDto(int id, int creatorId, string creatorUsername, int parentPostId, string contents, DateTime date)
    {
        Id = id;
        CreatorId = creatorId;
        CreatorUsername = creatorUsername;
        ParentPostId = parentPostId;
        Contents = contents;
        Date = date;
    }

    public int Id {get; set;}
    public int CreatorId {get; set;}
    public string CreatorUsername {get;set;}
    public int ParentPostId {get; set;}
    public string Contents {get; set;}
    public DateTime Date {get; set;}
}