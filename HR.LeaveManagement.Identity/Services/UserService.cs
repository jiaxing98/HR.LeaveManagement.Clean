using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HR.LeaveManagement.Identity.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManger;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManger, IHttpContextAccessor contextAccessor)
        {
            _userManger = userManger;
            _contextAccessor = contextAccessor;
        }

        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

        public async Task<Employee> GetEmployee(string userId)
		{
			var employee = await _userManger.FindByIdAsync(userId);
			return new Employee
			{
				Email = employee.Email,
				Id = employee.Id,
				FirstName = employee.FirstName,
				LastName = employee.LastName,
			};
		}

		public async Task<List<Employee>> GetEmployees()
		{
			var employees = await _userManger.GetUsersInRoleAsync("Employee");
			return employees.Select(x => new Employee
			{
				Id = x.Id,
				Email = x.Email,
				FirstName = x.FirstName,
				LastName = x.LastName,
			}).ToList();
		}
	}
}
