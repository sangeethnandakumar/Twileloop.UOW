using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using Twileloop.UOW.LiteDB.Core;
using Twileloop.UOW.Repository;

namespace Twileloop.UOW.LiteDB.Support
{

    public static class Extensions
    {
        public static void AddUnitOfWork(this IServiceCollection services, Action<Options>? options = null)
        {
            var uowOptions = new Options();
            options?.Invoke(uowOptions);

            var contexts = new ConcurrentDictionary<string, LiteDBContext>();
            foreach (var conn in uowOptions.Connections)
            {
                var db = new LiteDatabase(conn.ConnectionString);
                contexts.TryAdd(conn.Name, new LiteDBContext(conn.Name, db));
            }

            services.AddSingleton(contexts);
            services.AddScoped<UnitOfWork>();
        }
    }
}
