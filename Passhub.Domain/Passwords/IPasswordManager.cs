namespace Passhub.Domain.Passwords;

public interface IPasswordManager
{
    Task EncryptAsync(Password password, string value);
    Task<string?> DecryptAsync(Password password);
}