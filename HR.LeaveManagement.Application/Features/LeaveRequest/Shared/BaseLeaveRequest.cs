using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public abstract class BaseLeaveRequest
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
