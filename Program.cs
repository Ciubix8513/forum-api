using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
public class Program
{
    public void Main(string[] args)
    {
        var ServerVersion = new MySqlServerVersion(new Version(10,9,4));
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<Api.Data.ApiAuthContext>(options =>
        {
            //Connect to MySQL
            options.UseMySql(builder.Configuration.GetConnectionString("ApiAuthConnectction"),ServerVersion);
        });
        var app = builder.Build();

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
    }
}