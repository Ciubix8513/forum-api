using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var ServerVersion = new MySqlServerVersion(new Version(10, 9, 4));
var builder = WebApplication.CreateBuilder(args);
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
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
).AddCookie();
builder.Services.AddDbContext<Api.Data.ApiDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("ApiAuthConnection"), ServerVersion);
});
var app = builder.Build();
app.UseCors();

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