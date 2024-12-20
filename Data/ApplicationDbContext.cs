using Microsoft.EntityFrameworkCore;
using SW_Parcial_v3.Models;

namespace SW_Parcial_v3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
    }
}
