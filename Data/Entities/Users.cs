namespace Api.Data.Entities
{
    public class User
    {
        public User(int id, string? username, string? email, string? password, bool privilege, string? pFP, string? bIO)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Privilege = privilege;
            PFP = pFP;
            BIO = bIO;
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Privilege { get; set; }
        public string? PFP { get; set; }
        public string? BIO {get; set;}
    }
}