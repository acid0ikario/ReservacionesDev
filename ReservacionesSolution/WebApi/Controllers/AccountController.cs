using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Repository.Interfaces;
using WebApi.Models.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _account;
       
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAccountRepository account, IAuthenticationService authenticationService)
        {
            _account = account;
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] Usuarios usuario)
        {
            var userAthenticated = _account.AuthenticateUser(usuario.UserId, usuario.Password);
            if (userAthenticated == null)
                return Unauthorized("Usuario o Contraseña Incorrectas");

            var token = _authenticationService.GenerateSecurityToken(userAthenticated);
            ResponseLogin res = new ResponseLogin();
            res.Token = token;
            res.Expire = DateTime.Now.AddDays(30);
            return Ok(res);
        }

        private IActionResult BuildToken(Usuarios user)
        {
            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId),
            //    new Claim("RolId", user.RolId)
            //};

            //string secretKey = _configuration.GetValue<string>("DevSecretKey");
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var expiration = DateTime.UtcNow.AddYears(1);

            //JwtSecurityToken token = new JwtSecurityToken(
            //   issuer: "syntepro.com",
            //   audience: "syntepro.com",
            //   claims: claims,
            //   expires: expiration,
            //   signingCredentials: creds);

            //return Ok(new
            //{
            //    Token = new JwtSecurityTokenHandler().WriteToken(token),
            //    Expiration = expiration
            //});
            return Ok();

        }
    }
}