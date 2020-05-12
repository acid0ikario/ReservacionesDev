using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    /// <summary>
    /// Clase que me sirve para poder evitar hacer DI y obtener valores del appSetting de forma estatica
    /// https://stackoverflow.com/questions/46940710/getting-value-from-appsettings-json-in-net-core
    /// </summary>
    public static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
