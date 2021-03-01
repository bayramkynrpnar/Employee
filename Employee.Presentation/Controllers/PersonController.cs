using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Employe.DataAccess;
using Employee.Data.Models;
using Employee.Presentation.Authorization;
using Employee.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Employee.Presentation.Controllers
{
    [Authorization]
    public class PersonController : Controller
    {
        /// <summary>
        /// Personları listeler
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Index()
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var list = uow.GetRepository<PersonModels>().GetAll().ToList();
                return View(list);
            }
        }
        /// <summary>
        /// Person ekleme ekranı
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InsertPerson(int id)
        {
            
            ViewData["CompanyId"] = id;
            return View();
        }

      
        /// <summary>
        /// Person eklemek için kullanılır
        /// </summary>
        /// <param name="kisi"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert(PersonModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                try
                {
                    kisi.Password = CryptoHelpers.Instance.MD5(kisi.Password);
                    
                    uow.GetRepository<PersonModels>().Add(kisi);

                    uow.SaveChanges();
                    TempData["BasariliMesaj"] = "Ekleme İşlemi Başarıyla Gerçekleşti";           
                }
                catch (Exception)
                {

                    TempData["HataliMesaj"] = "Hata  oluştu yeniden dene";


                }
                return RedirectToAction("Index","Commpany");

            }
        }
        /// <summary>
        /// Update edilecek personu getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var kisi = uow.GetRepository<PersonModels>().Get(id);
                if (kisi == null)
                {
                    TempData["HataliMesaj"] = "Güncellenmek istenen kayıt bulunamadı";
                    return RedirectToAction("Index");
                }
                return View(kisi);
            }
        }
        /// <summary>
        /// Personu update etmek için kullanılır
        /// </summary>
        /// <param name="kisi"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(PersonModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                uow.GetRepository<PersonModels>().Update(kisi);              
                uow.SaveChanges();
                TempData["BasariliMesaj"] = "Kişi bilgileri başarıyla güncellendi";
                return RedirectToAction("Index","Commpany");
            }
        }
        /// <summary>
        /// Seçilen personu silmek için kullanılır
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var kisi = uow.GetRepository<PersonModels>().Get(id);
                uow.GetRepository<PersonModels>().Delete(kisi);
                uow.SaveChanges();
                return RedirectToAction("Index","Commpany");
            }
        }
    }
}
