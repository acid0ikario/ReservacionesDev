using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using WebApp.APIutilis;
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
            var result = ApiManager.PostAsync(EndPointsManager.AUTHENTICATE, usuario);
            var contentResult =  result.Content.ReadAsStringAsync();
            contentResult.GetAwaiter();
            if (result.IsSuccessStatusCode)
            {
                var _token = JsonConvert.DeserializeObject<TokenResponse>(contentResult.Result);
                HttpContext.Session.SetObject(Constantes.TokenSession, _token);
                SetCokie(Constantes.TokenSession, _token.Token);
                return RedirectToAction("Index","Reservaciones");
            }
            else
            {
               
                ModelState.AddModelError(result.StatusCode.ToString(), contentResult.Result);
            }
            return View(usuario);

        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        private void SetCokie(string key, string value)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(key, value, option);
        }
    }
}