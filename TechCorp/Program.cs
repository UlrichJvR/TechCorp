using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechCorp.Data;
using TechCorp.Models;
using TechCorp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TechCorpAuthDb"));

builder.Services.AddDefaultIdentity<AdminUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false; // set to true later if using email confirmation
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// builder.Services.AddIdentity<AdminUser, IdentityRole>(options =>
//     {
//         options.SignIn.RequireConfirmedAccount = false; // set to true later if using email confirmation
//     })
//     .AddRoles<IdentityRole>()
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders();

builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeAreaFolder("Employee", "/");
});

builder.Services.AddControllersWithViews();

// Services
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await AuthService.SeedDataAsync(services);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Redirect}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();