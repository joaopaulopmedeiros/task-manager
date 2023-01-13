using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infrastructure
{
    public class TaskDbContext : DbContext
    {
        protected TaskDbContext()
        {
        }

        public TaskDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>()
                .ToTable("tasks")
                .Property(t => t.CreatedAt).HasColumnName("created_at");

            base.OnModelCreating(modelBuilder);
        }
    }
}