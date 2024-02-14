using Microsoft.EntityFrameworkCore;
using Payment_Backend_Service.Data.Domain;
using Payment_Backend_Service.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Payment_Backend_Service.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalaryDetails> EmployeeSalaryDetails { get; set; }
        public DbSet<SalaryRequest> SalaryRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<EmployeeSalaryDetails>()
                .HasKey(esd => esd.Id);

            modelBuilder.Entity<SalaryRequest>()
                .HasKey(sr => sr.Id);




            modelBuilder.Entity<SalaryRequest>()
                 .HasOne(esd => esd.EmployeeSalaryDetails)
                 .WithMany()
                 .HasForeignKey(esd => esd.EmployeeSalaryDetailsId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
