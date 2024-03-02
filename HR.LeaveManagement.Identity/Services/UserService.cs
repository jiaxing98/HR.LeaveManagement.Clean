using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManger;

		public UserService(UserManager<ApplicationUser> userManger)
		{
			_userManger = userManger;
		}

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
