using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;
using QuanLyPhongKham.Models;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        if (!roleManager.Roles.Any())
        {
            var roles = new[] { "Admin", "Doctor", "Receptionist", "Pharmacist", "Cashier", "Patient" };
            foreach (var role in roles)
            {
                //Check if admin account existed to avoid creating duplicate account
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }    
        
        string email = "admin@gmail.com";
        string password = "Abc123!";

        //Only create 1 admin account when start
        //if there is no account created
        if (!userManager.Users.Any(u => u.UserName == email))
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Admin");
        }

        //Create a doctor account
        
        string doctorEmail = "truong.nguyen@gmail.com";
        if (!userManager.Users.Any(u => u.UserName == doctorEmail))
        {
            var user = new IdentityUser
            {
                UserName = doctorEmail,
                Email = doctorEmail,
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Doctor");

        }

        //Create a receptionist account
        string receptionistEmail = "hungnguyen@gmail.com";
        if (!userManager.Users.Any(u => u.UserName == receptionistEmail))
        {
            var user = new IdentityUser
            {
                UserName = receptionistEmail,
                Email = receptionistEmail,
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Receptionist");
        }

        //Create a pharmacist account
        string pharmacistEmail = "kim.nguyen@gmail.com";
        if (!userManager.Users.Any(u => u.UserName == pharmacistEmail))
        {
            var user = new IdentityUser
            {
                UserName = pharmacistEmail,
                Email = pharmacistEmail,
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Pharmacist");
        }

        //Create a cashier account
        string cashierEmail = "ngan.nguyen@gmail.com";
        if (!userManager.Users.Any(u => u.UserName == cashierEmail))
        {
            var user = new IdentityUser
            {
                UserName = cashierEmail,
                Email = cashierEmail,
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Cashier");
        }

    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }

}

app.Run();
