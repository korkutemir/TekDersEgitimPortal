using Autofac;
using Autofac.Extensions.DependencyInjection;
using BuildBackEnd.Core.Middleware;
using BuildBackEnd.Core.Models;
using BuildBackEnd.Data;
using BuildBackEnd.Service.Mapping;
using BuildBackEnd.WebApi.Hubs;
using BuildBackEnd.WebApi.Modules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.WriteIndented = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MapProfile));

// Database connection and DbContext settings
//string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    var migrationsAssembly = typeof(AppDbContext).Assembly.GetName().Name;
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
//        b => b.MigrationsAssembly(migrationsAssembly).EnableStringComparisonTranslations());
//});

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);


// Identity Configuration
builder.Services.AddIdentity<Users, UserRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Autofac dependency injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new RepoServiceModule()));

// CORS policies - Updated for SignalR with credentials
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:5160") // Your MVC app's origin
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()); // Important for SignalR with credentials
});

// HttpContext access
builder.Services.AddHttpContextAccessor();

// SignalR service
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// Routing middleware
app.UseRouting();

// CORS usage - Must be after UseRouting and before UseAuthentication
app.UseCors("CorsPolicy");

// Exception handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// SignalR Hub and Controller mapping
app.MapHub<CourseHub>("/courseHub");
app.MapControllers();

app.Run();