using MongoDB.Driver;

namespace Twileloop.UOW.MongoDB.Core
{
    public class MongoDBContext
    {
        public IMongoDatabase Database { get; }
        public string DatabaseName { get; }

        public MongoDBContext(string dbName, IMongoDatabase database)
        {
            Database = database;
            DatabaseName = dbName;
        }
    }
}
