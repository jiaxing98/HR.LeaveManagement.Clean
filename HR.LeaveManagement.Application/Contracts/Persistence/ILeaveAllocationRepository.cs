﻿using HR.LeaveManagement.Domain;

namespace Application.Contracts.Persistence
{
	public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
	{
		Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
		Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
		Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
		Task<bool> IsAllocationExists(string userId, int leaveTypeId, int period);
		Task AddAllocations(List<LeaveAllocation> allocations);
		Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
	}
}