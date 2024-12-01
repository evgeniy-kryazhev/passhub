namespace Passhub.Domain.Passwords;

public class Password(Guid id, Guid userId, string name, string url, string login, string value)
{
    public Guid Id { get; set; } = id;
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public string Url { get; set; } = url;
    public string Login { get; set; } = login;
    public string Value { get; set; } = value;
}