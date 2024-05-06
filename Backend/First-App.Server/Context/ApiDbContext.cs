using First_App.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace First_App.Server.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<ActivityLogType> ActivityLogTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskList>().HasIndex(t => t.Id).IsUnique();
            modelBuilder.Entity<TaskList>().HasIndex(t => t.Name).IsUnique();

            modelBuilder.Entity<Card>().HasIndex(t => t.Id).IsUnique();
            modelBuilder.Entity<Card>().HasOne(x => x.TaskList)
                .WithMany(x => x.Cards).HasForeignKey(x => x.TaskListId);

            modelBuilder.Entity<Priority>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Card>().HasOne(x => x.Priority).WithMany(x => x.Cards).HasForeignKey(x => x.PriorityId);

            modelBuilder.Entity<ActivityLog>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<ActivityLogType>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<ActivityLog>().HasOne(x => x.ActivityLogType).WithMany(x => x.ActivityLogs).HasForeignKey(x => x.ActivityLogTypeId);
            modelBuilder.Entity<ActivityLog>().HasOne(x => x.ChangedCard).WithMany(x => x.ActivityLogs).HasForeignKey(x => x.ChangedCardId);

            modelBuilder.Entity<ActivityLogType>().HasData(
                new ActivityLogType { Id = 1, Name = "Create" },
                new ActivityLogType { Id = 2, Name = "Edit" },
                new ActivityLogType { Id = 3, Name = "Delete" },
                new ActivityLogType { Id = 4, Name = "Move" },
                new ActivityLogType { Id = 5, Name = "Rename" }
            );

            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "High" },
                new Priority { Id = 2, Name = "Medium" },
                new Priority { Id = 3, Name = "Low" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
