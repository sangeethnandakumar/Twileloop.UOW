using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using Twileloop.UOW.MongoDB.Core;

namespace Twileloop.UOW.MongoDB.Support
{

    public static class Extensions
    {
        public static void AddUnitOfWork(this IServiceCollection services, Action<Options>? options = null)
        {
            var uowOptions = new Options();
            options?.Invoke(uowOptions);

            var contexts = new ConcurrentDictionary<string, MongoDBContext>();
            foreach (var conn in uowOptions.Connections)
            {
                var client = new MongoClient(conn.ConnectionString);
                contexts.TryAdd(conn.Name, new MongoDBContext(conn.Name, client.GetDatabase(conn.Name)));
            }

            services.AddSingleton(contexts);
            services.AddSingleton<UnitOfWork>();
        }

        public static ConcurrentDictionary<string, MongoDBContext> BuildDbContext(Action<Options>? options = null)
        {
            var uowOptions = new Options();
            options?.Invoke(uowOptions);

            var contexts = new ConcurrentDictionary<string, MongoDBContext>();
            foreach (var conn in uowOptions.Connections)
            {
                var client = new MongoClient(conn.ConnectionString);
                contexts.TryAdd(conn.Name, new MongoDBContext(conn.Name, client.GetDatabase(conn.Name)));
            }
            return contexts;
        }

    }
}
