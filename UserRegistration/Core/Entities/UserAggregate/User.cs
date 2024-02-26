namespace Core.Entities.UserAggregate;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    //public long UserId { get; set; }
    //public User UserEntity { get; set; } = null!;

}