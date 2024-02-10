using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
	public record DeleteLeaveTypeCommand : IRequest<Unit>
	{
		public int Id { get; set; }
	}
}
