using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Modules.Users.Core.Models;

public class User : IdentityUser<Guid>
{
    public override string Email { get; set; }
}