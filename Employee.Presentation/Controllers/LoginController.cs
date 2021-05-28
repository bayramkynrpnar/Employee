using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Employee.Data.Models;
using Employe.DataAccess;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Employee.Presentation.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using Employee.Presentation.Helpers;
using Employee.Presentation.Authorization;

namespace Employee.Presentation.Controllers
{

    public class LoginController : Controller
    {




        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Login Ekranını Getirir
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        /// <summary>
        /// Login olmuş kullanıcının diğer methodlara erişimine izin verilecek
        /// </summary>
        /// <param name="personModels"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(PersonModels personModels)
        {
            personModels.Password = CryptoHelpers.Instance.MD5(personModels.Password);
            var checkUser = LoginUser(personModels.Email, personModels.Password);
            if (checkUser != null)
            {


                var tokenString = TokenFactory.GenerateJSONWebToken(checkUser, _config);// token string oluyor burda

                HttpContext.Response.Cookies.Append("UserToken", tokenString,
                   new CookieOptions()
                   {
                       Domain = Environment.GetEnvironmentVariable("COOKIE_DOMAIN"),
                       Expires = DateTimeOffset.Now.AddHours(4)
                   });


                return RedirectToAction("Index", "Commpany");
            }
            return View();
        }





        /// <summary>
        /// Çıkış yapmak için kullanılır
        /// </summary>
        /// <returns></returns>

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("UserToken");
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Mail ve şifre doğru girildiyse true döner, girilmediyse false döner
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        private PersonModels LoginUser(string mail, string pas)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var Logindb = uow.GetRepository<PersonModels>().GetAll().FirstOrDefault(x => x.Email == mail && x.Password == pas);
                if (Logindb != null)
                {
                    return Logindb;
                }
                else
                {
                    TempData["HataliMesaj"] = "Hata  oluştu yeniden dene";
                    return null;
                }

            }
        }

        /// <summary>
        /// Kullanıcı kayıt ekranıdır. Hangi Companye mensup olduğunu seçmesi için companyler listelendi
        /// </summary>
        /// <returns></returns>    
        [HttpGet]
        public IActionResult Register()
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                ViewBag.list = uow.GetRepository<CompanyModels>().GetAll().ToList();

                return View();
            }
        }
        /// <summary>
        /// Person kayıt ekranı
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(PersonModels person)
        {

            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                try
                {
                    uow.GetRepository<PersonModels>().Add(person);
                    uow.SaveChanges();
                    TempData["BasariliMesaj"] = "Ekleme İşlemi Başarıyla Gerçekleşti";
                }
                catch (Exception)
                {
                    TempData["HataliMesaj"] = "Hata  oluştu yeniden dene";
                }
                return RedirectToAction("Login");
            }

        }

    }
}
