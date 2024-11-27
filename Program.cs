using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RunningGroupAPI.Data;
using RunningGroupAPI.Helpers;
using RunningGroupAPI.Helpers.AuthorizationHandler;
using RunningGroupAPI.Helpers.AutoMappers;
using RunningGroupAPI.Interfaces.Services;
using RunningGroupAPI.Models;
using RunningGroupAPI.Repositories;
using RunningGroupAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Enable controller for the API
builder.Services.AddControllers();

//Database connection
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(Environment.GetEnvironmentVariable("MY_CONNECTION_STRING")));
//Identity configuration
builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();


//Authentication configuration
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
		ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")))
	};
});

//Authorization configuration

builder.Services.AddAuthorization(options => 
{
	options.AddPolicy("ClubOwnerOrAdmin", policy => 
	{
		policy.RequireAuthenticatedUser();
		policy.AddRequirements(new ClubOwnerOrAdminRequirement());
		
	});
	
	options.AddPolicy("Admin", policy => 
	{
		policy.RequireAuthenticatedUser();
		policy.RequireRole(UserRoles.Admin);
	});
});	

builder.Services.AddScoped<IAuthorizationHandler, ClubOwnerOrAdminHandler>();

//AutoMapping
builder.Services.AddAutoMapper(typeof(ClubMappingProfile));

//Cloudinary Configuration
var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
{
    throw new Exception("Cloudinary environment variables are not set correctly.");
}
CloudinarySettings cloudinarySettings = new (cloudName, apiKey, apiSecret);


//My repositories
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

//My services
builder.Services.AddScoped<IAuthenticationService, AuthentiCationService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddSingleton<IPhotoService>(new CloudinaryPhotoService(cloudinarySettings));
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IClubMembershipService, ClubMembershipService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

