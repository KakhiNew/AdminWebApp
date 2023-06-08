using AdminWebApp.Data;
using AdminWebApp.Repositories;
using AdminWebApp.Services;
using AdminWebApp.Services.AdminWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.UseDeveloperExceptionPage();

if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (IServiceScope scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();
}

app.Run();
