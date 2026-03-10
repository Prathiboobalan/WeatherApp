using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorApp.Models
{
    public class MongoUser
    {
        

        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}

