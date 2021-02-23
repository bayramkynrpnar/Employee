using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employe.DataAccess;
using Employee.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Nest;


namespace Employee.Presentation.Controllers
{


    public class CommpanyController : Controller
    {


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
        [HttpPost]

        public IActionResult Update(CompanyModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                uow.GetRepository<CompanyModels>().Update(kisi);
                //if (eskikisi == null)
                //{

                //    TempData["HataliMesaj"] = "Güncellenmek istenen kayıt bulunamadı";
                //    return RedirectToAction("Index");
                //}

                //    eskikisi.CompanyName = kisi.CompanyName;
                //    eskikisi.CompanyCity = kisi.CompanyCity;
                uow.SaveChanges();

                TempData["BasariliMesaj"] = "Kişi bilgileri başarıyla güncellendi";
                return RedirectToAction("Index");



            }

        }

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

        [HttpGet]
        public IActionResult ListPerson(int id)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var list = uow.GetRepository<PersonModels>().GetAll(x => x.CompanyId == id).ToList();


                return View(list);
            }

        }

        //public IActionResult GetCompanyId(int id)
        //{
        //    using (var uow = new UnitOfWork<EmployeeDbContext>())
        //    {
        //        var list = uow.GetRepository<CompanyModels>().Get(x => x.CompanyId == id);
        //    }
        //}



    }
}
