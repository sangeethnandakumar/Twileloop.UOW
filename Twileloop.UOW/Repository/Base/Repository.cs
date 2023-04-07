using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AuthServer.Repository.Base {
    // Defines a generic repository class to perform basic CRUD operations on a database using LiteDB.
    // The type parameter T represents the entity type that the repository is designed to work with.
    // The T type is constrained to be a class and have a public parameterless constructor.
    public class Repository<T> where T : class, new() {
        private readonly LiteDbContext _dbContext;   // An instance of the LiteDbContext class used to access the database.
        private readonly ILiteCollection<T> _collection;   // Represents the collection of entities.

        // Initializes a new instance of the Repository class.
        // Takes an instance of the LiteDbContext class, which is used to access the LiteDB database.
        public Repository(LiteDbContext dbContext) {
            _dbContext = dbContext;
            _collection = _dbContext.Database.GetCollection<T>();  // Initializes the _collection field with the collection of entities in the database.
        }

        // Retrieves a single entity from the database based on the specified identifier.
        public T GetById(Guid id) {
            return _collection.FindById(id);
        }

        // Retrieves all entities of the specified type from the database.
        public IEnumerable<T> GetAll() {
            return _collection.FindAll();
        }

        // Retrieves a collection of entities that match the specified filter expression.
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) {
            return _collection.Find(predicate);
        }

        // Inserts a single entity into the database.
        public void Add(T entity) {
            _collection.Insert(entity);
        }

        // Inserts a collection of entities into the database.
        public void AddRange(IEnumerable<T> entities) {
            _collection.InsertBulk(entities);
        }

        // Updates a single entity in the database.
        public bool Update(T entity) {
            return _collection.Update(entity);
        }

        // Updates a collection of entities in the database.
        public void UpdateRange(IEnumerable<T> entities) {
            foreach (var entity in entities) {
                _collection.Update(entity);
            }
        }

        // Deletes a single entity from the database based on the specified identifier.
        public bool Delete(Guid id) {
            return _collection.Delete(id);
        }

        // Deletes a collection of entities from the database based on the specified filter expression.
        public int Delete(Expression<Func<T, bool>> predicate) {
            return _collection.DeleteMany(predicate);
        }

        // Deletes all entities of the specified type from the database.
        public int DeleteAll() {
            return _collection.DeleteMany(_ => true);
        }

        // Retrieves the number of entities of the specified type in the database.
        public long Count() {
            return _collection.Count();
        }

        // Retrieves the number of entities that match the specified filter expression in the database.
        public long Count(Expression<Func<T, bool>> predicate) {
            return _collection.Count(predicate);
        }

        // Checks if any entities of the specified type exist in the database that match the specified filter expression.
        public bool Exists(Expression<Func<T, bool>> predicate) {
            return _collection.Exists(predicate);
        }
    }
}