using LiteDB;

namespace Twileloop.UOW.Repository
{
    // Defines a class that represents a LiteDB database context.
    // Implements the IDisposable interface to ensure proper resource cleanup.
    public class LiteDbContext
    {
        public LiteDatabase Database { get; }
        public string DatabaseName { get; }

        public LiteDbContext(string dbName, LiteDatabase database)
        {
            Database = database;
            DatabaseName = dbName;
        }
    }
}
