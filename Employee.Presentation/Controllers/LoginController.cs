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
using Umbraco.Core.Models.Identity;
using Umbraco.Core.Migrations.Upgrade.V_8_9_0;

namespace Employee.Presentation.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

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

                //Just redirect to our index after logging in. 
                return RedirectToAction("Index", "Commpany");
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
        private bool LoginUser(string pas, string mail)
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
            //if (username == "cagatay" && password == "123")
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

        }
        //[HttpPost]
        //public ActionResult Login(string pas, string mail)
        //{
        //    using (var uow = new UnitOfWork<EmployeeDbContext>())
        //    {
        //        var Logindb = uow.GetRepository<PersonModels>().GetAll().FirstOrDefault(x => x.Email == mail && x.Password == pas);
        //        if (Logindb != null)
        //        {
        //            return RedirectToAction("Index","Commpany");
        //        }
        //        else
        //        {
        //            TempData["HataliMesaj"] = "Hata  oluştu yeniden dene";
        //            return View();
        //        }

        //    }

          
            

        //}
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
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
