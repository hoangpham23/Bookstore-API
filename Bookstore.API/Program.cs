using Bookstore.Infrastructure;
using BookStore.Application;
using Microsoft.EntityFrameworkCore;
using Bookstore.Core.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using DotNetEnv;

namespace Bookstore.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			Env.Load();
			// Add services to the container.
			var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
			builder.Configuration["JwtSettings:SecretKey"] = secretKey;

			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
			});

			builder.Services.AddAuthentication(options =>
			{
				// Set the default authentication scheme to JWT Bearer tokens
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

				// Handler the response http status for the invalid token (401 - 403)
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(option =>
			{
				option.RequireHttpsMetadata = false;

				// Save the token after successful authentication and able to send token back to client
				option.SaveToken = true;

				// Need to validate those attributes
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
					ValidAudience = builder.Configuration["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

					// no grace period for token expiration
					ClockSkew = TimeSpan.Zero,
					RoleClaimType = "Role"
				};
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookstore API", Version = "v1" });

				// Cấu hình Authorization
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter your token"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});

			builder.Services.AddApplication().AddInfrastructure();

			builder.Services.AddDbContext<BookManagementDbContext>(options
				=> options.UseLazyLoadingProxies().UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 29))));

			builder.Services.Configure<RouteOptions>(options =>
			{
				options.LowercaseUrls = true;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
