using Core.Entities;
using Core.Interfaces.Repositories.Base;

namespace Infrastructure.Repositories.Base;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
}