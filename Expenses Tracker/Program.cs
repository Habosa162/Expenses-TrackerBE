using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using ExpensesTracker.Core.Models;
using ExpensesTracker.Infrastructure.Data;
using Expenses_Tracker.Middlewares;
using ExpensesTracker.Core.Abstraction.Repositories;
using ExpensesTracker.Infrastructure.Repositories;
using ExpensesTracker.Application.Features.Categories.Queries;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Diagnostics;
using ExpensesTracker.Application.Helpers;
using ExpensesTracker.Application.Interfaces;
using Amazon.Runtime;
using Amazon.S3;
using ExpensesTracker.Application.Services;



var builder = WebApplication.CreateBuilder(args);

// Serilog Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/exceptions.logs.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
    .CreateLogger();
builder.Host.UseSerilog();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionTypesRepository, TransactionTypesRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAwsService, AwsService>(); 
//AWS
builder.Services.AddSingleton<AWSCredentials>(sp =>
new BasicAWSCredentials(
   builder.Configuration["AWS:AccessKey"],
   builder.Configuration["AWS:SecretKey"]
)
);

builder.Services.AddSingleton<IAmazonS3>(sp =>
    new AmazonS3Client(
        sp.GetRequiredService<AWSCredentials>(),
        Amazon.RegionEndpoint.GetBySystemName(builder.Configuration["AWS:Region"])
    )
);

// AutoMapper & MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllCategoriesQuery).Assembly));
builder.Services.AddAutoMapper(
    typeof(GetAllCategoriesQuery).Assembly,
    typeof(Program).Assembly
);

// Authentication (JWT)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    jwtOptions.MapInboundClaims = false;    
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});





// Rate Limiting

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ipAddress,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100, // max requests
                Window = TimeSpan.FromMinutes(1), // per time window
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0 // requests over the limit will be rejected immediately
            });
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Controllers + API Docs
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SupportNonNullableReferenceTypes();
    c.OperationFilter<SwaggerFileOperationFilter>();
});



//builder.Services.AddOpenApi(); // For Scalar

// Serilog for DI
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

// Build App
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await AspRoles.SeedRolesAsync(roleManager);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    await AspRoles.SeedAdminAsync(userManager, roleManager);
}


app.UseSwagger();
app.UseSwaggerUI();

//app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

// Global Error Logging Middleware
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// Middlewares
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
//        var exception = exceptionHandler?.Error;
//        Log.Error(exception, "Unhandled exception");
//        context.Response.StatusCode = 500;
//        await context.Response.WriteAsync("An unexpected error occurred.");
//    });
//});
// App Run
try
{
    Log.Information("Starting the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}
