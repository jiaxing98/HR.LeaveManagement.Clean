﻿using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveType
{
	public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
