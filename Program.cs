using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
var ServerVersion = new MySqlServerVersion(new Version(10, 9, 4));
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
).AddCookie();
builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsSpecs",
                builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(options => true).
                    AllowCredentials();
                });
            });
builder.Services.AddDbContext<Api.Data.ApiAuthContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("ApiAuthConnection"), ServerVersion);
});
var app = builder.Build();
app.UseCors("CorsSpecs");
app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();