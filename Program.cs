using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume;
using Resume.Models;
using Resume.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext") ?? throw new InvalidOperationException("Connection string 'DatabaseContext' not found.")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddScoped<IUserInitializer, UserInitializer>();
builder.Services.AddScoped<IUserInitializerService, UserInitializerService>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddResponseCaching();

builder.Services.AddAuthentication("Identity.Application").AddCookie(options =>
{
    options.LoginPath = new PathString("/Authorization/Login");
    options.AccessDeniedPath = new PathString("/Home/Portfolio");
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddRequestLocalization(options =>
{
    var supportedCulters = new List<CultureInfo>()
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

    options.DefaultRequestCulture = new RequestCulture(supportedCulters.FirstOrDefault());
    options.SupportedCultures = supportedCulters;
    options.SupportedUICultures = supportedCulters;

    options.ApplyCurrentCultureToResponseHeaders = true;

    options.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider()
    {
        Options = options
    });
});

builder.Services.AddControllersWithViews(options =>
        options.CacheProfiles.Add("DefaultHour",
        new CacheProfile()
        {
            Duration = 3600
        }))
    //options => options.Filters.Add(new CultureFilter())))
    .AddDataAnnotationsLocalization()
    .AddViewLocalization();
builder.Services.AddRazorPages();

var app = builder.Build();

var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = serviceScopeFactory.CreateScope())
{
    var service = (IUserInitializerService)scope.ServiceProvider.GetService(typeof(IUserInitializerService));
    await service.InitializeAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

//var rules = new RewriteOptions().AddRedirect(@"^.{0}$", "/Home/Portfolio");
//var rules = new RewriteOptions()
//    .AddRedirect(@"account[/]?$", "/account/portfolio")
//    .AddRedirect(@"^.{0}$", "/portfolio");
//app.UseRewriter(rules);

app.UseResponseCaching();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "culture",
    pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}",
    constraints: new
    {
        culture = new CultureConstraint(defaultCulture: "en", pattern: "[a-z]{2}")
    });

//app.MapControllerRoute(
//    name: "login",
//    pattern: "{action=Login}",
//    defaults: new { controller = "Authorization" });

//app.MapControllerRoute(
//    name: "root",
//    pattern: "{action=Portfolio}/{id?}",
//    defaults: new { controller = "Home" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Portfolio}/{id?}");

app.Run();