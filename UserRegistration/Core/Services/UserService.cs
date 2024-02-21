using Core.Entities.UserAggregate;
using Core.Interfaces.Services;
using Core.Services.Base;

namespace Core.Services;

public class UserService : BaseService<User>, IUserService
{
}