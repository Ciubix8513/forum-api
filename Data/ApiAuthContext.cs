using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApiAuthContext : DbContext
    {
        public ApiAuthContext(DbContextOptions<ApiAuthContext> option) : base(option)
        {

        }
        public DbSet<User> User { get; set; }
    }
}