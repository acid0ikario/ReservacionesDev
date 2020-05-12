using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _account;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountRepository account, IConfiguration config)
        {
            _account = account;
            _configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] Usuarios usuario)
        {
            Usuarios objUsuario = _account.AuthenticateUser(usuario.UserId, usuario.Password);
            if (objUsuario != null)
            {
                return BuildToken(objUsuario);
            }
            return Unauthorized("Usuario o contraseñas incorrectas");
        }

        private IActionResult BuildToken(Usuarios user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId),
                new Claim("RolId", user.RolId)
            };

            string secretKey = _configuration.GetValue<string>("DevSecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "syntepro.com",
               audience: "syntepro.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            });

        }
    }
}