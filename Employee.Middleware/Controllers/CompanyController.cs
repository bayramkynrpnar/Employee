using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employe.DataAccess;
using Employee.Data.Models;
using Employee.Presentation.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Employee.Middleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CompanyController : ControllerBase
    {
        [HttpPost("Listing")]
        public List<CompanyModels> Listing()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "EmailQuee",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
            
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "EmailQuee",
                    basicProperties: null
                    );

            }
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                var list = uow.GetRepository<CompanyModels>().GetAll().Include(x => x.PersonModels).ToList();
            return list;


            }

        }

        [HttpPost("Insert")]
        public  void Insert(CompanyModels kisi)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
                uow.GetRepository<CompanyModels>().Add(kisi);
                uow.SaveChanges();
            }
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "EmailQuee",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                var message = JsonConvert.SerializeObject(kisi);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange:"",
                    routingKey:"EmailQuee",
                    basicProperties:null,
                    body:body
                    );

            }

        }
        [HttpGet("GetCompany/{name}")]
        public IActionResult GetCompany(string name)
        {
            using (var uow = new UnitOfWork<EmployeeDbContext>())
            {
              var company=  uow.GetRepository<CompanyModels>().GetAll(x => x.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (company == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(company);
                }
            }
        }

    }
}
