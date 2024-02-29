namespace UserRegistration.Models;

public class UpdateUserRequestModel
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
}