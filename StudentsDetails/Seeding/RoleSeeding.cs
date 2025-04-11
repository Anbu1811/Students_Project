using StudentsDetails.Context;
using StudentsDetails.Models;

namespace StudentsDetails.Seeding
{
    public class RoleSeeding
    {
        public int Id { get; set; }

        public string RoleName { get; set; }


    }

    public class RoleDataSeeding
    {
        //private readonly IConfiguration _configuration;
        //private readonly ApplicationDbContext _context;

        //public RoleDataSeeding(IConfiguration configuration, ApplicationDbContext context)
        //{
        //    _configuration = configuration;
        //    _context = context;
        //}

        public static async Task  RoleSeedingAsync(ApplicationDbContext _context)
        {
            if (!_context.roles.Any())
            {
                await _context.roles.AddRangeAsync(
                    new Role
                    {
                        UserRole = "Admin"
                    },
                    new Role
                    {
                        UserRole = "Dean"
                    },
                    new Role
                    {
                        UserRole = "Staff"
                    },
                    new Role
                    {
                        UserRole = "Students"
                    },
                    new Role
                    {
                        UserRole = "SuperAdmin"
                    }

                );
                await _context.SaveChangesAsync();
            }
        }


    }

}
