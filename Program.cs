using System.Text.Json.Serialization;
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
using OptiCompare.Services;

var builder = WebApplication.CreateBuilder(args);

// Set up Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptiCompare API", Version = "v1" });
});
//Prevent infinity loop of references during de-/serialization
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddScoped<PhoneRepository, PhoneRepository>();
builder.Services.AddScoped(typeof(IRepository<Phone>), typeof(PhoneRepository));
builder.Services.AddScoped<IRepository<Phone>>(provider => provider.GetRequiredService<PhoneRepository>());
builder.Services.AddScoped<IPhoneService, PhoneService>();

builder.Services.AddControllersWithViews();

PhoneDetailsFetcher.SetApiToken(builder.Configuration["phones_BearerToken"]);

var connectionString = builder.GetConnectionString();

// Set up database and identity
builder.Services.AddDbContext<OptiCompareDbContext>(options => options.UseMySQL(connectionString));
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

// Set up authentication and authorization
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
.AddNegotiate();

builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

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

// Ensure roles exist
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();