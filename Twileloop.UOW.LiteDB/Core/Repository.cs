using Humanizer;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Twileloop.UOW.LiteDB.Core
{
    public class Repository<T> where T : class, new()
    {
        private readonly LiteDBContext _dbContext;
        private readonly ILiteCollection<T> _collection;

        public Repository(LiteDBContext dbContext)
        {
            _dbContext = dbContext;
            _collection = _dbContext.Database.GetCollection<T>(typeof(T).Name.Pluralize(inputIsKnownToBeSingular: false));
        }

        public T GetById(Guid id)
        {
            return _collection.FindById(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.FindAll();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _collection.Find(predicate);
        }

        public void Add(T entity)
        {
            _collection.Insert(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _collection.InsertBulk(entities);
        }

        public bool Update(T entity)
        {
            return _collection.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _collection.Update(entity);
            }
        }

        public bool Delete(Guid id)
        {
            return _collection.Delete(id);
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return _collection.DeleteMany(predicate);
        }

        public int DeleteAll()
        {
            return _collection.DeleteMany(_ => true);
        }

        public long Count()
        {
            return _collection.Count();
        }

        public long Count(Expression<Func<T, bool>> predicate)
        {
            return _collection.Count(predicate);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _collection.Exists(predicate);
        }
    }
}