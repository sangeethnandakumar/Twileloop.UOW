using LiteDB;
using System;

namespace Twileloop.UOW.Repository {
    // Defines a class that represents a LiteDB database context.
    // Implements the IDisposable interface to ensure proper resource cleanup.
    public class LiteDbContext : IDisposable {
        // The LiteDatabase instance used to access the database.
        public LiteDatabase Database { get; }

        // Initializes a new instance of the LiteDbContext class using the specified connection string.
        // The connection string represents the path to the database file.
        public LiteDbContext(string connectionString) {
            Database = new LiteDatabase(connectionString); // Initializes a new LiteDatabase instance using the connection string.
        }

        // Disposes the LiteDatabase instance to release any unmanaged resources.
        public void Dispose() {
            Database.Dispose();
        }
    }
}
