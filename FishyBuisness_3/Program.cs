using FishyBuisness_3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


//Localization

builder.Services.AddLocalization(options => options.ResourcesPath = "Rescoures");
builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new List<CultureInfo> {
        new CultureInfo("en"),
        new CultureInfo("pl"),
        new CultureInfo("de")

        };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

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

//var cultures = new[] { "en", "pl", "de" };
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(cultures[0])
//	.AddSupportedCultures(cultures)
//	.AddSupportedUICultures(cultures);
app.UseRequestLocalization(app.Services.CreateScope().ServiceProvider
    .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "FishRoute",
    pattern: "Fish/{action=Index}/{id?}",
    defaults: new { controller = "Fish" },
    constraints: new { action = "Index|Details|Create|Edit|Delete" }
);

app.MapControllerRoute(
    name: "FishTanksRoute",
    pattern: "FishTanks/{action=Index}/{id?}",
    defaults: new { controller = "FishTanks" },
    constraints: new { action = "Index|Details|Create|Edit|Delete" }
);

app.MapControllerRoute(
    name: "StocksRoute",
    pattern: "Stocks/{action=Index}/{id?}",
    defaults: new { controller = "Stocks" },
    constraints: new { action = "Index|Details|Create|Edit|Delete" }
);

app.MapControllerRoute(
    name: "EnvironmentsRoute",
    pattern: "Environments/{action=Index}/{id?}",
    defaults: new { controller = "Environments" },
    constraints: new { action = "Index|Details|Create|Edit|Delete" }
);
app.MapControllerRoute(
    name: "AppRolesRoute",
    pattern: "AppRoles/{action=Index}/{id?}",
    defaults: new { controller = "AppRoles" },
    constraints: new { action = "Index|Details|Create|Edit|Delete" }
);

app.MapRazorPages();
//AppDbInitializer.Seed(app); //Seed database

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin"};

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));


        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "admin@admin.com";
    string password = "Admin123!";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }


}
app.Run();


