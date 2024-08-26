using Microsoft.EntityFrameworkCore;
using TicketsPanel.Models;

namespace TicketsPanel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Ticket | Definitions and Relationship

            builder.Entity<Ticket>().HasKey(c => c.TicketId);
            builder.Entity<Ticket>().Property(c => c.AttendantId).HasDefaultValue(null);
            builder.Entity<Ticket>().Property(c => c.OpenTime).HasDefaultValue(DateTime.UtcNow);
            builder.Entity<Ticket>().Property(c => c.CloseTime).HasDefaultValue(null);
            builder.Entity<Ticket>().Property(c => c.Sla).HasDefaultValue(null);
            builder.Entity<Ticket>().Property(c => c.ReceiveResponse).HasDefaultValue(true);
            builder.Entity<Ticket>().Property(c => c.SendReply).HasDefaultValue(true);

            // One to Many
            builder.Entity<Ticket>().HasMany(c => c.Messages).WithOne(m => m.Ticket).HasForeignKey(c => c.TicketId).OnDelete(DeleteBehavior.NoAction);

            // Many to One
            builder.Entity<Ticket>().HasOne(c => c.Department).WithMany(d => d.Tickets).HasForeignKey(c => c.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Ticket>().HasOne(c => c.Category).WithMany(c => c.Tickets).HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Ticket>().HasOne(c => c.Attendant).WithMany(a => a.TicketsAttendant).HasForeignKey(c => c.AttendantId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Ticket>().HasOne(c => c.Priority).WithMany(p => p.Tickets).HasForeignKey(c => c.PriotiryId).OnDelete(DeleteBehavior.NoAction);

            // Many to Many
            builder.Entity<Ticket>()
                .HasMany(t => t.Clients)
                .WithMany(u => u.TicketsClient)
                .UsingEntity<Dictionary<string, object>>(
                    "TicketClient",
                    t => t.HasOne<User>().WithMany().HasForeignKey("ClientId"),
                    c => c.HasOne<Ticket>().WithMany().HasForeignKey("TicketId"));

            #endregion

            #region User | Definitions and Relatioship

            builder.Entity<User>().HasKey(u => u.UserId);
            builder.Entity<User>().Property(u => u.Name).IsRequired();

            // Many to Many
            builder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    u => u.HasOne<User>().WithMany().HasForeignKey("UserId"));

            // One to Many
            builder.Entity<User>().HasMany(u => u.Messages).WithOne(m => m.Sender).HasForeignKey(u => u.SenderId).OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Department | Definitions and Relationship

            // One to Many
            builder.Entity<Department>().HasMany(d => d.Categories).WithOne(c => c.Department).HasForeignKey(c => c.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Department>().HasMany(d => d.Users).WithOne(u => u.Department).HasForeignKey(u => u.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            
            // Many to One
            builder.Entity<Department>().HasOne(d => d.Organization).WithMany(o => o.Departments).HasForeignKey(d => d.OrganizationId).OnDelete(DeleteBehavior.NoAction);
            
            // One to One
            builder.Entity<Department>().HasOne(d => d.Manager).WithOne().HasForeignKey<Department>(d => d.ManagerId).OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}