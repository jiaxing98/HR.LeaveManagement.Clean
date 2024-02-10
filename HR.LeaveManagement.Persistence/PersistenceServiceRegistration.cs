using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HR.LeaveManagement.Persistence.DataContexts;
using HR.LeaveManagement.Persistence.Repositories;

namespace HR.LeaveManagement.Persistence
{
	public static class PersistenceServiceRegistration
	{
		public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<EntityDbContext>(options =>
			{
				var connectionString = configuration.GetConnectionString("Default");
				options.UseSqlServer(connectionString);
			});

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
			services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
			services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

			return services;
		}
	}
}
