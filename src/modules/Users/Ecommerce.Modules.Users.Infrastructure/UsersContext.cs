using Ecommerce.Modules.Users.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Modules.Users.Infrastructure;

public class UsersContext : IdentityDbContext<User, Role, Guid>
{
    public UsersContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Users");
        
        base.OnModelCreating(builder);
    }
}