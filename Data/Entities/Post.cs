namespace Api.Data.Entities;

public class Post
{
    public int Id {get; set;}
    public int CreatorId {get; set;}
    public int ParentPostId {get; set;}
    public string Contents {get; set;}
    public DateTime Date {get; set;}
}