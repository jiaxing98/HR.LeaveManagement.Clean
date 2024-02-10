using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
	public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
	{
		Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
		Task<List<LeaveAllocation>> GetLeaveAllocationListWithDetails();
		Task<List<LeaveAllocation>> GetLeaveAllocationListWithDetails(string userId);
		Task<bool> IsAllocationExists(string userId, int leaveTypeId, int period);
		Task AddAllocations(List<LeaveAllocation> allocations);
		Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
	}
}
