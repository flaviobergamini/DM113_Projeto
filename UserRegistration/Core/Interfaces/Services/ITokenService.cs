using Core.Entities.UserAggregate;

namespace Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();

}