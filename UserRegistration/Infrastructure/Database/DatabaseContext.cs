using Core.Entities.UserAggregate;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Database;

public class DatabaseContext
{
    protected readonly IMongoCollection<User> _userCollection;

    public DatabaseContext(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _userCollection= mongoDatabase.GetCollection<User>(databaseSettings.Value.UserCollectionName);
    }
}