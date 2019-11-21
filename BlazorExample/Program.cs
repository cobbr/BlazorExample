using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using BlazorExample.Models;

namespace BlazorExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MyContext>();
                context.Database.EnsureCreated();
                if (!context.ListenerTypes.Any())
                {
                    var types = new List<ListenerType>
                    {
                        new ListenerType { Name = "test1", Description = "description1" },
                        new ListenerType { Name = "test2", Description = "description2" }
                    };
                    context.ListenerTypes.AddRange(types);
                    context.SaveChanges();
                }
                if (!context.Listeners.Any())
                {
                    var listeners = new List<Listener>
                    {
                        new Listener { ListenerType = context.ListenerTypes.First(), Name = "Listener1" },
                        new Listener { ListenerType = context.ListenerTypes.Skip(1).First(), Name = "Listener2" }
                    };
                    context.Listeners.AddRange(listeners);
                    context.SaveChanges();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
