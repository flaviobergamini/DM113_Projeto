﻿namespace Core.Entities.UserAggregate;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string RefreshToken { get; set; } = string.Empty;
}