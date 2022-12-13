using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var ServerVersion = new MySqlServerVersion(new Version(10, 9, 4));
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(options => true)
                    .AllowCredentials();
                });
            });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddDbContext<Api.Data.ApiDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("ApiAuthConnection") + Environment.GetEnvironmentVariable("DB_PASSWORD") +';',ServerVersion);
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();