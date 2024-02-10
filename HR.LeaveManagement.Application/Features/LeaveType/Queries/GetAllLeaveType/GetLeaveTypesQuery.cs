using MediatR;

namespace Application.Features.LeaveType.Queries.GetAllLeaveType
{
	public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
