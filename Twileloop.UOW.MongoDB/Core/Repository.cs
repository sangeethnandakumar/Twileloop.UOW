using Humanizer;
using LiteDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Twileloop.UOW.MongoDB.Support;

namespace Twileloop.UOW.MongoDB.Core
{
    public class Repository<T> where T : EntityBase, new()
    {
        private readonly MongoDBContext _dbContext;
        private readonly IMongoCollection<T> _collection;

        public Repository(MongoDBContext dbContext)
        {
            _dbContext = dbContext;
            _collection = _dbContext.Database.GetCollection<T>(typeof(T).Name.Pluralize(inputIsKnownToBeSingular: false));
        }

        public T GetById(Guid id)
        {
            return _collection.FindSync(Builders<T>.Filter.Eq("_id", id)).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.FindSync(Builders<T>.Filter.Empty).ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _collection.FindSync(predicate).ToList();
        }

        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _collection.InsertMany(entities);
        }

        public bool Update(T entity)
        {
            var result = _collection.ReplaceOne(Builders<T>.Filter.Eq("_id", GetEntityId(entity)), entity);
            return result.ModifiedCount > 0;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _collection.ReplaceOne(Builders<T>.Filter.Eq("_id", GetEntityId(entity)), entity);
            }
        }

        public bool Delete(Guid id)
        {
            var result = _collection.DeleteOne(Builders<T>.Filter.Eq("_id", id));
            return result.DeletedCount > 0;
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            var result = _collection.DeleteMany(predicate);
            return (int)result.DeletedCount;
        }

        public int DeleteAll()
        {
            var result = _collection.DeleteMany(Builders<T>.Filter.Empty);
            return (int)result.DeletedCount;
        }

        public long Count()
        {
            return _collection.CountDocuments(Builders<T>.Filter.Empty);
        }

        public long Count(Expression<Func<T, bool>> predicate)
        {
            return _collection.CountDocuments(predicate);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _collection.Find(predicate).Any();
        }

        private object GetEntityId(T entity)
        {
            return entity.Id;
        }
    }
}