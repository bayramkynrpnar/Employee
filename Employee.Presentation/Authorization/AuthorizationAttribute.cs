using Employe.DataAccess;
using Employee.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Presentation.Authorization
{
    public class AuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var token = context.HttpContext.Request.Cookies.Keys.FirstOrDefault(x => x.Contains("UserToken"));
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectResult("http://localhost:5000/Login/Login");
            }


                    }
    }
}
