using Microsoft.EntityFrameworkCore;
using PEMSApi.Models;

namespace PEMSApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Lương", Type = false },
                new Category { Id = 2, Name = "Thưởng", Type = false },
                new Category { Id = 3, Name = "Ăn uống", Type = true },
                new Category { Id = 4, Name = "Di chuyển", Type = true },
                new Category { Id = 5, Name = "Tiền nhà", Type = true }
            );
        }
    }
}
