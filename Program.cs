// using Ultimate_POS_Api.Services;
// using Ultimate_POS_Api.Data;
// using Serilog;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Versioning;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
// using Ultimate_POS_Api.Repository;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddControllers();

// // Swagger configuration
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo
//     {
//         Title = "Ultimate POS API",
//         Version = "v1"
//     });
// });

// // Register Services
// builder.Services.AddScoped<IServices, UltimateServices>();
// builder.Services.AddScoped<IRepository, UltimateRepository>();
// builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


// // Configure Serilog
// Log.Logger = new LoggerConfiguration()
//     .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

// // Configure Logging
// builder.Logging.ClearProviders();
// builder.Logging.AddDebug();
// builder.Logging.AddConsole();

// // Add DbContext for MySQL
// builder.Services.AddDbContext<UltimateDBContext>(options =>
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         new MySqlServerVersion(new Version(8, 0, 39)))
//         .LogTo(message => Log.Error(message), LogLevel.Error)  // Log errors to Serilog
//         .EnableSensitiveDataLogging() // Enable for development only
// );


// // API Versioning
// builder.Services.AddApiVersioning(options =>
// {
//     options.DefaultApiVersion = new ApiVersion(1, 0);
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.ReportApiVersions = true;
//     options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
// });

// // CORS policy
// builder.Services.AddCors(options =>
//     // options.AddPolicy("AllowAll", builder =>
//     //     builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())

//         options.AddPolicy("AllowVueClient",
//         builder =>
//         {
//             builder.WithOrigins("http://localhost:5173") // Vue.js app's URL
//                    .AllowAnyHeader()
//                    .AllowAnyMethod();
//         })
// );

// // Build the app
// var app = builder.Build();


// // Middleware for handling OPTIONS requests for CORS
// app.Use(async (context, next) =>
// {
//     if (context.Request.Method == "OPTIONS")
//     {
//         context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
//         context.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
//         context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
//         context.Response.StatusCode = 200;
//         return;
//     }
//     await next();
// });

// // Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
//         c.RoutePrefix = "Swagger";
//     });
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();
// app.UseAuthentication(); // This should come before authorization

// // app.UseCors("AllowAll");

// app.UseCors("AllowVueClient");

// app.MapControllers();

// app.Run();


using Ultimate_POS_Api.Services;
using Ultimate_POS_Api.Data;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ultimate_POS_Api.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ultimate POS API",
        Version = "v1"
    });
});

// Register Services
builder.Services.AddScoped<IServices, UltimateServices>();
builder.Services.AddScoped<IRepository, UltimateRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddDebug();
builder.Logging.AddConsole();

// Add DbContext for MySQL
builder.Services.AddDbContext<UltimateDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 39)))
        .LogTo(message => Log.Error(message), LogLevel.Error)  // Log errors to Serilog
        .EnableSensitiveDataLogging() // Enable for development only
);

// CORS policy
        builder.Services.AddCors(options =>
         {
            options.AddPolicy("VueApp", PolicyBuilder =>
                {
                    _ = PolicyBuilder.WithOrigins("http://localhost:5173");
                    _ = PolicyBuilder.AllowAnyHeader();
                    _ = PolicyBuilder.AllowAnyMethod();
                    _ = PolicyBuilder.AllowCredentials();
               });
        });

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
});

// // CORS policy
//         builder.Services.AddCors(options =>
//          {
//             options.AddPolicy("VueApp", CorsPolicyBuilder =>
//                 {
//                     CorsPolicyBuilder.WithOrigins("http://localhost:5173");
//                     CorsPolicyBuilder.AllowAnyHeader();
//                     CorsPolicyBuilder.AllowAnyMethod();
//                     CorsPolicyBuilder.AllowCredentials();
//                });
//         });


    // options.AddPolicy("AllowVueClient", builder =>
    // {
    //     builder.WithOrigins("http://localhost:5173") // Vue.js app's URL
    //            .AllowAnyHeader()
    //            .AllowAnyMethod()
    //            .AllowCredentials(); // Add if you are using credentials like cookies or tokens
    // });
// });

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "Swagger";
    });
}

// app.UseHttpsRedirection();

// Ensure CORS is applied before Authentication and Authorization
// app.UseCors("AllowVueClient");
app.UseCors("VueApp");

app.UseAuthentication(); // Should come before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();



