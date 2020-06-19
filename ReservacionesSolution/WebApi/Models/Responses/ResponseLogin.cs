using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Responses
{
    public class ResponseLogin
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
