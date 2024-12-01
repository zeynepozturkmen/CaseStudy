using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    public DbSet<Core.Entities.User> Users { get; set; }
}
