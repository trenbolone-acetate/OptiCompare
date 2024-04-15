using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OptiCompare;
using OptiCompare.Data;
using OptiCompare.Extensions;
using OptiCompare.Models;
using OptiCompare.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptiCompare API", Version = "v1" });
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<PhoneRepository>();

builder.Services.AddControllersWithViews();
var connectionString = builder.GetConnectionString();
PhoneDetailsFetcher.SetApiToken(builder.Configuration["phones:BearerToken"]);
builder.Services.AddDbContext<OptiCompareDbContext>(options => { options.UseMySQL(connectionString); });

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });
builder.Services.AddRazorPages();
builder.Services.AddDbContext<OptiCompareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OptiCompareDbContext") ??
                         throw new InvalidOperationException("Connection string 'OptiCompareDbContext' not found.")));

builder.Services.AddIdentity<User,IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
    })
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<OptiCompareDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSession();
builder.Services.AddSingleton<ITempDataProvider, SessionStateTempDataProvider>();
var app = builder.Build();
app.UseSession();

app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptiCompare API v1");
});
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Editor", "DefaultUser" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();