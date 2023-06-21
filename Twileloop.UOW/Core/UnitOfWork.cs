using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Twileloop.UOW.LiteDB.Core
{
    // Defines a class that represents a Unit of Work (UoW) pattern for database operations.
    // Implements the IDisposable interface to ensure proper resource cleanup.
    public class UnitOfWork
    {
        private readonly ConcurrentDictionary<string, LiteDBContext> _dbContexts;
        private string _currentDbName;

        public UnitOfWork(ConcurrentDictionary<string, LiteDBContext> contexts)
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

        public Repository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_dbContexts[_currentDbName]);
        }

        public void BeginTransaction()
        {
            _dbContexts[_currentDbName].Database.BeginTrans();
        }

        public void Commit()
        {
            _dbContexts[_currentDbName].Database.Commit();
        }

        public void Rollback()
        {
            _dbContexts[_currentDbName].Database.Rollback();
        }
    }
}
