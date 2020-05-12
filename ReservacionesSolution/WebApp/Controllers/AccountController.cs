using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.AspNetCore.Session;
using WebApp.Extensions;
using WebApp.Models;
using WebApp.Utilidades;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            TokenResponse _token = new TokenResponse();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44312/api/Account/Authenticate");
            var responseTask = client.PostAsJsonAsync(client.BaseAddress.AbsoluteUri, usuario);
            responseTask.Wait();
            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TokenResponse>();
                readTask.Wait();

                _token = readTask.Result;
                HttpContext.Session.SetObject(Constantes.TokenSession, _token);
                SetCokie(Constantes.TokenSession, _token.Token);
                return RedirectToAction("Index","Reservaciones");
            }
            else
            {
                var response = result.Content.ReadAsStringAsync();
                ModelState.AddModelError(result.StatusCode.ToString(), response.Result);
            }
            return View(usuario);

        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void SetCokie(string key, string value)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(key, value, option);
        }
    }
}