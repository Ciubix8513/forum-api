namespace Api.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Privilege { get; set; }
        public string? PFP { get; set; }
    }
}