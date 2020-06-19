using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApp.APIutilis
{
    public static class EndPointsManager
    {
        public static readonly string AUTHENTICATE = Startup.StaticConfiguration.GetSection("EndPoints").GetSection("AUTHENTICATE").Value; 
    }
}
