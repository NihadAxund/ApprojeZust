using App.Business.Concrete;
using App.Entities.Models;
using approje.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});



// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppPoroje;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<CustomIdentityUserService, CustomIdentityUserService>();
// Hizmetlerin yapılandırılması

builder.Services.AddDbContext<CustomIdentityDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
    .AddEntityFrameworkStores<CustomIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{

    options.LoginPath = "/home/login"; // Oturum açma sayfasının URL'si
    options.AccessDeniedPath = "/home/login"; // Erişim reddedildi sayfasının URL'si
});

builder.Services.AddSignalR();




var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub").RequireAuthorization(); // ChatHub'ı endpointlere ekleyin ve kimlik doğrulama gerektirin

    // Diğer endpointler
    endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapFallbackToController("Index", "Home");
});





//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
