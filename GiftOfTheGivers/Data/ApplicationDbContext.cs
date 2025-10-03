using GiftOfTheGivers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✅ DbSets for your entities
        public DbSet<Donation> Donations { get; set; }
        public DbSet<IncidentReport> IncidentReports { get; set; }
        public DbSet<VolunteerTask> VolunteerTasks { get; set; }
        public DbSet<VolunteerAssignment> VolunteerAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity constraints (for tokens/logins compatibility)
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.IsVolunteer)
                      .HasDefaultValue(false);
            });

            builder.Entity<VolunteerAssignment>()
                .HasOne(a => a.VolunteerTask)
                .WithMany(t => t.Assignments)
                .HasForeignKey(a => a.TaskId);

            builder.Entity<VolunteerAssignment>()
                .HasOne(a => a.VolunteerUser)
                .WithMany()
                .HasForeignKey(a => a.VolunteerUserId);
        }
    }
}
