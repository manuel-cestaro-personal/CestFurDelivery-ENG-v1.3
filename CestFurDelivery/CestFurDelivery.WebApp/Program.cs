using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CestFurDelivery.WebApp.Data;
using CestFurDelivery.Services.Services;
using CestFurDelivery.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CestFurDeliveryWebAppContextConnection") ?? throw new InvalidOperationException("Connection string 'CestFurDeliveryWebAppContextConnection' not found.");
var services = builder.Services;
var configuration = builder.Configuration;


builder.Services.AddDbContext<CestFurDeliveryWebAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 10;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = false;
})
    //.AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<CestFurDeliveryWebAppContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders(); ;


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IDeliveryService, DeliveryService>();
builder.Services.AddSingleton<IDeliveryStateService, DeliveryStateService>();
builder.Services.AddSingleton<ITeamService, TeamService>();
builder.Services.AddSingleton<IVehicleService, VehicleService>();
builder.Services.AddSingleton<IWeekService, WeekService>();
builder.Services.AddSingleton<IViewService, ViewService>();
builder.Services.AddSingleton<IChangeStatusService, ChangeStatusService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
