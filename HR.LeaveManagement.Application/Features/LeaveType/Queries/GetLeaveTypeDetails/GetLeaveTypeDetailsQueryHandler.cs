using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
	public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
	{
		private readonly ILeaveTypeRepository _repository;
		private readonly IMapper _mapper;
		private readonly IAppLogger<GetLeaveTypeDetailsQueryHandler> _logger;

		public GetLeaveTypeDetailsQueryHandler(
			ILeaveTypeRepository repository,
			IMapper mapper,
			IAppLogger<GetLeaveTypeDetailsQueryHandler> logger)
		{
			_repository = repository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
		{
			var leaveType = await _repository.GetByIdAsync(request.Id);
			if (leaveType == null)
			{
				throw new NotFoundException(nameof(HR.LeaveManagement.Domain.LeaveType), request.Id);
			}

			var dto = _mapper.Map<LeaveTypeDetailsDto>(leaveType);
			_logger.LogInformation("Leave types were retrieved successfully");
			return dto;
		}
	}
}
