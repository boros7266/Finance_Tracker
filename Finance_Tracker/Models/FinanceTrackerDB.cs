using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.IdentityAuth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Finance_Tracker.Models
{
    public class FinanceTrackerDB : IdentityDbContext<ApplicationUser>
    {
        public FinanceTrackerDB(DbContextOptions<FinanceTrackerDB> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<CostInvoice> CostInvoices { get; set; }
        public DbSet<Company> Companys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
