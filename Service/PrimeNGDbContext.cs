using Microsoft.EntityFrameworkCore;
using PrimeNG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNG.API.Service
{
    public class PrimeNGDbContext : DbContext
    {
        public PrimeNGDbContext(DbContextOptions<PrimeNGDbContext> options)
        : base(options)
        { }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=PrimeNGDbContext;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
