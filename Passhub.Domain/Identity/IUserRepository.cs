namespace Passhub.Domain.Identity;

public interface IUserRepository
{
    Task<ApplicationUser> GetAsync(Guid id);
}