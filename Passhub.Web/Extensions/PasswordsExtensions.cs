using Passhub.Domain.Passwords;

namespace Passhub.Web.Extensions;

public static class PasswordsExtensions
{
    public static string GetIconWebsite(this Password password)
    {
        var uri = new Uri(password.Url);
        return $"{uri.Scheme}://{uri.Authority.TrimEnd('/')}/favicon.ico";
    }
}