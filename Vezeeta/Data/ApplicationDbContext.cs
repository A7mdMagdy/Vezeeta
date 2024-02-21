using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Vezeeta.Models;

namespace Vezeeta.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            //builder.Entity<Appointments>()
            //.HasOne(a => a.Doctor)
            //.WithMany(u => u.Appointments)
            //.HasForeignKey(a => a.DoctorId)
            //.OnDelete(DeleteBehavior.ClientSetNull);
            ////.OnDelete(DeleteBehavior.Restrict); // or Cascade if you want appointments to be deleted when a user is deleted

            //builder.Entity<Appointments>()
            //    .HasOne(a => a.Patient)
            //    .WithMany(u => u.Appointments)
            //    .HasForeignKey(a => a.PatientId)
            //.OnDelete(DeleteBehavior.ClientSetNull);
            ////.OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Reviews>()
            //.HasOne(a => a.Doctor)
            //.WithMany(u => u.Reviews)
            //.HasForeignKey(a => a.DoctorId)
            //.OnDelete(DeleteBehavior.ClientSetNull);
            ////.OnDelete(DeleteBehavior.Restrict); // or Cascade if you want appointments to be deleted when a user is deleted

            //builder.Entity<Reviews>()
            //    .HasOne(a => a.Patient)
            //    .WithMany(u => u.Reviews)
            //    .HasForeignKey(a => a.PatientId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
            ////.OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Appointments>()
            .HasOne(a => a.Doctor)
            .WithMany(u => u.DoctorAppointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointments>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.PatientAppointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reviews>()
            .HasOne(a => a.Doctor)
            .WithMany(u => u.DoctorReviews)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reviews>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.PatientReviews)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
    }
}
