using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //setting up the forieng keys
 builder.Entity<Enrollment>(x => x.HasKey(p => new { p.AppUserId, p.CourseID }));

//connect the FKs into the tables
				 builder.Entity<Enrollment>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(p => p.AppUserId);
            
		        builder.Entity<Enrollment>()
                .HasOne(u => u.Course)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(p => p.CourseID);

            List<IdentityRole> roles = new List<IdentityRole>
      {
        new IdentityRole {Name = "Admin", NormalizedName ="ADMIN"},

        new IdentityRole {Name = "User", NormalizedName = "USER"},
      };

            builder.Entity<IdentityRole>().HasData(roles);
        }


    }
}