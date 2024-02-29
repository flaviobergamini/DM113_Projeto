using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Models;

public class CreateUserRequestModel
{
    [Required] public string Name { get; set; } = null!;
    [Required] public string Surname { get; set; } = null!;
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}