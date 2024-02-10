using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.LeaveManagement.Application
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
			});

			return services;
		}
	}
}
