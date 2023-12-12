using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using note_app_API.Database;
using note_app_API.Extensions;
using note_app_API.Profiles;
using note_app_API.Services.CheckListItemService;
using note_app_API.Services.NoteService;
using note_app_API.Services.TokenService;
using note_app_API.Services.UserService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var services = builder.Services;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IUserService, UserService>();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<INoteService, NoteService>();
services.AddScoped<ICheckListItemService, CheckListItemService>();


services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

services.AddCors(options =>
{
    options.AddPolicy(name: "note-app",
                      policy =>
                      {
                          policy.WithOrigins("localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyHeader()
                          .AllowCredentials();
                      });
});

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = false,
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecretKey"]!))
       };
   });



var ConnectionString = builder.Configuration.GetConnectionString("Default");
services.AddDbContext<DataContext>(option =>
                                   option.UseSqlServer(ConnectionString)
                                   .EnableDetailedErrors(true)
                                   .EnableSensitiveDataLogging(true)
                                   );

var app = builder.Build();


var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var dataContext = serviceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
    DataSeeder.SeedData(dataContext);

}
catch(Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database.");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("note-app");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
