using System.Text;
using AlexandruMaries.Data.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
var policyName = "myPolicy";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "https://happy-sand-0fffc8d03.2.azurestaticapps.net",
                    "https://alexandrumaries.com")
                .WithMethods("GET", "POST", "DELETE", "PATCH", "PUT")
                .AllowAnyHeader();
        });
});
builder.Services.AddControllers().AddJsonOptions(options => { });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// auth for swagger /////////////****************\\\\\\\\\\\\\\\\\\\\\\
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standar Auth header using the Bearer scheme, e.g. \"bearer {token} \"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDataServices(builder.Configuration.GetConnectionString("LocalConnection")!);

// for authentication /////////////****************\\\\\\\\\\\\\\\\\\\\\\
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyName);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();