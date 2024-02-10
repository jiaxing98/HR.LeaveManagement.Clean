using Application.Contracts.Logging;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Infrastructure.LoggingService
{
	public class LoggerAdapter<T> : IAppLogger<T>
	{
		private readonly ILogger<T> _logger;

		public LoggerAdapter(ILoggerFactory factory)
		{
			_logger = factory.CreateLogger<T>();
		}

		public void LogInformation(string message, params object[] args)
		{
			_logger.LogInformation(message, args);
		}

		public void LogWarning(string message, params object[] args)
		{
			_logger.LogWarning(message, args);
		}
	}
}
