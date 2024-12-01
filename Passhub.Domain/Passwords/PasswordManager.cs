using System.Text;
using Passhub.Domain.Identity;

namespace Passhub.Domain.Passwords;

public class PasswordManager(IPasswordRepository passwordRepository,
    IPasswordEncryptor passwordEncryptor,
    IUserRepository userRepository) : IPasswordManager
{
    public async Task EncryptAsync(Password password, string value)
    {
        var user = await userRepository.GetAsync(password.UserId);

        if (user.Email == null)
        {
            throw new NullReferenceException(nameof(user.Email));
        }

        if (user.PasswordHash == null)
        {
            throw new NullReferenceException(nameof(user.PasswordHash));
        }
        
        var passwordEncrypt = passwordEncryptor
            .Encrypt(value, user.PasswordHash, Encoding.UTF8.GetBytes(user.Email));

        if (passwordEncrypt != null)
        {
            password.Value = passwordEncrypt;
            await passwordRepository.InsertAsync(password);
        }
    }

    public Task<string?> DecryptAsync(Password password)
    {
        throw new NotImplementedException();
    }
}