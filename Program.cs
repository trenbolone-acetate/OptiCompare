using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OptiCompare;
using OptiCompare.Data;
using OptiCompare.Extensions;
using OptiCompare.Models;
using OptiCompare.Repositories;

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Program>()
                .UseUrls("https://*:44335")
                .ConfigureKestrel(serverOptions =>
                {
                    serverOptions.ConfigureHttpsDefaults(listenOptions =>
                    {
                        listenOptions.ServerCertificate = new X509Certificate2(
                            @"localhost.pfx",
                            "a1r2t3e4m5"
                        );
                    });
                });
        });
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
app.Run();