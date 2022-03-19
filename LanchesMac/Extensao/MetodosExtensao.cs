﻿using LanchesMac.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace LanchesMac.Extensao
{
    public static class MetodosExtensao
    {
        public static IHost CreateAdminRole(this IHost host)
        {
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    SeedData.CreateRoles(serviceProvider, configuration).Wait();
                }
                catch (Exception exception)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "Ocorreu um erro na criação dos perfis dos usuários");
                }
            }
            return host;


        }
    }
}
