namespace Api.Dtos;

public class PostsGetDto
{
    public PostsGetDto(int id, int? creatorId, string? creatorUsername, string? creatorPFP, int? parentPostId, string? contents, DateTime? date, int? replyCount)
    {
        Id = id;
        CreatorId = creatorId;
        CreatorUsername = creatorUsername;
        CreatorPFP = creatorPFP;
        ParentPostId = parentPostId;
        Contents = contents;
        Date = date;
        ReplyCount = replyCount;
    }

    public int Id {get; set;}
    public int? CreatorId {get; set;}
    public string? CreatorUsername {get;set;}
    public string? CreatorPFP {get;set;}
    public int? ParentPostId {get; set;}
    public string? Contents {get; set;}
    public DateTime? Date {get; set;}
    public int? ReplyCount {get;set;}
}