using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PortfolioApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Database")),
    RequestPath = new PathString("/Database")
});
app.MapControllerRoute(
    name: "projectsPage",
    pattern: "{controller=Projects}/{action=ProjectsPage}");
app.MapControllerRoute(
    name: "addProject",
    pattern: "{controller=AddProject}/{action=Index}");
app.MapControllerRoute(
    name: "addCategory",
    pattern: "{controller=AddCategory}/{action=Index}");
app.MapControllerRoute(
    name: "getProjectDetails",
    pattern: "{controller=GetProjectDetails}/{action=Get}");
app.MapControllerRoute(
    name: "register",
    pattern: "{controller=Register}/{action=Index}");
app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Login}/{action=Index}");
app.MapControllerRoute(
    name: "accountPage",
    pattern: "{controller=Account}/{action=Index}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
