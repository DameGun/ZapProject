using Microsoft.EntityFrameworkCore;
using ZapProject.Data.Enum;
using ZapProject.Models;

namespace ZapProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<Category> Category { get; set; }
    }
}
