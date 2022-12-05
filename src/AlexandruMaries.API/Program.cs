using AlexandruMaries.API.Helpers;
using AlexandruMaries.Data;
using AlexandruMaries.Data.Interfaces;
using AlexandruMaries.Data.Repositories;
using AlexandruMaries.Infrastructure.Interfaces;
using AlexandruMaries.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var policyName = "myPolicy";

// Add services to the container.

builder.Services.AddCors(options =>
{
	options.AddPolicy(policyName,
		builder =>
		{
			builder.WithOrigins("http://localhost:3000", "https://happy-sand-0fffc8d03.2.azurestaticapps.net", "https://alexandrumaries.com")
				.WithMethods("GET", "POST", "DELETE", "PATCH", "PUT")
				.AllowAnyHeader();
		});
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
});
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


builder.Services.AddDbContext<AlexandruMariesDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"),
	b => b.MigrationsAssembly("AlexandruMaries.Data")
	));
builder.Services.AddScoped<IReferenceRepository, ReferenceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IViewsRepository, ViewsRepository>();


// for authentication /////////////****************\\\\\\\\\\\\\\\\\\\\\\
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
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
