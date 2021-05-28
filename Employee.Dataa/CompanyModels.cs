using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Employee.Data.Models
{ 
 
    public class CompanyModels
    {
        
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCity { get; set; }
        [IgnoreDataMember]
        public virtual List<PersonModels> PersonModels { get; set; }
    }
}
