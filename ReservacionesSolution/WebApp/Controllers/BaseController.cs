using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models;
using WebApp.Utilidades;

namespace WebApp.Controllers
{
    public class BaseController: Controller
    {
        public new RedirectToActionResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }

        public string GetTokenRequest()
        {
            string token = "";
            TokenResponse _token = HttpContext.Session.GetObject<TokenResponse>(Constantes.TokenSession);
            if (_token.Token == null)
            {
                 token = Request.Cookies[Constantes.TokenSession].ToString();
            }
            else
            {
                token = _token.Token;
            }
            return token;
        }
    }
}
