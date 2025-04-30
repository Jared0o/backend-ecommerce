using Ecommerce.Modules.Users.Core.Models;

namespace Ecommerce.Modules.Users.Core.Services;

public interface ITokenService
{
    public Task<string> GenerateJwtTokenAsync(User user);
}