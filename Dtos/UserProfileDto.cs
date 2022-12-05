namespace Api.Dtos;
public class UserProfileDto
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string BIO { get; set; }
    public bool Privilege { get; set; }
    public string PFP { get; set; }
    public UserProfileDto(int userID, string username, string bIO, bool privilege, string pFP)
    {
        UserID = userID;
        Username = username;
        BIO = bIO;
        Privilege = privilege;
        PFP = pFP;
    }
}