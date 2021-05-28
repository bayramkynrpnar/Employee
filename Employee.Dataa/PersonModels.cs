using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
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
        [JsonIgnore]

        public virtual CompanyModels CompanyModels { get; set; }

    }

}
