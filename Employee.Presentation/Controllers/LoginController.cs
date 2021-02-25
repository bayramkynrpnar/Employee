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

namespace Employee.Presentation.Controllers
{
    public class LoginController : Controller
    {
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

        public async Task<IActionResult> Login(PersonModels personModels)
        {
            if (LoginUser(personModels.Email, personModels.Password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, personModels.Email)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);  
                await HttpContext.SignInAsync(principal);

                
                return RedirectToAction("Index", "Commpany");
            }
            return View();
        }
        /// <summary>
        /// Çıkış yapmak için kullanılır
        /// </summary>
        /// <returns></returns>
        public IActionResult  LogOut()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        /// <summary>
        /// Mail ve şifre doğru girildiyse true döner, girilmediyse false döner
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        private bool LoginUser(string mail, string pas)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var Logindb = uow.GetRepository<PersonModels>().GetAll().FirstOrDefault(x => x.Email == mail && x.Password == pas);
                if (Logindb != null)
                {
                    return true;
                }
                else
                {
                    TempData["HataliMesaj"] = "Hata  oluştu yeniden dene";
                    return false;
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
                ViewBag.list =   uow.GetRepository<CompanyModels>().GetAll().ToList();

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
