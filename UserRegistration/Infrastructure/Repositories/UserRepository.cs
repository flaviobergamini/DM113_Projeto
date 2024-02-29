using Core.Entities.UserAggregate;
using Core.Interfaces.Repositories;
using Infrastructure.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UserRepository : DatabaseContext, IUserRepository
{
    public UserRepository(IOptions<DatabaseSettings> databaseSettings) : base(databaseSettings)
    {
    }

    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _userCollection.InsertOneAsync(user, cancellationToken);

        return user;
    }

    public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _userCollection
            .FindAsync(x => x.Id == id && x.Deleted == false)
            .Result.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _userCollection
            .FindAsync(x => x.Email == email && x.Deleted == false)
            .Result.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateUserAsync(string id, User user, CancellationToken cancellationToken)
    {
        var objectId = new ObjectId(id);

        var filter = Builders<User>.Filter.Eq("_id", objectId);

        var result = await _userCollection.ReplaceOneAsync(x => x.Id == id, user);

        if (result.ModifiedCount == 1)
            return true;

        return false;
    }

    public async Task<bool> DeleteUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        var objectId = new ObjectId(id);

        var filter = Builders<User>.Filter.Eq("_id", objectId);

        var result = await _userCollection.DeleteOneAsync(filter, cancellationToken);

        if (result.DeletedCount == 1)
            return true;

        return false;
    }

    public async Task<List<User>> ListAllUsersAsync(CancellationToken cancellationToken)
    {
        return await _userCollection.FindAsync(x => x.Deleted == false).Result.ToListAsync(cancellationToken);
    }


}