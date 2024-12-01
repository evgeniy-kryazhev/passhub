using System.ComponentModel.DataAnnotations;

namespace Passhub.Web.ViewModels;

public class PasswordFormInput
{
    [Required] [MinLength(length: 1)] public string Name { get; set; } = "";

    [Required] [DataType(DataType.Url)] public string Url { get; set; } = "";

    [Required] public string Login { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}