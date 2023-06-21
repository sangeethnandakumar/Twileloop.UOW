using LiteDB;

namespace Twileloop.UOW.LiteDB.Core
{
    // Defines a class that represents a LiteDB database context.
    // Implements the IDisposable interface to ensure proper resource cleanup.
    public class LiteDBContext
    {
        public LiteDatabase Database { get; }
        public string DatabaseName { get; }

        public LiteDBContext(string dbName, LiteDatabase database)
        {
            Database = database;
            DatabaseName = dbName;
        }
    }
}
