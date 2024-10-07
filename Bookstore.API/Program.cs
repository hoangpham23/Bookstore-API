using Bookstore.Domain.Abstractions;
using Bookstore.Infrastructure;
using Bookstore.Infrastructure.Repositories;
using BookStore.Application;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BookStore.Application.QueryHandlers;

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
			//builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();

			builder.Services.AddApplication().AddInfrastructure();

			builder.Services.AddDbContext<BookManagementDbContext>(options 
				=> options.UseLazyLoadingProxies().UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8,0,29))));

			builder.Services.Configure<RouteOptions>(options =>
			{
				options.LowercaseUrls = true;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			//if (app.Environment.IsDevelopment())
			//{
			//	app.UseSwagger();
			//	app.UseSwaggerUI();
			//}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
