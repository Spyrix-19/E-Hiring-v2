using Asp.Versioning;
using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;
using BFPR4B.EHiring.ApiService.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Reports API versions in response headers
    options.AssumeDefaultVersionWhenUnspecified = true; // If a client doesn't specify a version, assume the default
    options.DefaultApiVersion = new ApiVersion(1, 0); // Set default API version to 1.0
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Read API version from URL segment (e.g., /v1/controller)
})
.AddMvc() // IMPORTANT: Chain .AddMvc() if you are using Controllers
// CRITICAL FIX: Uncomment and configure AddApiExplorer for API Versioning
// This registers services that allow Swagger to discover and group API versions.
.AddApiExplorer(options =>
{
    // GroupNameFormat: Defines the format of the API version in Swagger UI dropdowns.
    // "'v'VVV" means "v1", "v2", etc.
    options.GroupNameFormat = "'v'VVV";
    // SubstituteApiVersionInUrl: Tells Swagger to replace the version placeholder in URLs.
    // E.g., /api/v{version}/MyController becomes /api/v1/MyController
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddOpenApi();


builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IFrameworkRepository, FrameworkRepository>();
builder.Services.AddScoped<IRecruitmentRepository, RecruitmentRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddDataProtection(); // Add this line

builder.Services.AddIdentityCore<IdentityUser>()
  .AddRoles<IdentityRole>()
  .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("BFPR4B.EHiring.Server")
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["APISettings:Issuer"],
        ValidAudience = builder.Configuration["APISettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["APISettings:SecretKey"]))
    };

    // Handling token expiration event

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    // Renamed policy to avoid confusion, you had "AllowMultipleOrigins" later but defined "AllowSpecificOrigin"
    options.AddPolicy("AllowAllLocalhostOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:5227", "https://localhost:7222", "https://localhost:56539") // Add your API's origin too
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();  // Allow credentials for cookies or authentication headers
    });
});

var app = builder.Build();

app.UseCors("AllowAllLocalhostOrigins");

app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "BFPR4B EHiring API Version 1.0");
        options.SwaggerEndpoint("/openapi/v2.json", "BFPR4B EHiring API Version 2.0");
    });

    app.UseReDoc(options =>
    {
        options.SpecUrl("/openapi/v1.json");
    });

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
