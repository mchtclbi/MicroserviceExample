using System;
using System.Linq;
using MongoDB.Driver;
using System.Linq.Expressions;
using Neredekal.Data.Entities;
using Neredekal.Data.Interfaces;
using System.Collections.Generic;

namespace Neredekal.Data.Concretes
{
    public class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity, new()
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoRepository()
        {
            if (_mongoDatabase == null)
                _mongoDatabase = new MongoClient("connection string").GetDatabase("db name");
        }

        public void Add(T entity) => GetCollection().InsertOne(entity);
        public void Delete(T entity) => GetCollection().DeleteOne(q => q.Id == entity.Id);
        public void Update(T entity) => GetCollection().ReplaceOne(q => q.Id == entity.Id, entity);

        public IQueryable<T> GetQueryable() => GetCollection().AsQueryable();
        public T Get(Expression<Func<T, bool>> filter) => GetCollection().Find(filter).FirstOrDefault();
        public List<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null
                ? GetCollection().Find(q => true).ToList()
                : GetCollection().Find(filter).ToList();
        }

        private IMongoCollection<T> GetCollection() => _mongoDatabase.GetCollection<T>(typeof(T).Name);
    }
}