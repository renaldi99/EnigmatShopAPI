using EnigmatShopAPI.Extensions;
using EnigmatShopAPI.Mapper;
using EnigmatShopAPI.Middlewares;
using EnigmatShopAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "ENIGMA SHOP API", Version = "v1" });
    // supaya bisa add auth bearer di swagger
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
});


// EF core add DBContext
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("dev")));
// DI Service & Repository
builder.Services.AddDependencyInjection();
// DI Middleware
builder.Services.AddTransient<HandleExceptionMiddleware>();

// config JWT
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
    option.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            // do something after authentication
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            // do something when authentication
            return Task.CompletedTask;
        }
    };
});

// add global authorize
builder.Services.AddAuthorization(option =>
{
    option.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

// Inisialisasi AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add custom origin
//const string AllowAllHeadersPolicy = "AllowAllHeadersPolicy";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(AllowAllHeadersPolicy,
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:7296")
//                    .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<HandleExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // name tab page
        options.DocumentTitle = "ENIGMA REST API";
    });
}

app.UseHttpsRedirection();

// add security
app.UseAuthentication();
app.UseAuthorization();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();
