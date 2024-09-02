using Ultimate_POS_Api.Services;
using Ultimate_POS_Api.Data;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ultimate_POS_Api.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Register Services
builder.Services.AddControllers();
builder.Services.AddScoped<IServices,UltimateServices>();
builder.Services.AddScoped<IRepository,UltimateRepository>();

//builder.Host.UseSerilog();
//configure serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
//configure logging

builder.Logging.ClearProviders();
builder.Logging.AddDebug();
builder.Logging.AddConsole();

//ADD DBCONTEXT 
builder.Services.AddDbContext<UltimateDBContext>(options =>

  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    //log information at different levels -- warining -- Error
    .LogTo(message => Log.Error(message), LogLevel.Error) // Log to Serilog
    .EnableSensitiveDataLogging()
    );



builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api",
        Version = "v1"
    });
});
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()));






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "Swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseCors("AllowAllOrigins");

app.Run();
