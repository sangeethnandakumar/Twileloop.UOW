using System;
using System.Collections.Concurrent;
using System.Linq;
using Twileloop.UOW.MongoDB.Support;

namespace Twileloop.UOW.MongoDB.Core
{
    // Defines a class that represents a Unit of Work (UoW) pattern for database operations.
    // Implements the IDisposable interface to ensure proper resource cleanup.
    public class UnitOfWork
    {
        private readonly ConcurrentDictionary<string, MongoDBContext> _dbContexts;
        private string _currentDbName;

        public UnitOfWork(ConcurrentDictionary<string, MongoDBContext> contexts)
        {
            _dbContexts = contexts;
            if (!_dbContexts.Any())
            {
                throw new ArgumentException($"You have to register at least one LiteDB database");
            }
            if (_dbContexts.Count == 1)
            {
                _currentDbName = _dbContexts.Keys.FirstOrDefault();
            }
        }

        public void UseDatabase(string dbName)
        {
            _currentDbName = _dbContexts.ContainsKey(dbName) ? dbName : throw new ArgumentException($"Can't find a registered database with name: {dbName}");
        }

        public Repository<T> GetRepository<T>() where T : EntityBase, new()
        {
            return new Repository<T>(_dbContexts[_currentDbName]);
        }
    }
}
