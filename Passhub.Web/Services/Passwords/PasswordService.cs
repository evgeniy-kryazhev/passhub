using Passhub.Domain.Passwords;
using Passhub.Web.Services.Identity;

namespace Passhub.Web.Services.Passwords;

public class PasswordService(ICurrentUser currentUser,
    IPasswordRepository passwordRepository,
    IPasswordManager passwordManager) : IPasswordService
{
    public async Task<Password> CreateASync(string name, string login, string url, string password)
    {
        var passwordEntity = new Password(Guid.NewGuid(), currentUser.GetId(), name, url, login, string.Empty);
        await passwordManager.EncryptAsync(passwordEntity, password);
        return passwordEntity;
    }

    public async Task<List<Password>> GetListAsync()
    {
        return await passwordRepository.GetListAsync(currentUser.GetId());
    }

    public async Task DeleteAsync(Guid id)
    {
        var password = await passwordRepository.GetAsync(id);
        if (password != null && password.UserId == currentUser.GetId())
        {
            await passwordRepository.DeleteAsync(password);
        }
    }
}