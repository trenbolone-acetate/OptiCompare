using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OptiCompare;
using OptiCompare.Data;
using OptiCompare.Models;
using OptiCompare.Utils;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var connectionString = Utils.GetConnectionString(builder);
PhoneDetailsFetcher.SetApiToken(builder.Configuration["phones:BearerToken"]);
builder.Services.AddDbContext<OptiCompareDbContext>(options =>
{
    options.UseMySQL(connectionString);
});
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();
builder.Services.AddDbContext<OptiCompareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OptiCompareDbContext") ?? throw new InvalidOperationException("Connection string 'OptiCompareDbContext' not found.")));
builder.Services.AddSession();
builder.Services.AddSingleton<ITempDataProvider,SessionStateTempDataProvider>();
var app = builder.Build();
app.UseSession();

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
