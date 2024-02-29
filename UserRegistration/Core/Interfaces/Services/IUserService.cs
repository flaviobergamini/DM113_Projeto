using Ardalis.Result;
using Core.Entities.UserAggregate;
using Core.Interfaces.Services.Base;

namespace Core.Interfaces.Services;

public interface IUserService : IBaseService<User>
{
    Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<Result<User>> LoginUserAsync(string email, string password, CancellationToken cancellationToken);
    Task<Result<User>> UpdateUserAsync(string id, User user, CancellationToken cancellationToken);
    Task<Result<User>> DeleteUserByIdAsync(string id, CancellationToken cancellationToken);
    Task<Result<User?>> GetUserByIdAsync(string id, CancellationToken cancellationToken);

}