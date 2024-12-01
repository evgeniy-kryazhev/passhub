using Passhub.Domain.Identity;

namespace Passhub.Web.Services.Identity;

public interface ICurrentUser
{
    bool IsAuth();
    Guid GetId();
    string GetEmail();

    Task<ApplicationUser> GetAsync();
}