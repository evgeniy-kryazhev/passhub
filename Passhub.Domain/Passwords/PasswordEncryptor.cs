using System.Security.Cryptography;
using System.Text;

namespace Passhub.Domain.Passwords;

public class PasswordEncryptor : IPasswordEncryptor
{
    public string? Encrypt(string plainText, string passPhrase, byte[] salt)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using var password = new Rfc2898DeriveBytes(passPhrase, salt);
        var keyBytes = password.GetBytes(256 / 8);
        using var symmetricKey = Aes.Create();
        symmetricKey.Mode = CipherMode.CBC;
        using var encryptor = symmetricKey.CreateEncryptor(keyBytes, "jkE49230Tf093b42"u8.ToArray());
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        var cipherTextBytes = memoryStream.ToArray();
        return Convert.ToBase64String(cipherTextBytes);
    }

    public string? Decrypt(string cipherText, string passPhrase, byte[] salt)
    {
        var cipherTextBytes = Convert.FromBase64String(cipherText);
        using var password = new Rfc2898DeriveBytes(passPhrase, salt);
        var keyBytes = password.GetBytes(256 / 8);
        using var symmetricKey = Aes.Create();
        symmetricKey.Mode = CipherMode.CBC;
        using var decryptor = symmetricKey.CreateDecryptor(keyBytes, "jkE49230Tf093b42"u8.ToArray());
        using var memoryStream = new MemoryStream(cipherTextBytes);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        var plainTextBytes = new byte[cipherTextBytes.Length];
        var totalReadCount = 0;
        while (totalReadCount < cipherTextBytes.Length)
        {
            var buffer = new byte[cipherTextBytes.Length];
            var readCount = cryptoStream.Read(buffer, 0, buffer.Length);
            if (readCount == 0)
            {
                break;
            }

            for (var i = 0; i < readCount; i++)
            {
                plainTextBytes[i + totalReadCount] = buffer[i];
            }

            totalReadCount += readCount;
        }

        return Encoding.UTF8.GetString(plainTextBytes, 0, totalReadCount);
    }
}