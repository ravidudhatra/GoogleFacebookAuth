using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GoogleFacebookAuth.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using GoogleFacebookAuth.Services.Interface;
using GoogleFacebookAuth.Services;
using GoogleFacebookAuth.Models;
using Microsoft.AspNetCore.Authentication;
using System.Configuration;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    //Google Authentication
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        googleOptions.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;

    })
    //Facebook Authentication
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration.GetSection("FacebookKeys:AppId").Value;
        facebookOptions.ClientSecret = builder.Configuration.GetSection("FacebookKeys:ClientSecret").Value;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    })
    //Twitter Authentication
    .AddTwitter(twitterOptions =>
    {

        twitterOptions.ConsumerKey = builder.Configuration.GetSection("TwitterAuthSetting:ApiKey").Value;
        twitterOptions.ConsumerSecret = builder.Configuration.GetSection("TwitterAuthSetting:ApiSecret").Value;

        twitterOptions.CallbackPath = new PathString("/signin-twitter");
    });

var connectionString = builder.Configuration.GetConnectionString("GoogleFacebookAuthContextConnection") 
        ?? throw new InvalidOperationException("Connection string 'GoogleFacebookAuthContextConnection' not found.");

builder.Services.AddDbContext<GoogleFacebookAuthContext>(options => 
        options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(GoogleFacebookAuthContext).Assembly.FullName)));

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<GoogleFacebookAuthContext>();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // A property naming policy, or null to leave property names unchanged.
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

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
app.UseAuthentication();

app.UseRouting();
    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapControllers();

app.Run();


