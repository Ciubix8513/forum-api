using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> option) : base(option) { }
        public DbSet<User> User { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PReport> pReport { get; set; }
        public DbSet<UReport> uReport { get; set; }
    }
}