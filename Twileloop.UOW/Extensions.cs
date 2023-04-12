using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Twileloop.UOW.Repository;

namespace Twileloop.UOW {

    public static class Extensions {
        public static void AddUnitOfWork(this IServiceCollection services, Action<UOWOptions>? options = null) {
            // Create a new instance of UOWOptions and configure it using the provided delegate
            var uowOptions = new UOWOptions();
            options?.Invoke(uowOptions);

            //Register all DB connections at once
            var contexts = new List<LiteDbContext>();
            foreach (var conn in uowOptions.Connections) {
                contexts.Add(new LiteDbContext(conn.Name, conn.ConnectionString));
            }
            services.AddSingleton(contexts);

            // Register the UnitOfWork service for each connection
            services.AddSingleton<UnitOfWork>();
        }
    }
}
