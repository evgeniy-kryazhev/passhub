using Passhub.Domain.Passwords;

namespace Passhub.Web.Services.Passwords;

public interface IPasswordService
{
    Task<Password> CreateASync(string name, string login, string url, string password);
    Task<List<Password>> GetListAsync();
    Task DeleteAsync(Guid id);
}