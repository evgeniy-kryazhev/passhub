using System.Security.Claims;
using Passhub.Domain.Identity;

namespace Passhub.Web.Services.Identity;

public class CurrentUser(
    IHttpContextAccessor contextAccessor,
    IUserRepository userRepository) : ICurrentUser
{
    public bool IsAuth()
    {
        if (contextAccessor.HttpContext == null)
        {
            throw new UnauthorizedAccessException();
        }

        var claims = contextAccessor.HttpContext.User;
        return claims.Identity?.IsAuthenticated ?? false;
    }

    public Guid GetId()
    {
        if (contextAccessor.HttpContext == null)
        {
            throw new UnauthorizedAccessException();
        }

        var claims = contextAccessor.HttpContext.User;
        var idClaim = claims.FindFirst(ClaimTypes.NameIdentifier);
        if (idClaim == null)
        {
            throw new UnauthorizedAccessException();
        }

        return Guid.Parse(idClaim.Value);
    }

    public string GetEmail()
    {
        if (contextAccessor.HttpContext == null)
        {
            throw new UnauthorizedAccessException();
        }

        var claims = contextAccessor.HttpContext.User;
        var emailClaim = claims.FindFirst(ClaimTypes.Email);
        if (emailClaim == null)
        {
            throw new UnauthorizedAccessException();
        }

        return emailClaim.Value;
    }

    public async Task<ApplicationUser> GetAsync()
    {
        return await userRepository.GetAsync(GetId());
    }
}