using Core.Entities.UserAggregate;
using Core.Interfaces.Repositories.Base;

namespace Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> UpdateUserAsync(string id, User user, CancellationToken cancellationToken);
    Task<bool> DeleteUserByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<User>> ListAllUsersAsync(CancellationToken cancellationToken);
}