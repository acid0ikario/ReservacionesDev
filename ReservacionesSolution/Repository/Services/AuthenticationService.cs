using BusinessServices.Interfaces;
using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessServices.Services
{
   public class AuthenticationService: IAuthenticationService
    {
        private readonly string _secret;
        public AuthenticationService(IConfiguration config)
        {
            _secret = config.GetValue<string>("DevSecretKey");
        }

        public string GenerateSecurityToken(Usuarios AuthUsers)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, AuthUsers.UserId),
                    new Claim(ClaimTypes.Role, AuthUsers.RolId)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
