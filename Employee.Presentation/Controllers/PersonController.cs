using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employe.DataAccess;
using Employee.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Presentation.Controllers
{
    public class PersonController : Controller
    {
        CompanyModels companyModels = new CompanyModels();

        [HttpGet]
        public IActionResult Index()
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var list = uow.GetRepository<PersonModels>().GetAll().ToList();
                return View(list);
            }
        }

        [HttpGet]
        public IActionResult InsertPerson()
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                uow.GetRepository<CompanyModels>().Get(companyModels.CompanyId);

            }
            return View();

        }

        [HttpPost]
        public IActionResult Insert(PersonModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                try
                {

                    uow.GetRepository<PersonModels>().Add(kisi);

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
        
        [HttpPost]
        public IActionResult Update(PersonModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                uow.GetRepository<PersonModels>().Update(kisi);
                //if (eskikisi == null)
                //{

                //    TempData["HataliMesaj"] = "Güncellenmek istenen kayıt bulunamadı";
                //    return RedirectToAction("Index");
                //}

                //    eskikisi.CompanyName = kisi.CompanyName;
                //    eskikisi.CompanyCity = kisi.CompanyCity;
                uow.SaveChanges();

                TempData["BasariliMesaj"] = "Kişi bilgileri başarıyla güncellendi";
                return RedirectToAction("Index","Commpany");



            }

        }

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
