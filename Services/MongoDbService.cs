using BlazorApp.Models;
using MongoDB.Driver;

public class MongoDbService
{
    private readonly IMongoCollection<MongoUser> _users;

    public MongoDbService(IConfiguration config)
    {
        var client = new MongoClient(config["Mongo:ConnectionString"]);
        var db = client.GetDatabase(config["Mongo:Database"]);
        _users = db.GetCollection<MongoUser>("users");
    }

    public async Task InsertUserAsync(MongoUser user)
    {
        await _users.InsertOneAsync(user);
    }
}

