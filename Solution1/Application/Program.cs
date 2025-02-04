
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Interfaces;
using Domain.Services;
using Domain.Validators;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Interface.DTO.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Security.Interfaces;
using Security.Services;
using Support.Utils;
using AuthenticationMiddleware = Application.Middleware.AuthenticationMiddleware;



var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); 
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOriginsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.WriteIndented = true;
    
    
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
});



var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", true, true)
    .Build();

builder.Services.AddSingleton(configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(dbOptions =>
{
    if (builder.Environment.IsDevelopment())
    {
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }
    else if (builder.Environment.IsProduction())
    {
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors();
    }
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Corsersac API",
        Description = "Corsersac Rest API Documentation",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Corsersac",
            Url = new Uri("https://github.com/LordMathi2741")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
        
    });
});

builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ISeedRolesService, SeedRolesService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ISeedStatuesService, SeedStatuesService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IBusinessRulesValidator, BusinessRulesValidator>();
builder.Services.AddScoped<IUsageService, UsageService>();
builder.Services.AddAutoMapper(typeof(ModelToResponse), typeof(RequestToModel));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    var seedRolesService = services.GetRequiredService<ISeedRolesService>();
    var seedStatuesService = services.GetRequiredService<ISeedStatuesService>();
    await seedRolesService.SeedRoles();
    await seedStatuesService.SeedStatues();
}



if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173").AllowCredentials());

app.UseMiddleware<AuthenticationMiddleware>();

app.UseCors("AllowSpecificOriginsPolicy");


app.MapControllers();

app.UseAuthentication();

app.UseHttpsRedirection();


app.Run();
