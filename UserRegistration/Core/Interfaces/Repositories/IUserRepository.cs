using Core.Entities.UserAggregate;
using Core.Interfaces.Repositories.Base;

namespace Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
}