using ourTime_server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ourTime_server.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddScoped<ApplicationDbContext>();

var app = builder.Build();
app.UseCors("AllowReactApp");

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();

    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

    using var dbContext = new ApplicationDbContext(optionsBuilder.Options, configuration);

    // Perform any database initialization or seeding here

    // Ensure the database is created/updated
    dbContext.Database.EnsureCreated();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<WebRTC>("/webRTC");
});

app.Run();
