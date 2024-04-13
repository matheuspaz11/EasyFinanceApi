using EasyFinanceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinanceApi.Context
{
    public class EasyFinanceContext : DbContext
    {
        public EasyFinanceContext(DbContextOptions<EasyFinanceContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Objective> Objectives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}