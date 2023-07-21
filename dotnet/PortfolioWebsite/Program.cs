using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models; // Replace "YourNamespace" with the actual namespace of your Models folder
using PortfolioWebsite.Security; // Replace "YourNamespace" with the actual namespace of your Security folder

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PortfolioContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioConnection")));

// Register ITokenGenerator service
string jwtSecret = builder.Configuration["JwtSecret"]; // Ensure you have added the JwtSecret to your appsettings.json
builder.Services.AddSingleton<ITokenGenerator>(new JwtGenerator(jwtSecret));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
