using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Twileloop.UOW.Repository;
using Twileloop.UOW.Repository.Base;

namespace Twileloop.UOW {
    // Defines a class that represents a Unit of Work (UoW) pattern for database operations.
    // Implements the IDisposable interface to ensure proper resource cleanup.
    public class UnitOfWork : IDisposable {
        // The LiteDbContext instance used to access the database.
        private readonly List<LiteDbContext> _dbContexts;
        // The LiteDbContext instance used to access the database.
        private LiteDbContext _dbContext;

        // Initializes a new instance of the UnitOfWork class using the specified connection string.
        // The connection string represents the path to the database file.
        public UnitOfWork(List<LiteDbContext> contexts) {
            _dbContexts = contexts;
            if (!contexts.Any()) {
                throw new ArgumentException($"You have to register atleast one LiteDB database");
            }
            if (contexts.Count == 1) {
                _dbContext = contexts.FirstOrDefault();
            }
        }

        //Uses a specific database for operations
        public void UseDatabase(string dbName) {
            _dbContext = _dbContexts.Where(x => x.DatabaseName == dbName).FirstOrDefault();
            if (_dbContext is null) {
                throw new ArgumentException($"Can't find a registered database with name: {dbName}");
            }
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
