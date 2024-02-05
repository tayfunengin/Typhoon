using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Typhoon.Domain.Mappings;
using Typhoon.Respository;
using Typhoon.Respository.Context;
using Typhoon.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conntectionString = builder.Configuration.GetConnectionString("TyphoonDbConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(conntectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Typhoon's API",
        Description = "An Example API For Managing Category and Products",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Tayfun Engin",
            Email = "engin.tayfun@gmail.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));


#region Dependency Injection
builder.Services.AddServices();
builder.Services.AddRepositories();
#endregion

builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Typhoon's API v1");
    });
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
