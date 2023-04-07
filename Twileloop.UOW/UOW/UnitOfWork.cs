using AuthServer.Repository.Base;
using AuthServer.Repository;
using System;

namespace AuthServer.UOW {
    // Defines a class that represents a Unit of Work (UoW) pattern for database operations.
    // Implements the IDisposable interface to ensure proper resource cleanup.
    public class UnitOfWork : IDisposable {
        private readonly LiteDbContext _dbContext;  // The LiteDbContext instance used to access the database.

        // Initializes a new instance of the UnitOfWork class using the specified connection string.
        // The connection string represents the path to the database file.
        public UnitOfWork(string connectionString) {
            _dbContext = new LiteDbContext(connectionString); // Initializes a new LiteDbContext instance using the connection string.
        }

        // Retrieves a repository instance of the specified entity type.
        public Repository<T> GetRepository<T>() where T : class, new() {
            return new Repository<T>(_dbContext); // Returns a new Repository instance initialized with the current LiteDbContext instance.
        }

        // Begins a new transaction.
        public void BeginTransaction() {
            _dbContext.Database.BeginTrans(); // Starts a new transaction using the LiteDatabase instance.
        }

        // Commits the current transaction.
        public void Commit() {
            _dbContext.Database.Commit(); // Commits the current transaction using the LiteDatabase instance.
        }

        // Rolls back the current transaction.
        public void Rollback() {
            _dbContext.Database.Rollback(); // Rolls back the current transaction using the LiteDatabase instance.
        }

        // Disposes the LiteDbContext instance to release any unmanaged resources.
        public void Dispose() {
            _dbContext.Dispose(); // Disposes the LiteDbContext instance when it is no longer needed.
        }
    }
}
