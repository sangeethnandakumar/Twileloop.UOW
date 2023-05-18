using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using Twileloop.UOW.Repository;

namespace Twileloop.UOW
{

    public static class Extensions
    {
        public static void AddUnitOfWork(this IServiceCollection services, Action<UOWOptions>? options = null)
        {
            var uowOptions = new UOWOptions();
            options?.Invoke(uowOptions);

            var contexts = new ConcurrentDictionary<string, LiteDbContext>();
            foreach (var conn in uowOptions.Connections)
            {
                var db = new LiteDatabase(conn.ConnectionString);
                contexts.TryAdd(conn.Name, new LiteDbContext(conn.Name, db));
            }

            services.AddSingleton(contexts);
            services.AddScoped<UnitOfWork>();
        }
    }
}
