using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
	public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
	{
		public LeaveAllocationRepository(EntityDbContext context) : base(context) { }

		public async Task AddAllocations(List<LeaveAllocation> allocations)
		{
			await _context.AddRangeAsync(allocations);
			await _context.SaveChangesAsync();
		}

		public async Task<List<LeaveAllocation>> GetLeaveAllocationListWithDetails()
		{
			var leaveAllocations = await _context.LeaveAllocations
				.Include(q => q.LeaveType)
				.ToListAsync();

			return leaveAllocations;
		}

		public async Task<List<LeaveAllocation>> GetLeaveAllocationListWithDetails(string userId)
		{
			var leaveAllocations = await _context.LeaveAllocations
							.Where(q => q.EmployeeId == userId)
							.Include(q => q.LeaveType)
							.ToListAsync();

			return leaveAllocations;
		}

		public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
		{
			var leaveAllocation = await _context.LeaveAllocations
							.Include(q => q.LeaveType)
							.FirstOrDefaultAsync(q => q.Id == id);

			return leaveAllocation;
		}

		public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
		{
			var leaveAllocation = await _context.LeaveAllocations
				.FirstOrDefaultAsync(q => q.EmployeeId == userId
				&& q.LeaveTypeId == leaveTypeId);

			return leaveAllocation;
		}

		public async Task<bool> IsAllocationExists(string userId, int leaveTypeId, int period)
		{
			return await _context.LeaveAllocations
				.AnyAsync(q => q.EmployeeId == userId
				&& q.LeaveTypeId == leaveTypeId
				&& q.Period == period);
		}
	}
}
