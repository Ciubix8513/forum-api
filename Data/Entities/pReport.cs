namespace Api.Data.Entities;

public class PReport
{
    public PReport(int id, int postId, string reason)
    {
        Id = id;
        PostId = postId;
        Reason = reason;
    }

    public int Id {get; set;}
    public int PostId {get; set;}
    public string Reason {get; set;}
} 