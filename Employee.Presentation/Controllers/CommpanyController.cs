using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employe.DataAccess;
using Employee.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;


namespace Employee.Presentation.Controllers
{
    [Authorize]

    public class CommpanyController : Controller
    {

        /// <summary>
        /// companyModele ait bütün veriyi listelemek çiin kullanılır
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public IActionResult Index()
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var list = uow.GetRepository<CompanyModels>().GetAll().ToList();
                return View(list);
            }
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        
        /// <summary>
        /// CompanyModelse Company eklemek için kullanılır
        /// </summary>
        /// <param name="kisi"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert(CompanyModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                try
                {
                    uow.GetRepository<CompanyModels>().Add(kisi);
                    uow.SaveChanges();
                    TempData["BasariliMesaj"] = "Ekleme İşlemi Başarıyla Gerçekleşti";
                }
                catch (Exception)
                {

                    TempData["HataliMesaj"] = "Hata  oluştu yeniden dene";


                }
                return RedirectToAction("Index");

            }
        }
        /// <summary>
        /// CompanyModelse ait verileri update etmek için kullanılır. Update edilmek istenen kişiyi getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var kisi = uow.GetRepository<CompanyModels>().Get(id);
                if (kisi == null)
                {
                    TempData["HataliMesaj"] = "Güncellenmek istenen kayıt bulunamadı";
                    return RedirectToAction("Index");
                }
                return View(kisi);
            }
        }
        /// <summary>
        ///  CompanyModelse ait verileri update etmek için kullanılır.
        /// </summary>
        /// <param name="kisi"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult Update(CompanyModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                uow.GetRepository<CompanyModels>().Update(kisi);          
                uow.SaveChanges();
                TempData["BasariliMesaj"] = "Kişi bilgileri başarıyla güncellendi";
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// İdsi getirilen Companymodelsi silmek için kullanılır
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var kisi = uow.GetRepository<CompanyModels>().Get(id);
                uow.GetRepository<CompanyModels>().Delete(kisi);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }
       /// <summary>
       /// Seçilen companye mensup personları listelemek için kullanılır
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpGet]
        
        public IActionResult ListPerson(int id)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var list = uow.GetRepository<PersonModels>().GetAll(x => x.CompanyId == id).ToList();

                ViewData["CompanyId"] = id;
                return View(list);
            }

        }      

    }
}
