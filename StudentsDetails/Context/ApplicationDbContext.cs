using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsDetails.Models;

namespace StudentsDetails.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Course> courses { get; set; }

        public DbSet<Student> students { get; set; }


        public DbSet<User> users { get; set; }

        public DbSet<Role> roles { get; set; }



    }
}
