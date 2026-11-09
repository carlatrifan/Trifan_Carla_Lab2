using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Trifan_Carla_Lab2.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});
var connectionString = builder.Configuration.GetConnectionString("Trifan_Carla_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Trifan_Carla_Lab2Context' not found.");

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/BookPages");
    options.Conventions.AllowAnonymousToPage("/BookPages/Index");
    options.Conventions.AllowAnonymousToPage("/BookPages/Details");
    options.Conventions.AuthorizeFolder("/MemberPages", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/PublisherPages", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/CategoryPages", "AdminPolicy");
});
builder.Services.AddDbContext<Trifan_Carla_Lab2Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Trifan_Carla_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Trifan_Carla_Lab2Context' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Trifan_Carla_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Trifan_Carla_Lab2Context' not found.")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
