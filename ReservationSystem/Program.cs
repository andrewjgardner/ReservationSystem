using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using ReservationSystem.Data;
using ReservationSystem.Data.Context;
using ReservationSystem.Data.Utilities;
using ReservationSystem.Services;
using System.Globalization;
using System.Text;
using NLog;
using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); ;

// Add services to the container.

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddTransient<IEmailSender, EmailService>();
builder.Services.Configure<SendGridOptions>(builder.Configuration);
//var EmailSenderOptions = builder.Configuration.GetSection("SendGrid").Get<EmailSenderOptions>();
//EmailService.Main();


builder.Services.AddAuthentication(o =>
    {
        o.DefaultScheme = "JWT_OR_COOKIE";
        o.DefaultChallengeScheme = "JWT_OR_COOKIE";
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

            RequireExpirationTime = true,

            ClockSkew = TimeSpan.Zero
        };
    })
    .AddPolicyScheme("JWT_OR_COOKIE", null, o =>
    {
        o.ForwardDefaultSelector = c =>
        {
            string auth = c.Request.Headers[HeaderNames.Authorization];
            if (!string.IsNullOrWhiteSpace(auth) && auth.StartsWith("Bearer "))
            {
                return JwtBearerDefaults.AuthenticationScheme;
            }

            return IdentityConstants.ApplicationScheme;
        };
    }).AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Google:Id"];
    googleOptions.ClientSecret = builder.Configuration["Google:Secret"];
});


#region NLog: Setup NLog for Dependency injection

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

#endregion

var app = builder.Build();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

var cultureInfo = new CultureInfo("en-AU");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.Run();

// Make Program Public for WebFactory creation in testing
public partial class Program { }
