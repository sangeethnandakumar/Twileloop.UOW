namespace Twileloop.UOW.MongoDB.Support
{
    public class MongoDBConnection
    {
        public MongoDBConnection(string name, string connectionString)
        {
            Name = name;
            ConnectionString = connectionString;
        }

        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
