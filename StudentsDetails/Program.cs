using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StudentsDetails.Context;
using StudentsDetails.Models;
using StudentsDetails.Registration;
using StudentsDetails.Seeding;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddStudentsServices();

#region Database connectivity


var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(connection));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>( options =>
{

}).AddEntityFrameworkStores<ApplicationDbContext>();

#endregion

#region UnAuthorized Redirect to LoginPage

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login"; // Your actual login path
});

#endregion

#region UseSession

builder.Services.AddSession();

#endregion

builder.Services.AddControllersWithViews();

var app = builder.Build();

UpdateRolesAsync(app);


static async void UpdateRolesAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var provider = scope.ServiceProvider;

        try
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "ADMIN",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "STAFF",
                    NormalizedName = "STAFF"

                },
                new IdentityRole
                {
                    Name = "STUDENT",
                    NormalizedName = "STUDENT"

                },
                 new IdentityRole
                {
                    Name = "SUPERADMIN",
                    NormalizedName = "SUPERADMIN"

                },
                  new IdentityRole
                {
                    Name = "DEAN",
                    NormalizedName = "DEAN"

                }

            };


            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

        }
        catch (Exception)
        {

            throw;
        }

    }
}


    /*static async void UpdateRolesAsync(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;

            try
            {
                var context = provider.GetRequiredService<ApplicationDbContext>();

                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                await RoleDataSeeding.RoleSeedingAsync(context);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }*/

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
