﻿using Application.Contracts.Email;
using Application.Contracts.Logging;
using Application.Models.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using HR.LeaveManagement.Infrastructure.LoggingService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

			return services;
		}
	}
}