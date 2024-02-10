using Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
	public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
	{
		public LeaveRequestRepository(EntityDbContext context) : base(context) { }

		public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
		{
			var leaveRequests = await _context.LeaveRequests
				.Include(q => q.LeaveType)
				.ToListAsync();

			return leaveRequests;
		}

		public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
		{
			var leaveRequests = await _context.LeaveRequests
				.Where(q => q.RequestingEmployeeId == userId)
				.Include(q => q.LeaveType)
				.ToListAsync();

			return leaveRequests;
		}

		public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
		{
			var leaveRequest = await _context.LeaveRequests
				.Include(q => q.LeaveType)
				.FirstOrDefaultAsync(q => q.Id == id);

			return leaveRequest;
		}
	}
}
