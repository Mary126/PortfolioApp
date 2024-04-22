using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projects.Data;

namespace PortfolioApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProjectCategory>().HasMany(c => c.Projects).WithOne(p => p.ProjectCategory).HasForeignKey(p => p.ProjectCategoryId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.Entity<Project>().HasMany(p => p.Images).WithOne(i => i.Project).HasForeignKey(i => i.ProjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            base.OnModelCreating(builder);
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
