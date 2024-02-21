using Core.Entities.AddressAggregate;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
}