using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Controllers;
using WebApp.Extensions;
using WebApp.Models;
using WebApp.Utilidades;

namespace WebApp.Filters
{
    public class AuthenticateAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            TokenResponse _token = context.HttpContext.Session.GetObject<TokenResponse>(Constantes.TokenSession);
            if (_token== null)
            {
                var controller = (BaseController)context.Controller;
                context.Result = controller.RedirectToAction("Login", "Account");

            }
           
        }

       
    }
}
