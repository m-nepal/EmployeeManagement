using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=lblprod02;database=EmployeeDB;Trusted_Connection=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
