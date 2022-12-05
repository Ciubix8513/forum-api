namespace Api.Dtos;

public class PostsGetDto
{
    public PostsGetDto(int id, int creatorId, int parentPostId, string contents, DateTime date)
    {
        Id = id;
        CreatorId = creatorId;
        ParentPostId = parentPostId;
        Contents = contents;
        Date = date;
    }

    public int Id {get; set;}
    public int CreatorId {get; set;}
    public int ParentPostId {get; set;}
    public string Contents {get; set;}
    public DateTime Date {get; set;}
}