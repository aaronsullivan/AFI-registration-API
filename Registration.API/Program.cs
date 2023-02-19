using Lamar.Microsoft.DependencyInjection;
using Lamar.Scanning.Conventions;
using Microsoft.EntityFrameworkCore;
using Registration.API.Middleware;
using Registration.Application.Customers;
using Registration.Infrastructure;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar(x =>
{
    x.Scan(s =>
    {
        s.TheCallingAssembly();
        s.Assembly(typeof(ICustomerService).Assembly);
        s.Assembly(typeof(DatabaseRepository<>).Assembly);
        s.LookForRegistries();
        s.WithDefaultConventions(OverwriteBehavior.Never);
    });
});

builder.Host.UseSerilog((context, services, loggerConfig) =>
 {
     loggerConfig
         .MinimumLevel.Warning()
         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
         .MinimumLevel.Override("System", LogEventLevel.Warning)
         .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Warning)
         .Enrich.FromLogContext()
         .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), "Logs/log-.json",
             rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100000000, retainedFileCountLimit: 60)
         .WriteTo.Console(
             outputTemplate:
             "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}",
             theme: AnsiConsoleTheme.Literate);
 });

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("RegistrationDatabase") ?? "Data Source=Registration.db";
    options.UseSqlite(connectionString);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.SetupExceptionHandling(builder.Environment);

app.UseAuthorization();

app.MapControllers();

app.Run();