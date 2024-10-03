using Bookstore.Domain.Abstractions;
using Bookstore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		return services;
	}
}
