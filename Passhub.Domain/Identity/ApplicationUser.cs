using Microsoft.AspNetCore.Identity;
using Passhub.Domain.Passwords;

namespace Passhub.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public List<Password> Passwords { get; set; } = [];
}