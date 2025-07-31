using Asp.Versioning;
using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Repository.IRepository;
using BFPR4B.EHiring.ApiService.Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; 
    options.AssumeDefaultVersionWhenUnspecified = true; 
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); 
})
.AddMvc() 
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddOpenApi("v1", options =>
{
    options.ShouldInclude = (apiDescription) => apiDescription.GetApiVersion().MajorVersion == 1;
});

builder.Services.AddOpenApi("v2", options =>
{
    options.ShouldInclude = (apiDescription) => apiDescription.GetApiVersion().MajorVersion == 2;
});

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
    options.AddPolicy("AllowAllLocalhostOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:5227", "https://localhost:7222", "https://localhost:56539") // Add your API's origin too
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();  
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
        options.RoutePrefix = "redoc/v1";
    });

    app.UseReDoc(options =>
    {
        options.SpecUrl("/openapi/v2.json");
        options.RoutePrefix = "redoc/v2";
    });

    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

//var apiDescriptionProvider = app.Services.GetService<IApiDescriptionGroupCollectionProvider>();
//if (apiDescriptionProvider != null)
//{
//    Console.WriteLine("\n--- Discovered API Description Groups (via IApiDescriptionGroupCollectionProvider) ---");
//    foreach (var group in apiDescriptionProvider.ApiDescriptionGroups.Items)
//    {
//        Console.WriteLine($"Group Name: {group.GroupName ?? "No Name"}");
//        foreach (Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription apiDescription in group.Items) // <-- Explicitly qualify type here
//        {
//            Console.WriteLine($"  Path: {apiDescription.RelativePath}, Method: {apiDescription.HttpMethod}");
//            foreach (var version in apiDescription.ApiVersions)
//            {
//                Console.WriteLine($"    API Version: {version}");
//            }
//        }
//    }
//    Console.WriteLine("-----------------------------------------------------------------------------------\n");
//}

app.Run();
