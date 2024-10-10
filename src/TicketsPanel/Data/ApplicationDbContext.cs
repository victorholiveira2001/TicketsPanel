using Microsoft.Build.Execution;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.RateLimiting;
using TicketsPanel.Enums;
using TicketsPanel.Models;
using Microsoft.AspNetCore.Identity;

namespace TicketsPanel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Category> Categories { get; set; }

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
            builder.Entity<Ticket>().Property(c => c.OpenTime).HasDefaultValueSql("FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss')");
            builder.Entity<Ticket>().Property(c => c.Situation).HasDefaultValue(Situation.NotDefinedAttendent);
            builder.Entity<Ticket>().Property(c => c.Priority).HasDefaultValue(Priority.Normal);    
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
            //builder.Entity<Ticket>().HasOne(c => c.Priority).WithMany(p => p.Tickets).HasForeignKey(c => c.PriotiryId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Ticket>().HasOne(t => t.Client).WithMany(u => u.TicketsClient).HasForeignKey(t => t.ClientId);


            #endregion

            #region User | Definitions and Relatioship

            builder.Entity<ApplicationUser>(entity =>
                {
                    entity.Property(e => e.Id).HasColumnOrder(1);
                    entity.Property(e => e.UserName).HasColumnOrder(2);
                    entity.Property(e => e.SSN).HasColumnOrder(3);
                    entity.Property(e => e.Email).HasColumnOrder(4);
                    entity.Property(e => e.PhoneNumber).HasColumnOrder(5);
                    entity.Property(e => e.PasswordHash).HasColumnName("Password").HasColumnOrder(6);
                    entity.Property(e => e.Position).HasColumnOrder(7);


                });

            // One to Many
            builder.Entity<ApplicationUser>().HasMany(u => u.Messages).WithOne(m => m.Sender).HasForeignKey(u => u.SenderId).OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Organization | Definitions and Relationship

            builder.Entity<Organization>().HasKey(o => o.OrganizationId);
            builder.Entity<Organization>()
                .Property(o => o.OrganizationId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Entity<Organization>(o => o
                .HasMany(d => d.Departments)
                .WithOne(o => o.Organization)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction)
                );

            #endregion

            #region Department | Definitions and Relationship

            builder.Entity<Category>().HasKey(c  => c.CategoryId);
            builder.Entity<Department>().HasKey(c => c.DepartmentId);

            // One to Many
            builder.Entity<Department>().HasMany(d => d.Categories).WithOne(c => c.Department).HasForeignKey(c => c.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Department>().HasMany(d => d.Users).WithOne(u => u.Department).HasForeignKey(u => u.DepartmentId).OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}