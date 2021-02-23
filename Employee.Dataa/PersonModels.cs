using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Data.Models
{
    public class PersonModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }

        public string Email { get; set; }

        public virtual CompanyModels CompanyModels { get; set; }

    }

}
