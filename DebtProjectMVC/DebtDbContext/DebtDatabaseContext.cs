using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DebtProjectMVC.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DebtProjectMVC.DebtDbContext
{
    public class DebtDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public DebtDatabaseContext(DbContextOptions<DebtDatabaseContext> options): base(options)
        {

        }

        public DbSet<Creditor> Creditors { get; set; }
        //public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<DebtEntry> Debts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Creditor>()
                .ToTable("Creditor")
                .HasIndex(c => c.PESEL)
                .IsUnique();

            //modelBuilder
            //    .Entity<Borrower>()
            //    .ToTable("Borrower")
            //    .HasOne(p => p.Creditor)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder
            //    .Entity<Borrower>()
            //    .HasIndex(b => b.PESEL);

            modelBuilder
                .Entity<DebtEntry>()
                .ToTable("Debt")
                .Property(e => e.Status)
                .HasConversion(
                v => v.ToString(),
                v => (DebtEntryStatus)Enum.Parse(typeof(DebtEntryStatus), v))
                .HasDefaultValue(DebtEntryStatus.Open);
        }
    }
}
