using Passhub.Domain.Identity;

namespace Passhub.EfCore.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<ApplicationUser> GetAsync(Guid id)
    {
        var user = await context.FindAsync<ApplicationUser>(id);
        if (user == null)
        {
            throw new NullReferenceException(nameof(user));
        }
        return user;
    }
}