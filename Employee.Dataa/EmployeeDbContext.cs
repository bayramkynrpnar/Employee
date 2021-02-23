using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Models
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<PersonModels> personModels { get; set; }
        public DbSet<CompanyModels> CompanyModels { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public EmployeeDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_URI"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModels>()
                .HasOne(p => p.CompanyModels)
                .WithMany(b => b.PersonModels);



        }

        public object GetIncludePaths(Type type)
        {
            throw new NotImplementedException();
        }
    }

}
