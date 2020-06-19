using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Helpers
{

    public  class TokenManipulations
    {

        private readonly string _secretKey;
        private readonly string _token;
        private readonly HttpRequest _httpRequest;

        public TokenManipulations(HttpRequest request)
        {
            _httpRequest = request;
            _secretKey = ConfigurationManager.AppSetting["DevSecretKey"];
            _token = GetTokenAuth();
        }

        public bool IsAdminUser()
        {
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claims = handler.ValidateToken(_token, validations, out var tokenSecure);

            if (claims.Claims.FirstOrDefault(x => x.Type == "RolId").Value == "ADM")
                return true;

            return false;
           
        }

        public string GetLoggedUser()
        {
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(_token, validations, out var tokenSecure);
            return claims.Identity.Name;
            
        }


        public string GetTokenAuth()
        {
            var header = _httpRequest.Headers.First(x => x.Key == "Authorization");
            string token = header.Value;
            token = token.Replace("Bearer ", "");
            return token;
        }

    }

}
