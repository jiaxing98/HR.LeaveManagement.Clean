using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
	internal class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
	{
		private readonly ILeaveTypeRepository _repository;
		private readonly IMapper _mapper;
		private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

		public UpdateLeaveTypeCommandHandler(
			ILeaveTypeRepository repository,
			IMapper mapper,
			IAppLogger<UpdateLeaveTypeCommandHandler> logger)
		{
			_repository = repository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateLeaveTypeCommandValidator(_repository);
			var validationResult = await validator.ValidateAsync(request);

			if(!validationResult.IsValid)
			{
				_logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(HR.LeaveManagement.Domain.LeaveType), request.Id);
				throw new BadRequestException("Invalid Leave Type", validationResult);
			}

			var leaveTypeToUpdate = _mapper.Map<HR.LeaveManagement.Domain.LeaveType>(request);
			//if (leaveTypeToUpdate == null) { }
			
			await _repository.UpdateAsync(leaveTypeToUpdate);
			return Unit.Value;
		}
	}
}
