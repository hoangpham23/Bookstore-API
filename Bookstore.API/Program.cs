using Application.PersonCmd.Commands;
using Bookstore.Domain.Abstractions;
using Bookstore.Infrastructure;
using Bookstore.Infrastructure.Repositories;
using BookStore.Application;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Bookstore.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddApplication().AddInfrastructure();

			builder.Services.AddDbContext<ApplicationDbContext>(options 
				=> options.UseLazyLoadingProxies().UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8,0,29))));

			builder.Services.AddMediatR(typeof(CreatePerson).Assembly);

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
