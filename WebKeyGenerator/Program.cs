using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebKeyGenerator.Context;
using WebKeyGenerator.Services;
using WebKeyGenerator.Utils;
using Telegram.Bot;
using NLog.Web;
using NLog;
using System.Data;
using Hangfire.MySql;
using Hangfire;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                            builder =>
                            {
                                builder.WithOrigins(
                            "http://localhost:5000",
                            "https://localhost:44369",
                            "https://localhost:5001").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                //builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
                            });
});

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));


builder.Services.AddHangfire(x => x.UseSqlServerStorage(connection));
// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDataService, DataService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = TokenHelper.Issuer,
            ValidAudience = TokenHelper.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret))
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHangfireDashboard();

app.UseAuthentication();





app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseRouting();
app.UseCors(builder =>
{
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
});
app.UseAuthorization();

app.UseEndpoints(enpoints => 
{
    enpoints.MapHangfireDashboard();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;




app.Run();

