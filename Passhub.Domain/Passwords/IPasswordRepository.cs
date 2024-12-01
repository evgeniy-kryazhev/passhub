namespace Passhub.Domain.Passwords;

public interface IPasswordRepository
{
    Task<Password?> GetAsync(Guid id);
    Task InsertAsync(Password password);
    Task<List<Password>> GetListAsync(Guid id);
    Task DeleteAsync(Password password);
}