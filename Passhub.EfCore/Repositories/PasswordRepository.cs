using Microsoft.EntityFrameworkCore;
using Passhub.Domain.Passwords;

namespace Passhub.EfCore.Repositories;

public class PasswordRepository(ApplicationDbContext context) : IPasswordRepository
{
    public async Task<Password?> GetAsync(Guid id)
    {
        return await context.Passwords
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Password password)
    {
        await context.Passwords.AddAsync(password);
        await context.SaveChangesAsync();
    }

    public async Task<List<Password>> GetListAsync(Guid id)
    {
        return await context.Passwords
            .Where(x => x.UserId == id)
            .ToListAsync();
    }

    public async Task DeleteAsync(Password password)
    {
        context.Passwords.Remove(password);
        await context.SaveChangesAsync();
    }
}