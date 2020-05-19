using BillingManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingManagement.UI
{
    public class BillingManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseSqlite("Data Source =BillManagement.db");
       

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
}