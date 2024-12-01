namespace Passhub.Domain.Passwords;

public interface IPasswordEncryptor
{
    string? Encrypt(string plainText, string passPhrase, byte[] salt);
    string? Decrypt(string cipherText, string passPhrase, byte[] salt);
}