using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Models
{[Authorize]
    public class EmployeeDbContext : DbContext
    {/// <summary>
    /// Tablolatımızı Dbset ile tanımlıyoruz. Dbset propertyleri veritabanındaki tablolara karşılık gelen tabloyu temsil eder
    /// 
    /// </summary>
        public DbSet<PersonModels> personModels { get; set; }
        public DbSet<CompanyModels> CompanyModels { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public EmployeeDbContext()
        {
        }
        /// <summary>
        /// Veritabanı bağlantısı sağlanıyor
        /// </summary>
        /// <param name="optionsBuilder"></param>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_URI"));
        }
        /// <summary>
        /// Tabloların özellikleri belirtiliyor
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModels>()
                .HasOne(p => p.CompanyModels)
                .WithMany(b => b.PersonModels);



        }

      
    }

}
