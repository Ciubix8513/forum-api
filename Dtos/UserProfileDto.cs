public class UserProfileDto
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string BIO { get; set; }
    public string Email { get; set; }
    public bool Privilege { get; set; }
    public string PFP { get; set; }
    public UserProfileDto(int userID, string username, string bIO, string email, bool privilege, string pFP)
    {
        UserID = userID;
        Username = username;
        BIO = bIO;
        Email = email;
        Privilege = privilege;
        PFP = pFP;
    }
}