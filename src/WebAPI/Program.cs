using Domain.Identity;

using Infrastructure.DatabaseContext;
using Infrastructure.Persistence;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Serilog;

using WebAPI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((
                            context,
                            services,
                            loggerConfiguration) =>
                        {
                          loggerConfiguration
                              // 从built-in configuration中读取配置
                              .ReadFrom.Configuration(context.Configuration)
                              // 读取当前app的服务，使其对于serilog可用
                              .ReadFrom.Services(services);
                        });

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// When running the application
// Initialize the database.
using (var scope = app.Services.CreateScope())
{
  // Create the ServiceProvider
  var services = scope.ServiceProvider;

  try
  {
    // Get all the services
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Run the migrations
    await context.Database.MigrateAsync();
    // Use the seed data
    await Seed.SeedData(context, userManager);
  }
  catch (Exception e)
  {
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "An error occurred while migrating the database");
  }
}

app.Run();
