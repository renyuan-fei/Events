using Infrastructure.DatabaseContext;
using Infrastructure.Identity;
using Infrastructure.Persistence;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Serilog;

using WebAPI.SignalR;
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

builder.Services.AddInfrastructure(builder.Configuration);
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

app.UseAuthentication();

app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

// Map the SignalR hubs
app.MapHub<ChatHub>("/chat");
app.MapHub<NotificationHub>("/notifications");
app.MapFallbackToController("Index","Fallback");
// When running the application
// Initialize the database.
using (var scope = app.Services.CreateScope())
{
  // Create the ServiceProvider
  var services = scope.ServiceProvider;

  try
  {
    // Get all the services
    var eventsContext = services.GetRequiredService<EventsDbContext>();
    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Run the migrations
    await eventsContext.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();

    // Use the seed data
    await Seed.SeedData(identityContext, eventsContext, userManager);
  }
  catch (Exception e)
  {
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "An error occurred while migrating the database");
  }
}

app.Run();
