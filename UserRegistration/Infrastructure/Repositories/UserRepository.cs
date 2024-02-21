using Core.Entities.UserAggregate;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
}